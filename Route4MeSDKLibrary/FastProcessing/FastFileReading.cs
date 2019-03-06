using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using Route4MeSDK.DataTypes;
using System.Threading;

namespace Route4MeSDK.FastProcessing
{
    /// <summary>
    /// THe class for asynchronous reading large JSON file by chunks (default chunk size = 100 addresses)
    /// </summary>
    public class FastFileReading
    {
        const long offset = 0x10000000; // 256 megabytes
        const long length = 0x20000000; // 512 megabytes

        string jsonFileName;

        public int jsonObjectsChunkSize { get; set; }

        private ManualResetEvent manualResetEvent = null;

        #region // Event handler for the JsonFileChunkIsReady event
        public event EventHandler<JsonFileChunkIsReadyArgs> JsonFileChunkIsReady;

        public event EventHandler<JsonFileReadingIsDoneArgs> JsonFileReadingIsDone;

        public delegate void JsonFileChunkIsReadyEventHandler(object sender, JsonFileChunkIsReadyArgs e);

        public class JsonFileChunkIsReadyArgs : EventArgs
        {
            public string AddressesChunk { get; set; }
        }

        protected virtual void OnJsonFileChunkIsReady(JsonFileChunkIsReadyArgs e)
        {
            EventHandler<JsonFileChunkIsReadyArgs> handler = JsonFileChunkIsReady;

            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion

        #region // Event handler for the JsonFileReadingIsDone event
        protected virtual void OnJsonFileReadingIsDone(JsonFileReadingIsDoneArgs e)
        {
            EventHandler< JsonFileReadingIsDoneArgs> handler = JsonFileReadingIsDone;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        public delegate void JsonFileReadingIsDoneEventHandler(object sender, JsonFileReadingIsDoneArgs e);

        public class JsonFileReadingIsDoneArgs : EventArgs
        {
            public bool IsDone { get; set; }
        }

        #endregion

        public void fastReadFromFile(String sFileName)
        {
            if (sFileName.Substring(1, 1) != ":")
            {
                String startupPath = AppDomain.CurrentDomain.BaseDirectory;
                sFileName = startupPath + "/" + sFileName;

            }

            try
            {
                using (MemoryMappedFile memoryMappedFile = MemoryMappedFile.CreateFromFile(sFileName))
                {
                    using (MemoryMappedViewStream memoryMappedViewStream = memoryMappedFile.CreateViewStream(0, 1204, MemoryMappedFileAccess.Read))
                    {
                        var contentArray = new byte[1024];

                        memoryMappedViewStream.Read(contentArray, 0, contentArray.Length);

                        string content = Encoding.UTF8.GetString(contentArray);

                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during JSON file readinf. " + ex.Message);
            }

            
        }

        //static AutoResetEvent autoEvent = new AutoResetEvent(false);

        /// <summary>
        /// Read content from A large JSON file by chunks
        /// </summary>
        /// <param name="fileName">JSON file name</param>
        public void readingChunksFromLargeJsonFile(string fileName)
        {
            //manualResetEvent = new ManualResetEvent(false);
            //FastBulkGeocoding fbGeocoding = new FastBulkGeocoding("");

            //fbGeocoding.GeocodingIsFinished += FbGeocoding_GeocodingIsFinished;

            //manualResetEvent.WaitOne();

            JsonSerializer serializer = new JsonSerializer();

            AddressField o = null;

            string sJsonAddressesChunk = "";
            int curJsonObjects = 0;
            using (FileStream s = File.Open(fileName, FileMode.Open))
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                //reader.SupportMultipleContent = true;
                bool blStartAdresses = false;
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartArray) blStartAdresses = true;

                    if (reader.TokenType == JsonToken.StartObject && blStartAdresses)
                    {
                        o = serializer.Deserialize<AddressField>(reader);

                        if (o.Address == null) continue;

                        sJsonAddressesChunk += JsonConvert.SerializeObject(o, Formatting.None) + ",";
                        curJsonObjects++;

                        if (curJsonObjects >= jsonObjectsChunkSize)
                        {
                            sJsonAddressesChunk = "{\"rows\":[" + sJsonAddressesChunk.TrimEnd(',') + "]}";
                            JsonFileChunkIsReadyArgs chunkIsReady = new JsonFileChunkIsReadyArgs();
                            chunkIsReady.AddressesChunk = sJsonAddressesChunk;
                            sJsonAddressesChunk = "";
                            curJsonObjects = 0;

                            //manualResetEvent.Set();
                            OnJsonFileChunkIsReady(chunkIsReady);
                            Thread.Sleep(5000);
                            //manualResetEvent.WaitOne();
                        }
                    }
                }

                if (sJsonAddressesChunk != "")
                {
                    sJsonAddressesChunk = "{\"rows\":[" + sJsonAddressesChunk.TrimEnd(',') + "]}";
                    JsonFileChunkIsReadyArgs chunkIsReady = new JsonFileChunkIsReadyArgs();
                    chunkIsReady.AddressesChunk = sJsonAddressesChunk;
                    sJsonAddressesChunk = "";
                    OnJsonFileChunkIsReady(chunkIsReady);

                    System.Threading.Thread.Sleep(5000);
                }

                JsonFileReadingIsDoneArgs args = new JsonFileReadingIsDoneArgs() { IsDone = true };
                OnJsonFileReadingIsDone(args);

            }
        }

        private void FbGeocoding_GeocodingIsFinished(object sender, FastBulkGeocoding.GeocodingIsFinishedArgs e)
        {
            //manualResetEvent.Set();
        }

        public string readJsonTextFromFile(String sFileName)
        {
            if (!File.Exists(sFileName))
            {
                Console.WriteLine("The file " + sFileName + " doesn't exist..."); return "";
            }

            string jsonContent = File.ReadAllText(sFileName);

            return jsonContent;
        }

        public List<T> getObjectsListfromJsonText<T>(string jsonText)
        {
            List<T> lsObjects = fastJSON.JSON.ToObject<List<T>>(jsonText);

            return lsObjects;
        }

        public List<T> getObjectsListFromJsonFile<T>(String sFileName)
        {
            String jsonText = readJsonTextFromFile(sFileName);

            List<T> lsObjects = getObjectsListfromJsonText<T>(jsonText);

            return lsObjects;
        }

    }
}
