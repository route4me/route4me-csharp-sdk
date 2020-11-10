using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Immutable;
using Quobject.SocketIoClientDotNet.EngineIoClientDotNet;
using Quobject.SocketIoClientDotNet.EngineIoClientDotNet.Client.Transports;
using Client = Quobject.SocketIoClientDotNet.Client;
using IO = Quobject.SocketIoClientDotNet.Client.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Route4MeSDK.DataTypes;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;

namespace Route4MeSDK.FastProcessing
{
    /// <summary>
    /// The class for the geocoding of bulk addresses
    /// </summary>
    public class FastBulkGeocoding : Connection
    {
        private ManualResetEvent manualResetEvent = null;
        private ManualResetEvent mainResetEvent = null;
        private Socket socket;
        public string Message;
        private int Number;
        private bool Flag;
        public static Connection con = new Connection();
        private int requestedAddresses;
        private int nextDownloadStage;
        private int loadedAddressesCount;
        string TEMPORARY_ADDRESSES_STORAGE_ID;

        FastFileReading fileReading;

        bool largeJsonFileProcessingIsDone;
        bool geocodedAddressesDownloadingIsDone;

        List<AddressGeocoded> savedAddresses;

        JsonSerializer jsSer = new JsonSerializer();

        public string apiKey { get; set; }

        public FastBulkGeocoding(string ApiKey, bool EnableTraceSource = false)
        {
            if (ApiKey!="") apiKey = ApiKey;
            Quobject.SocketIoClientDotNet.TraceSourceTools.LogTraceSource.TraceSourceLogging(EnableTraceSource);
        }

        #region // Addresses chunk's geocoding is finished event handler
        public event EventHandler<AddressesChunkGeocodedArgs> AddressesChunkGeocoded;

        protected virtual void OnAddressesChunkGeocoded(AddressesChunkGeocodedArgs e)
        {
            EventHandler< AddressesChunkGeocodedArgs> handler = AddressesChunkGeocoded;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        public class AddressesChunkGeocodedArgs : EventArgs
        {
            public List<AddressGeocoded> lsAddressesChunkGeocoded { get; set; }
        }

        public delegate void AddressesChunkGeocodedEventHandler(object sender, AddressesChunkGeocodedArgs e);
        #endregion

        #region // geocoding is finished event handler
        public event EventHandler<GeocodingIsFinishedArgs> GeocodingIsFinished;

        protected virtual void OnGeocodingIsFinished(GeocodingIsFinishedArgs e)
        {
            EventHandler<GeocodingIsFinishedArgs> handler = GeocodingIsFinished;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        public class GeocodingIsFinishedArgs : EventArgs
        {
            public bool isFinished { get; set; }
        }

        public delegate void GeocodingIsFinishedEventHandler(object sender, AddressesChunkGeocodedArgs e);
        #endregion


        /// <summary>
        /// Upload and geocode large JSON file
        /// </summary>
        /// <param name="fileName">JSON file name</param>
        public void uploadAndGeocodeLargeJsonFile(string fileName)
        {
            var route4Me = new Route4MeManager(apiKey);

            largeJsonFileProcessingIsDone = false;

            fileReading = new FastFileReading();

            fileReading.jsonObjectsChunkSize = 200;

            savedAddresses = new List<AddressGeocoded>();

            fileReading.JsonFileChunkIsReady += FileReading_JsonFileChunkIsReady;

            fileReading.JsonFileReadingIsDone += FileReading_JsonFileReadingIsDone;

            mainResetEvent = new ManualResetEvent(false);

            fileReading.readingChunksFromLargeJsonFile(fileName);

        }

        /// <summary>
        /// Event handler for the JsonFileReadingIsDone event
        /// </summary>
        /// <param name="sender">Event raiser object</param>
        /// <param name="e">Event arguments of the type JsonFileReadingIsDoneArgs</param>
        private void FileReading_JsonFileReadingIsDone(object sender, FastFileReading.JsonFileReadingIsDoneArgs e)
        {
            bool isDone = e.IsDone;
            if (isDone)
            {
                largeJsonFileProcessingIsDone = true;
                mainResetEvent.Set();
                if (geocodedAddressesDownloadingIsDone)
                {
                    OnGeocodingIsFinished(new GeocodingIsFinishedArgs() { isFinished = true });
                }
                // fire here event for external (test) code
            }

        }


        /// <summary>
        /// Event handler for the JsonFileChunkIsReady event
        /// </summary>
        /// <param name="sender">Event raiser object</param>
        /// <param name="e">Event arguments of the type JsonFileChunkIsReadyArgs</param>
        private void FileReading_JsonFileChunkIsReady(object sender, FastFileReading.JsonFileChunkIsReadyArgs e)
        {
            string jsonAddressesChunk = e.AddressesChunk;

            var uploadAddressesResponse = uploadAddressesToTemporaryStorage(jsonAddressesChunk);

            if (uploadAddressesResponse!=null)
            {
                string tempAddressesStorageID = uploadAddressesResponse.optimization_problem_id;
                int addressesInChunk = (int)uploadAddressesResponse.address_count;

                if (addressesInChunk < fileReading.jsonObjectsChunkSize) requestedAddresses = addressesInChunk; // last chunk

                downloadGeocodedAddresses(tempAddressesStorageID, addressesInChunk);
            }
            
        }

        /// <summary>
        /// Upload JSON addresses to a temporary storage
        /// </summary>
        /// <param name="streamSource">Input stream source - file name or JSON text</param>
        /// <returns>Response object of the type uploadAddressesToTemporaryStorageResponse</returns>
        public Route4MeManager.uploadAddressesToTemporaryStorageResponse uploadAddressesToTemporaryStorage(string streamSource)
        {
            Route4MeManager route4Me = new Route4MeManager(apiKey);

            //List<AddressField> lsAddresses = readLargeJsonFileOfAddresse(sFileName);

            string jsonText = "";

            if (streamSource.Contains("{") && streamSource.Contains("}")) 
                jsonText = streamSource;
            else
                jsonText = readJsonTextFromLargeJsonFileOfAddresses(streamSource);

            string errorString = "";

            Route4MeManager.uploadAddressesToTemporaryStorageResponse uploadResponse =
                route4Me.uploadAddressesToTemporaryStorage(jsonText, out errorString);


            if (uploadResponse == null || !uploadResponse.status) return null;

            return uploadResponse;
        }

        /// <summary>
        /// Geocode and download the addresses from the temporary storage.
        /// </summary>
        /// <param name="temporaryAddressesStorageID">ID of the temporary storage</param>
        /// <param name="addressesInFile">Chunk size of the addresses to be geocoded</param>
        public async void downloadGeocodedAddresses(string temporaryAddressesStorageID, int addressesInFile)
        {
            //bool done = false;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
            | SecurityProtocolType.Tls11
            | SecurityProtocolType.Tls12
            | (SecurityProtocolType)12288;

            geocodedAddressesDownloadingIsDone = false;

            savedAddresses = new List<AddressGeocoded>();

            TEMPORARY_ADDRESSES_STORAGE_ID = temporaryAddressesStorageID;
            if (addressesInFile != null) requestedAddresses = addressesInFile;

            manualResetEvent = new ManualResetEvent(false);
            Flag = false;

            var options = CreateOptions();
            options.Path = "/socket.io";
            options.Host = "validator.route4me.com/";
            options.AutoConnect = true;
            options.IgnoreServerCertificateValidation = true;
            options.Timeout = 60000;
            options.Upgrade = true;
            options.ForceJsonp = true;
            options.Transports = ImmutableList.Create<string>(new string[] { Polling.NAME, WebSocket.NAME });
            

            var uri = CreateUri();
            socket = IO.Socket(uri, options);
            

            socket.On("error", (message) =>
            {
                Debug.Print("Error -> " + message);
                //await Task.Delay(500);
                Thread.Sleep(500);
                manualResetEvent.Set();
                socket.Disconnect();
                //manualResetEvent.Set();
            });

            socket.On(Socket.EVENT_ERROR, (e) =>
            {
                var exception = (Quobject.SocketIoClientDotNet.EngineIoClientDotNet.Client.EngineIOException)e;
                Console.WriteLine("EVENT_ERROR. "+exception.Message);
                Console.WriteLine("BASE EXCEPTION. " + exception.GetBaseException());
                Console.WriteLine("DATA COUNT. " + exception.Data.Count);
                //events.Enqueue(exception.code);
                socket.Disconnect();
                //manager.Close();
                 manualResetEvent.Set(); ;
            });

            socket.On(Socket.EVENT_MESSAGE, (message) =>
            {
                //Debug.Print("Error -> " + message);
                //await Task.Delay(500);
                Thread.Sleep(500);
                //manualResetEvent.Set();
            });

            socket.On("data", (d) =>
            {
                //Debug.Print("data -> " + d.ToString());
                //await Task.Delay(1000);
                Thread.Sleep(1000);
                //manualResetEvent.Set();
            });

            socket.On(Socket.EVENT_CONNECT, () =>
            {
                //Debug.Print("Socket opened");
                //socket.Close();
                //await Task.Delay(500);
                Thread.Sleep(500);

                //manualResetEvent.Set();
            });

            socket.On(Socket.EVENT_DISCONNECT, () =>
            {
                //Debug.Print("Socket disconnected");
                //socket.Close();
                //await Task.Delay(500);
                Thread.Sleep(700);

                //manualResetEvent.Set();
            });

            socket.On(Socket.EVENT_RECONNECT_ATTEMPT, () =>
            {
                //Debug.Print("Socket reconnect attempt");
                //socket.Close();
                //await Task.Delay(1000);
                Thread.Sleep(1500);

                //manualResetEvent.Set();
            });

            socket.On("addresses_bulk", (addresses_chunk) =>
            {
                //Debug.Print("addresses_chunk received");

                //await Task.Delay(500);

                string jsonChunkText = addresses_chunk.ToString();

                List<string> errors = new List<string>();

                JsonSerializerSettings jsonSettings = new JsonSerializerSettings()
                {
                    Error = delegate (object sender, ErrorEventArgs args)
                    {
                        errors.Add(args.ErrorContext.Error.Message);
                        args.ErrorContext.Handled = true;
                    },
                    NullValueHandling = NullValueHandling.Ignore
                };

                var addressesChunk = JsonConvert.DeserializeObject<AddressGeocoded[]>(jsonChunkText, jsonSettings);

                if (errors.Count>0)
                {
                    Debug.Print("Json serializer errors:");
                    foreach (string errMessage in errors) Debug.Print(errMessage);
                }
                
                savedAddresses = savedAddresses.Concat(addressesChunk).ToList();

                loadedAddressesCount += addressesChunk.Length;

                //Debug.Print(addressesChunk.Length.ToString());

                //Debug.Print("Got chunks from websocket %s / %s", loadedAddressesCount, requestedAddresses);
                if (loadedAddressesCount == nextDownloadStage)
                {
                    //Debug.Print("Downloading");
                    download(loadedAddressesCount);
                }

                if (loadedAddressesCount == requestedAddresses)
                {
                    //Debug.Print("First address:", savedAddresses[0].geocodedAddress);
                    //Debug.Print("Done, saved addresses %s", savedAddresses.Count);

                    socket.Emit("disconnect", TEMPORARY_ADDRESSES_STORAGE_ID);
                    loadedAddressesCount = 0;
                    AddressesChunkGeocodedArgs args = new AddressesChunkGeocodedArgs() { lsAddressesChunkGeocoded = savedAddresses };
                    OnAddressesChunkGeocoded(args);

                    manualResetEvent.Set();

                    geocodedAddressesDownloadingIsDone = true;

                    if (largeJsonFileProcessingIsDone)
                    {
                        OnGeocodingIsFinished(new GeocodingIsFinishedArgs() { isFinished = true });
                    }

                    socket.Close();
                }
                
            });

            socket.On("geocode_progress", (message) =>
            {
                //Debug.Print("Progress from websocket:", message.ToString());

                var progressMessage = JsonConvert.DeserializeObject<clsProgress>(message.ToString());

                if (progressMessage.total == progressMessage.done)
                {
                    //Debug.Print("Geocoding Done, Downloading...");
                    if (requestedAddresses == null) requestedAddresses = progressMessage.total;
                    download(0);
                }
            });

            var jobj = new JObject();
            jobj.Add("temporary_addresses_storage_id", TEMPORARY_ADDRESSES_STORAGE_ID);
            jobj.Add("force_restart", true);

            var _args = new List<object> { };
            _args.Add(jobj);

            try
            {
                socket.Emit("geocode", _args);
            }
            catch (Exception ex)
            {
                Debug.Print("Socket connection failed. " + ex.Message);
            }

            manualResetEvent.WaitOne();

        }

        /// <summary>
        /// Download chunk of the geocoded addresses
        /// </summary>
        /// <param name="start">Download addresses starting from</param>
        public void download(int start)
        {
            int bufferFailSafeMaxAddresses = 100;
            int chunkSize = (int)Math.Round((decimal)(Math.Min(200, Math.Max(10, requestedAddresses / 100))));
            int chunksLimit = (int)Math.Ceiling(((decimal)(bufferFailSafeMaxAddresses / chunkSize)));

            int maxAddressesToBeDownloaded = chunkSize * chunksLimit;
            nextDownloadStage = loadedAddressesCount + maxAddressesToBeDownloaded;

            // from_index = (chunks_limit * chunk_size);
            var jobj = new JObject();

            jobj.Add("temporary_addresses_storage_id", TEMPORARY_ADDRESSES_STORAGE_ID);
            jobj.Add("from_index", start);
            jobj.Add("chunks_limit", chunksLimit);
            jobj.Add("chunk_size", chunkSize);

            var _args = new List<object> { };
            _args.Add(jobj);
            //var data = Quobject.SocketIoClientDotNet.Parser.Packet.Args2JArray(_args);

            socket.Emit("download", _args);
        }

        public string readJsonTextFromLargeJsonFileOfAddresses(String sFileName)
        {
            FastFileReading fileRead = new FastFileReading();

            return fileRead != null ? fileRead.readJsonTextFromFile(sFileName) : String.Empty;
        }
    }

    /// <summary>
    /// Response class of the received event about geocoding progress
    /// </summary>
    class clsProgress
    {
        public int total { get; set; }

        public int done { get; set; }
    }
}
