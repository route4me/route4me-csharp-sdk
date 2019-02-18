using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using Quobject.Collections.Immutable;
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
    public class FastBulkGeocoding : Connection
    {
        private ManualResetEvent manualResetEvent = null;
        private Socket socket;
        public string Message;
        private int Number;
        private bool Flag;
        public static Connection con = new Connection();
        private int requestedAddresses;
        private int nextDownloadStage;
        private int loadedAddressesCount;
        string TEMPORARY_ADDRESSES_STORAGE_ID;

        List<AddressGeocoded> savedAddresses;

        JsonSerializer jsSer = new JsonSerializer();

        public string apiKey { get; set; }

        public FastBulkGeocoding(string ApiKey)
        {
            apiKey = ApiKey;
        }

        public Route4MeManager.uploadAddressesToTemporarryStorageResponse uploadAddressesToTemporarryStorage(string fileName)
        {
            Route4MeManager route4Me = new Route4MeManager(apiKey);

            //List<AddressField> lsAddresses = readLargeJsonFileOfAddresse(sFileName);

            string jsonText = readJsonTextFromLargeJsonFileOfAddresses(fileName);

            string errorString = "";

            Route4MeManager.uploadAddressesToTemporarryStorageResponse uploadResponse =
                route4Me.uploadAddressesToTemporarryStorage(jsonText, out errorString);


            if (uploadResponse == null || !uploadResponse.status) return null;

            return uploadResponse;
        }

        public List<AddressGeocoded> downloadGeocodedAddresses(string temporarryAddressesStorageID, int addressesInFile)
        {
            bool done = false;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
            | SecurityProtocolType.Tls11
            | SecurityProtocolType.Tls12
            | SecurityProtocolType.Ssl3;

            savedAddresses = new List<AddressGeocoded>();

            TEMPORARY_ADDRESSES_STORAGE_ID = temporarryAddressesStorageID;
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
            options.ExtraHeaders.Add("uri", "https://api.route4me.com/actions/upload/json-geocode.php?api_key=11111111111111111111111111111111");

            var uri = CreateUri();
            socket = IO.Socket(uri, options);

            manualResetEvent.Set();

            socket.On("error", async (message) =>
            {
                Console.WriteLine("Error -> " + message);
                await Task.Delay(1000);
                Thread.Sleep(2 * 1000);
                socket.Disconnect();
                manualResetEvent.Set();
            });

            socket.On(Socket.EVENT_MESSAGE, async (message) =>
            {
                Console.WriteLine("Error -> " + message);
                await Task.Delay(1000);
                Thread.Sleep(2 * 1000);
                manualResetEvent.Set();
            });

            socket.On("data", async (d) =>
            {
                Console.WriteLine("data -> " + d.ToString());
                await Task.Delay(1000);
                Thread.Sleep(2 * 1000);
                manualResetEvent.Set();
            });

            socket.On(Socket.EVENT_CONNECT, async () =>
            {
                Console.WriteLine("Socket opened");
                //socket.Close();
                await Task.Delay(1000);
                Thread.Sleep(2 * 1000);

                manualResetEvent.Set();
            });

            socket.On("addresses_bulk", (addresses_chunk) =>
            {
                Console.WriteLine("addresses_chunk received");

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

                Debug.Print("Json derializer errors:");
                foreach (string errMessage in errors) Debug.Print(errMessage);
                savedAddresses = savedAddresses.Concat(addressesChunk).ToList();

                loadedAddressesCount += addressesChunk.Length;

                Debug.Print(addressesChunk.Length.ToString());

                Debug.Print("Got chunks from websocket %s / %s", loadedAddressesCount, requestedAddresses);
                if (loadedAddressesCount == nextDownloadStage)
                {
                    Debug.Print("Downloading");
                    download(loadedAddressesCount);
                }

                if (loadedAddressesCount == requestedAddresses)
                {
                    Debug.Print("First address:", savedAddresses[0].geocodedAddress);
                    Debug.Print("Done, saved addresses %s", savedAddresses.Count);

                    socket.Emit("disconnect", TEMPORARY_ADDRESSES_STORAGE_ID);
                    done = true;
                }
            });


            socket.On("geocode_progress", (message) =>
            {

                Debug.Print("Progress from websocket:", message.ToString());
                //JsonSerializer jsSer = new JsonSerializer();
                var progressMessage = JsonConvert.DeserializeObject<clsProgress>(message.ToString());

                if (progressMessage.total == progressMessage.done)
                {
                    Debug.Print("Geocoding Done, Downloading...");
                    if (requestedAddresses == null) requestedAddresses = progressMessage.total;
                    download(0);
                }
            });

            var jobj = new JObject();
            jobj.Add("temporary_addresses_storage_id", TEMPORARY_ADDRESSES_STORAGE_ID);
            jobj.Add("force_restart", true);

            var _args = new List<object> { };
            _args.Add(jobj);

            socket.Emit("geocode", _args);
            manualResetEvent.Set();

            while (!done)
            {
                Thread.Sleep(500);
            }
            //string yn = Console.ReadLine();

            
            return savedAddresses;
            /*
            string command = "";
            while (command != "q")
            {
                command = Console.ReadLine();
                if (command == "g")
                {
                    socket.Emit("geocode", _args);
                    manualResetEvent.Set();
                }
                //socket.Emit(command);
            }
            */

            //socket.Disconnect();
        }

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

    class clsProgress
    {
        public int total { get; set; }

        public int done { get; set; }
    }
}
