using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quobject.EngineIoClientDotNet.Client;
using Quobject.EngineIoClientDotNet.Client.Transports;
using Quobject.SocketIoClientDotNet.Client;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using AddressBookContact = Route4MeSDK.DataTypes.V5.AddressBookContact;
using ErrorEventArgs = Newtonsoft.Json.Serialization.ErrorEventArgs;
using Socket = Quobject.SocketIoClientDotNet.Client.Socket;

namespace Route4MeSDK.FastProcessing
{
    /// <summary>
    ///     The class for the geocoding of bulk addresses
    /// </summary>
    public class FastBulkGeocoding : Connection
    {
        public static Connection con = new Connection();
        public string Message;

        private static List<List<AddressBookContact>> _threadPackage;
        private FastFileReading _fileReading;
        private bool _geocodedAddressesDownloadingIsDone;
        private bool _largeJsonFileProcessingIsDone;
        private int _loadedAddressesCount;
        private ManualResetEvent _mainResetEvent;
        private ManualResetEvent _manualResetEvent;
        private int _nextDownloadStage;
        private int _requestedAddresses;
        private List<AddressGeocoded> _savedAddresses;
        private Socket _socket;
        private string _temporaryAddressesStorageId;
        private int _totalCsvChunks;

        [Obsolete("EnableTraceSource is not used anymore. Use overloaded constructor without EnableTraceSource")]
        public FastBulkGeocoding(string ApiKey, bool EnableTraceSource = false) : this(ApiKey)
        {
        }

        public FastBulkGeocoding(string ApiKey)
        {
            if (ApiKey != "") apiKey = ApiKey;
            _threadPackage = new List<List<AddressBookContact>>();
        }

        public string apiKey { get; set; }

        public int CsvChunkSize { get; set; } = 300;
        public int JsonChunkSize { get; set; } = 300;
        public int ChunkPause { get; set; } = 2000;

        public string[] MandatoryFields { get; set; }

        public bool DoGeocoding { get; set; } = false;

        /// <summary>
        ///     Geocode only addresses with empty coordinates (latitude longitude)
        /// </summary>
        public bool GeocodeOnlyEmpty { get; set; } = false;


        /// <summary>
        ///     Upload and geocode large JSON file
        /// </summary>
        /// <param name="fileName">JSON file name</param>
        public void uploadAndGeocodeLargeJsonFile(string fileName)
        {
            _largeJsonFileProcessingIsDone = false;

            _fileReading = new FastFileReading();

            _fileReading.jsonObjectsChunkSize = 200;

            _savedAddresses = new List<AddressGeocoded>();

            _fileReading.JsonFileChunkIsReady += FileReading_JsonFileChunkIsReady;

            _fileReading.JsonFileReadingIsDone += FileReading_JsonFileReadingIsDone;

            _mainResetEvent = new ManualResetEvent(false);

            _fileReading.readingChunksFromLargeJsonFile(fileName);
        }

        /// <summary>
        ///     Event handler for the JsonFileReadingIsDone event
        /// </summary>
        /// <param name="sender">Event raiser object</param>
        /// <param name="e">Event arguments of the type JsonFileReadingIsDoneArgs</param>
        private void FileReading_JsonFileReadingIsDone(object sender, FastFileReading.JsonFileReadingIsDoneArgs e)
        {
            var isDone = e.IsDone;
            if (isDone)
            {
                _largeJsonFileProcessingIsDone = true;
                _mainResetEvent.Set();
                if (_geocodedAddressesDownloadingIsDone)
                    OnGeocodingIsFinished(new GeocodingIsFinishedArgs {isFinished = true});
                // fire here event for external (test) code
            }
        }


        /// <summary>
        ///     Event handler for the JsonFileChunkIsReady event
        /// </summary>
        /// <param name="sender">Event raiser object</param>
        /// <param name="e">Event arguments of the type JsonFileChunkIsReadyArgs</param>
        private void FileReading_JsonFileChunkIsReady(object sender, FastFileReading.JsonFileChunkIsReadyArgs e)
        {
            var jsonAddressesChunk = e.AddressesChunk;

            var uploadAddressesResponse = uploadAddressesToTemporaryStorage(jsonAddressesChunk);

            if (uploadAddressesResponse != null)
            {
                var tempAddressesStorageID = uploadAddressesResponse.OptimizationProblemId;
                var addressesInChunk = (int) uploadAddressesResponse.AddressCount;

                if (addressesInChunk < _fileReading.jsonObjectsChunkSize)
                    _requestedAddresses = addressesInChunk; // last chunk

                downloadGeocodedAddresses(tempAddressesStorageID, addressesInChunk);
            }
        }

        public void uploadLargeContactsCsvFile(string fileName, out string errorString)
        {
            errorString = null;
            _totalCsvChunks = 0;

            if (!File.Exists(fileName))
            {
                errorString = "The file " + fileName + " doesn't exist.";
                return;
            }

            _fileReading = new FastFileReading();

            _fileReading.csvObjectsChunkSize = CsvChunkSize;
            _fileReading.chunkPause = ChunkPause;
            _fileReading.jsonObjectsChunkSize = JsonChunkSize;

            _fileReading.CsvFileChunkIsReady += FileReading_CsvFileChunkIsReady;

            //fileReading.CsvFileReadingIsDone += FileReading_CsvFileReadingIsDone;

            //fileReading.JsonFileChunkIsReady += FileReading_JsonFileChunkIsReady;

            _fileReading.CsvFileReadingIsDone += FileReading_CsvFileReadingIsDone;

            _mainResetEvent = new ManualResetEvent(false);

            _fileReading.readingChunksFromLargeCsvFile(fileName, out errorString);

            if ((errorString?.Length ?? 0) > 0)
                Console.WriteLine("Contacts file uploading canceled:" + Environment.NewLine + errorString);
        }

        private void FileReading_CsvFileReadingIsDone(object sender, FastFileReading.CsvFileReadingIsDoneArgs e)
        {
            var isDone = e.IsDone;
            if (isDone)
            {
                Parallel.ForEach(_threadPackage,
                    new ParallelOptions {MaxDegreeOfParallelism = Environment.ProcessorCount}, CsvFileChunkIsReady);

                _threadPackage = new List<List<AddressBookContact>>();

                /*
                largeCsvFileProcessingIsDone = true;
                mainResetEvent.Set();
                if (geocodedAddressesDownloadingIsDone)
                {
                    OnGeocodingIsFinished(new GeocodingIsFinishedArgs() { isFinished = true });
                }
                */
                // fire here event for external (test) code
            }
        }

        private void FileReading_CsvFileChunkIsReady(object sender, FastFileReading.CsvFileChunkIsReadyArgs e)
        {
            _threadPackage.Add(e.multiContacts);

            if (_threadPackage.Count > 15)
            {
                Parallel.ForEach(_threadPackage,
                    new ParallelOptions {MaxDegreeOfParallelism = Environment.ProcessorCount}, CsvFileChunkIsReady);

                _threadPackage = new List<List<AddressBookContact>>();
            }

            /* This works: 30 000 contacts in 3 min
            taskList.Add(Task.Run(() => CsvFileChunkIsReady(e.multiContacts)));

            if (taskList.Count>9)
            {
                Task.WaitAll(taskList.ToArray());

                taskList = new List<Task>();
            }
            */
        }

        private void CsvFileChunkIsReady(List<AddressBookContact> contactsChunk)
        {
            var route4Me = new Route4MeManagerV5(apiKey);
            var route4MeV4 = new Route4MeManager(apiKey);

            if (DoGeocoding && contactsChunk != null)
            {
                var contactsToGeocode = new Dictionary<int, AddressBookContact>();

                if (GeocodeOnlyEmpty)
                {
                    var emptyContacts = contactsChunk.Where(x => x.CachedLat == 0 && x.CachedLng == 0);
                    if (emptyContacts != null)
                        foreach (var econt in emptyContacts)
                            contactsToGeocode.Add(contactsChunk.IndexOf(econt), econt);
                }
                else
                {
                    for (var i = 0; i < (contactsChunk?.Count ?? 0); i++) contactsToGeocode.Add(i, contactsChunk[i]);
                }

                var lsAddressesToGeocode = contactsToGeocode
                    .Select(x => x.Value)
                    .Select(x => x.Address1 +
                                 ((x?.AddressCity?.Length ?? 0) > 0 ? ", " + x.AddressCity : "") +
                                 ((x?.AddressStateId?.Length ?? 0) > 0 ? ", " + x.AddressStateId : "") +
                                 ((x?.AddressZip?.Length ?? 0) > 0 ? ", " + x.AddressZip : "") +
                                 ((x?.AddressCountryId?.Length ?? 0) > 0 ? ", " + x.AddressCountryId : "")
                    )
                    .ToList();

                var addressesToGeocode = string.Join(Environment.NewLine, lsAddressesToGeocode);

                var geoParams = new GeocodingParameters
                {
                    Addresses = addressesToGeocode,
                    ExportFormat = "json"
                };

                var geocodedAddresses = route4MeV4.BatchGeocodingAsync(geoParams, out var errorString);

                if ((geocodedAddresses?.Length ?? 0) > 50)
                {
                    var geocodedObjects =
                        JsonConvert.DeserializeObject<GeocodingResponse[]>(geocodedAddresses).ToList();

                    // If returned objects not equal to input contacts, remove with duplicated original
                    if (geocodedObjects != null && geocodedObjects.Count > contactsToGeocode.Count)
                    {
                        var dupicates = new List<GeocodingResponse>();

                        for (var i = 1; i < geocodedObjects.Count; i++)
                            if (geocodedObjects[i].Original == geocodedObjects[i - 1].Original)
                                dupicates.Add(geocodedObjects[i]);

                        foreach (var duplicate in dupicates) geocodedObjects.Remove(duplicate);
                    }

                    if (geocodedObjects != null && geocodedObjects.Count == contactsToGeocode.Count)
                    {
                        var indexList = contactsToGeocode.Keys.ToList();

                        for (var i = 0; i < geocodedObjects.Count; i++)
                        {
                            contactsChunk[indexList[i]].CachedLat = geocodedObjects[i].Lat;
                            contactsChunk[indexList[i]].CachedLng = geocodedObjects[i].Lng;
                        }
                    }
                }
            }

            var contactParams = new Route4MeManagerV5.BatchCreatingAddressBookContactsRequest
            {
                Data = contactsChunk.ToArray()
            };

            var response = route4Me.BatchCreateAdressBookContacts(
                contactParams,
                MandatoryFields,
                out var resultResponse);

            if (response?.status ?? false) _totalCsvChunks += contactsChunk.Count;

            Console.WriteLine(
                response?.status ?? false
                    ? _totalCsvChunks + " address book contacts added to database"
                    : "Faild to add " + contactsChunk.Count + " address book contacts");

            if (!(response?.status ?? false))
            {
                Console.WriteLine("Exit code: " + resultResponse.ExitCode + Environment.NewLine +
                                  "Code: " + resultResponse.Code + Environment.NewLine +
                                  "Status: " + resultResponse.Status + Environment.NewLine
                );

                foreach (var msg in resultResponse.Messages)
                    Console.WriteLine(msg.Key + ": " + string.Join(", ", msg.Value));

                Console.WriteLine("Start address: " + contactsChunk[0].Address1);
                Console.WriteLine("End address: " + contactsChunk[contactsChunk.Count - 1].Address1);
                Console.WriteLine("-------------------------------");
            }
        }

        /// <summary>
        ///     Upload JSON addresses to a temporary storage
        /// </summary>
        /// <param name="streamSource">Input stream source - file name or JSON text</param>
        /// <returns>Response object of the type uploadAddressesToTemporaryStorageResponse</returns>
        public Route4MeManager.UploadAddressesToTemporaryStorageResponse uploadAddressesToTemporaryStorage(
            string streamSource)
        {
            var route4Me = new Route4MeManager(apiKey);

            //List<AddressField> lsAddresses = readLargeJsonFileOfAddresse(sFileName);

            var jsonText = "";

            if (streamSource.Contains("{") && streamSource.Contains("}"))
                jsonText = streamSource;
            else
                jsonText = readJsonTextFromLargeJsonFileOfAddresses(streamSource);

            var errorString = "";

            var uploadResponse =
                route4Me.UploadAddressesToTemporaryStorage(jsonText, out errorString);


            if (uploadResponse == null || !uploadResponse.Status) return null;

            return uploadResponse;
        }

        /// <summary>
        ///     Geocode and download the addresses from the temporary storage.
        /// </summary>
        /// <param name="temporaryAddressesStorageID">ID of the temporary storage</param>
        /// <param name="addressesInFile">Chunk size of the addresses to be geocoded</param>
        public async void downloadGeocodedAddresses(string temporaryAddressesStorageID, int addressesInFile)
        {
            //bool done = false;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                   | SecurityProtocolType.Tls11
                                                   | SecurityProtocolType.Tls12
                                                   | (SecurityProtocolType) 12288;

            _geocodedAddressesDownloadingIsDone = false;

            _savedAddresses = new List<AddressGeocoded>();

            _temporaryAddressesStorageId = temporaryAddressesStorageID;
            if (addressesInFile != null) _requestedAddresses = addressesInFile;

            _manualResetEvent = new ManualResetEvent(false);

            var options = CreateOptions();
            options.Path = "/socket.io";
            options.Host = "validator.route4me.com/";
            options.AutoConnect = true;
            options.IgnoreServerCertificateValidation = true;
            options.Timeout = 60000;
            options.Upgrade = true;
            options.ForceJsonp = true;
            options.Transports = ImmutableList.Create(Polling.NAME, WebSocket.NAME);


            var uri = CreateUri();
            _socket = IO.Socket(uri, options);


            _socket.On("error", message =>
            {
                Debug.Print("Error -> " + message);
                //await Task.Delay(500);
                Thread.Sleep(500);
                _manualResetEvent.Set();
                _socket.Disconnect();
                //manualResetEvent.Set();
            });

            _socket.On(Socket.EVENT_ERROR, e =>
            {
                var exception = (EngineIOException) e;
                Console.WriteLine("EVENT_ERROR. " + exception.Message);
                Console.WriteLine("BASE EXCEPTION. " + exception.GetBaseException());
                Console.WriteLine("DATA COUNT. " + exception.Data.Count);
                //events.Enqueue(exception.code);
                _socket.Disconnect();
                //manager.Close();
                _manualResetEvent.Set();
                ;
            });

            _socket.On(Socket.EVENT_MESSAGE, message =>
            {
                //Debug.Print("Error -> " + message);
                //await Task.Delay(500);
                Thread.Sleep(500);
                //manualResetEvent.Set();
            });

            _socket.On("data", d =>
            {
                //Debug.Print("data -> " + d.ToString());
                //await Task.Delay(1000);
                Thread.Sleep(1000);
                //manualResetEvent.Set();
            });

            _socket.On(Socket.EVENT_CONNECT, () =>
            {
                //Debug.Print("Socket opened");
                //socket.Close();
                //await Task.Delay(500);
                Thread.Sleep(500);

                //manualResetEvent.Set();
            });

            _socket.On(Socket.EVENT_DISCONNECT, () =>
            {
                //Debug.Print("Socket disconnected");
                //socket.Close();
                //await Task.Delay(500);
                Thread.Sleep(700);

                //manualResetEvent.Set();
            });

            _socket.On(Socket.EVENT_RECONNECT_ATTEMPT, () =>
            {
                //Debug.Print("Socket reconnect attempt");
                //socket.Close();
                //await Task.Delay(1000);
                Thread.Sleep(1500);

                //manualResetEvent.Set();
            });

            _socket.On("addresses_bulk", addresses_chunk =>
            {
                //Debug.Print("addresses_chunk received");

                //await Task.Delay(500);

                var jsonChunkText = addresses_chunk.ToString();

                var errors = new List<string>();

                var jsonSettings = new JsonSerializerSettings
                {
                    Error = delegate(object sender, ErrorEventArgs args)
                    {
                        errors.Add(args.ErrorContext.Error.Message);
                        args.ErrorContext.Handled = true;
                    },
                    NullValueHandling = NullValueHandling.Ignore
                };

                var addressesChunk = JsonConvert.DeserializeObject<AddressGeocoded[]>(jsonChunkText, jsonSettings);

                if (errors.Count > 0)
                {
                    Debug.Print("Json serializer errors:");
                    foreach (var errMessage in errors) Debug.Print(errMessage);
                }

                _savedAddresses = _savedAddresses.Concat(addressesChunk).ToList();

                _loadedAddressesCount += addressesChunk.Length;

                //Debug.Print(addressesChunk.Length.ToString());

                //Debug.Print("Got chunks from websocket %s / %s", loadedAddressesCount, requestedAddresses);
                if (_loadedAddressesCount == _nextDownloadStage)
                    //Debug.Print("Downloading");
                    download(_loadedAddressesCount);

                if (_loadedAddressesCount == _requestedAddresses)
                {
                    //Debug.Print("First address:", savedAddresses[0].geocodedAddress);
                    //Debug.Print("Done, saved addresses %s", savedAddresses.Count);

                    _socket.Emit("disconnect", _temporaryAddressesStorageId);
                    _loadedAddressesCount = 0;
                    var args = new AddressesChunkGeocodedArgs {lsAddressesChunkGeocoded = _savedAddresses};
                    OnAddressesChunkGeocoded(args);

                    _manualResetEvent.Set();

                    _geocodedAddressesDownloadingIsDone = true;

                    if (_largeJsonFileProcessingIsDone)
                        OnGeocodingIsFinished(new GeocodingIsFinishedArgs {isFinished = true});

                    _socket.Close();
                }
            });

            _socket.On("geocode_progress", message =>
            {
                //Debug.Print("Progress from websocket:", message.ToString());

                var progressMessage = JsonConvert.DeserializeObject<clsProgress>(message.ToString());

                if (progressMessage.total == progressMessage.done)
                {
                    //Debug.Print("Geocoding Done, Downloading...");
                    if (_requestedAddresses == default) _requestedAddresses = progressMessage.total;
                    download(0);
                }
            });

            var jobj = new JObject();
            jobj.Add("temporary_addresses_storage_id", _temporaryAddressesStorageId);
            jobj.Add("force_restart", true);

            var _args = new List<object>();
            _args.Add(jobj);

            try
            {
                _socket.Emit("geocode", _args);
            }
            catch (Exception ex)
            {
                Debug.Print("Socket connection failed. " + ex.Message);
            }

            _manualResetEvent.WaitOne();
        }

        /// <summary>
        ///     Download chunk of the geocoded addresses
        /// </summary>
        /// <param name="start">Download addresses starting from</param>
        public void download(int start)
        {
            var bufferFailSafeMaxAddresses = 100;
            var chunkSize = (int) Math.Round((decimal) Math.Min(200, Math.Max(10, _requestedAddresses / 100)));
            var chunksLimit = (int) Math.Ceiling((decimal) (bufferFailSafeMaxAddresses / chunkSize));

            var maxAddressesToBeDownloaded = chunkSize * chunksLimit;
            _nextDownloadStage = _loadedAddressesCount + maxAddressesToBeDownloaded;

            // from_index = (chunks_limit * chunk_size);
            var jobj = new JObject();

            jobj.Add("temporary_addresses_storage_id", _temporaryAddressesStorageId);
            jobj.Add("from_index", start);
            jobj.Add("chunks_limit", chunksLimit);
            jobj.Add("chunk_size", chunkSize);

            var _args = new List<object>();
            _args.Add(jobj);
            //var data = Quobject.SocketIoClientDotNet.Parser.Packet.Args2JArray(_args);

            _socket.Emit("download", _args);
        }

        public string readJsonTextFromLargeJsonFileOfAddresses(string sFileName)
        {
            var fileRead = new FastFileReading();

            return fileRead != null ? fileRead.readJsonTextFromFile(sFileName) : string.Empty;
        }


        #region // Addresses chunk's geocoding is finished event handler

        public event EventHandler<AddressesChunkGeocodedArgs> AddressesChunkGeocoded;

        protected virtual void OnAddressesChunkGeocoded(AddressesChunkGeocodedArgs e)
        {
            var handler = AddressesChunkGeocoded;

            if (handler != null) handler(this, e);
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
            var handler = GeocodingIsFinished;

            if (handler != null) handler(this, e);
        }

        public class GeocodingIsFinishedArgs : EventArgs
        {
            public bool isFinished { get; set; }
        }

        public delegate void GeocodingIsFinishedEventHandler(object sender, AddressesChunkGeocodedArgs e);

        #endregion
    }

    /// <summary>
    ///     Response class of the received event about geocoding progress
    /// </summary>
    internal class clsProgress
    {
        public int total { get; set; }

        public int done { get; set; }
    }
}