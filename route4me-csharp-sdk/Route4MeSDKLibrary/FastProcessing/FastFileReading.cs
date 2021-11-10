using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using CsvHelper;
using fastJSON;
using Newtonsoft.Json;
using Route4MeSDK.DataTypes;
using AddressBookContact = Route4MeSDK.DataTypes.V5.AddressBookContact;

namespace Route4MeSDK.FastProcessing
{
    /// <summary>
    ///     THe class for asynchronous reading large JSON file by chunks (default chunk size = 100 addresses)
    /// </summary>
    public class FastFileReading
    {
        private const long offset = 0x10000000; // 256 megabytes
        private const long length = 0x20000000; // 512 megabytesv

        public int chunkPause { get; set; } = 2000;

        public int jsonObjectsChunkSize { get; set; } = 300;
        public int csvObjectsChunkSize { get; set; } = 300;

        public static Dictionary<string, string> csvAddressMapping { get; set; }

        public void fastReadFromFile(string sFileName)
        {
            if (sFileName.Substring(1, 1) != ":")
            {
                var startupPath = AppDomain.CurrentDomain.BaseDirectory;
                sFileName = startupPath + "/" + sFileName;
            }

            try
            {
                using (var memoryMappedFile = MemoryMappedFile.CreateFromFile(sFileName))
                {
                    using (var memoryMappedViewStream =
                        memoryMappedFile.CreateViewStream(0, 1204, MemoryMappedFileAccess.Read))
                    {
                        var contentArray = new byte[1024];

                        memoryMappedViewStream.Read(contentArray, 0, contentArray.Length);

                        var content = Encoding.UTF8.GetString(contentArray);
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
        ///     Read content from A large JSON file by chunks
        /// </summary>
        /// <param name="fileName">JSON file name</param>
        public void readingChunksFromLargeJsonFile(string fileName)
        {
            //manualResetEvent = new ManualResetEvent(false);
            //FastBulkGeocoding fbGeocoding = new FastBulkGeocoding("");

            //fbGeocoding.GeocodingIsFinished += FbGeocoding_GeocodingIsFinished;

            //manualResetEvent.WaitOne();

            var serializer = new JsonSerializer();

            AddressField o = null;

            var sJsonAddressesChunk = "";
            var curJsonObjects = 0;
            using (var s = File.Open(fileName, FileMode.Open))
            using (var sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                //reader.SupportMultipleContent = true;
                var blStartAdresses = false;
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
                            var chunkIsReady = new JsonFileChunkIsReadyArgs();
                            chunkIsReady.AddressesChunk = sJsonAddressesChunk;
                            sJsonAddressesChunk = "";
                            curJsonObjects = 0;

                            //manualResetEvent.Set();
                            OnJsonFileChunkIsReady(chunkIsReady);
                            Thread.Sleep(chunkPause);
                            //manualResetEvent.WaitOne();
                        }
                    }
                }

                if (sJsonAddressesChunk != "")
                {
                    sJsonAddressesChunk = "{\"rows\":[" + sJsonAddressesChunk.TrimEnd(',') + "]}";
                    var chunkIsReady = new JsonFileChunkIsReadyArgs();
                    chunkIsReady.AddressesChunk = sJsonAddressesChunk;
                    sJsonAddressesChunk = "";
                    OnJsonFileChunkIsReady(chunkIsReady);

                    Thread.Sleep(chunkPause);
                }

                var args = new JsonFileReadingIsDoneArgs {IsDone = true};
                OnJsonFileReadingIsDone(args);
            }
        }

        private void FbGeocoding_GeocodingIsFinished(object sender, FastBulkGeocoding.GeocodingIsFinishedArgs e)
        {
            //manualResetEvent.Set();
        }

        public void readingChunksFromLargeCsvFile(string fileName, out string errorString)
        {
            errorString = null;
            var curJsonObjects = 0;
            //string sJsonAddressesChunk = "";
            //var serializer = new JsonSerializer();

            var wrongCoordPattern =
                @"^\-{0,1}\d{1,3}\.\d$"; // TO DO: API 5 contacts Batch uploading recognizes numbers with only one digit after point as wrong

            var lsMultiContacts = new List<AddressBookContact>();

            using (TextReader reader = File.OpenText(fileName))
            {
                var csv = new CsvReader(reader);

                csv.ReadHeader();
                var csvHeaders = csv.FieldHeaders.Where(x => x.Length > 0).ToArray();

                foreach (var csvHeader in csvHeaders)
                    if (!csvAddressMapping.ContainsKey(csvHeader))
                    {
                        errorString = "CSV file header " + csvHeader + " is not specified in the CSV address mapping.";
                        return;
                    }

                foreach (var k1 in csvAddressMapping.Keys)
                    if (!csvHeaders.Contains(k1))
                    {
                        errorString = "The CSV address mapping key " + k1 + " is not found in the CSV header";
                        return;
                    }

                while (csv.Read())
                {
                    var abContact = new AddressBookContact();

                    foreach (var csvHeader in csvHeaders)
                    {
                        var fieldIndex = Array.IndexOf(csvHeaders, csvHeader);
                        var fieldValue = csv.GetField(fieldIndex);
                        object oFieldValue = fieldValue;

                        if ((fieldValue?.Length ?? 0) > 0)
                        {
                            var propinfo = abContact.GetType().GetProperty(csvAddressMapping[csvHeader]);
                            //string fieldType = propinfo.PropertyType.FullName;

                            //if (Nullable.GetUnderlyingType(propinfo.PropertyType) != null)
                            //{
                            //    fieldType = Nullable.GetUnderlyingType(propinfo.PropertyType).Name;
                            //}

                            var fieldType = Nullable.GetUnderlyingType(propinfo.PropertyType) != null
                                ? Nullable.GetUnderlyingType(propinfo.PropertyType).Name
                                : propinfo.PropertyType.Name;

                            switch (fieldType)
                            {
                                case "String":
                                    if (csvAddressMapping[csvHeader] == "AddressAlias" && fieldValue.Length > 59)
                                        fieldValue = fieldValue.Substring(0, 59);
                                    if (csvAddressMapping[csvHeader] == "AddressZip" && fieldValue.Length > 6)
                                        fieldValue = fieldValue.Substring(0, 5);

                                    abContact
                                        .GetType()
                                        .GetProperty(csvAddressMapping[csvHeader])
                                        .SetValue(abContact, fieldValue);
                                    break;
                                case "Int32":
                                    int? intVal = R4MeUtils.ConvertObjectToType<int>(ref oFieldValue);

                                    if (intVal != null)
                                        abContact
                                            .GetType()
                                            .GetProperty(csvAddressMapping[csvHeader])
                                            .SetValue(abContact, intVal);
                                    else
                                        Console.WriteLine("The field " + csvHeader + " of the address book contact " +
                                                          abContact.Address1 + " ommited");
                                    break;
                                case "Int64":
                                    if (new[]
                                    {
                                        "LocalTimeWindowStart", "LocalTimeWindowEnd",
                                        "LocalTimeWindowStart2", "LocalTimeWindowEnd2"
                                    }.Contains(csvAddressMapping[csvHeader]) && fieldValue.Contains(":"))
                                    {
                                        var hmValue = fieldValue.Length < 5 ? "0" + fieldValue : fieldValue;
                                        hmValue = hmValue.Length == 7 ? "0" + hmValue : hmValue;
                                        hmValue = hmValue.Length < 8 ? "00:" + hmValue : hmValue;

                                        var secValue = R4MeUtils.DDHHMM2Seconds(hmValue, out var errorString2);

                                        long? seconds = secValue != null ? (long) secValue : default;

                                        if (seconds != null)
                                            abContact
                                                .GetType()
                                                .GetProperty(csvAddressMapping[csvHeader])
                                                .SetValue(abContact, seconds);

                                        continue;
                                    }

                                    long? longVal = R4MeUtils.ConvertObjectToType<long>(ref oFieldValue);

                                    if (csvAddressMapping[csvHeader] == "ServiceTime") longVal = longVal * 60;

                                    if (longVal != null)
                                        abContact
                                            .GetType()
                                            .GetProperty(csvAddressMapping[csvHeader])
                                            .SetValue(abContact, longVal);
                                    break;
                                case "Double":
                                    if (new[]
                                    {
                                        "CachedLat", "CachedLng", "CurbsideLat", "CurbsideLng"
                                    }.Contains(csvAddressMapping[csvHeader]))
                                    {
                                        var problematic = Regex.IsMatch(fieldValue, wrongCoordPattern);
                                        if (problematic)
                                        {
                                            fieldValue += "0000001";
                                            oFieldValue = fieldValue;
                                        }
                                    }

                                    double? dbVal = R4MeUtils.ConvertObjectToType<double>(ref oFieldValue);

                                    if (dbVal != null)
                                        abContact
                                            .GetType()
                                            .GetProperty(csvAddressMapping[csvHeader])
                                            .SetValue(abContact, dbVal);
                                    else
                                        Console.WriteLine("The field " + csvHeader + " of the address book contact " +
                                                          abContact.Address1 + " ommited");
                                    break;
                                case "Boolean":
                                    bool? blVal = R4MeUtils.ConvertObjectToType<bool>(ref oFieldValue);

                                    if (blVal != null)
                                        abContact
                                            .GetType()
                                            .GetProperty(csvAddressMapping[csvHeader])
                                            .SetValue(abContact, blVal);
                                    else
                                        Console.WriteLine("The field " + csvHeader + " of the address book contact " +
                                                          abContact.Address1 + " ommited");
                                    break;
                                default:
                                    if (fieldType == "Object" && propinfo.Name == "AddressCustomData")
                                    {
                                        var customData = JsonConvert
                                            .DeserializeObject<Dictionary<string, string>>(oFieldValue.ToString());

                                        abContact
                                            .GetType()
                                            .GetProperty(csvAddressMapping[csvHeader])
                                            .SetValue(abContact, customData);
                                    }

                                    break;
                            }
                        }
                    }

                    if (!csvAddressMapping.Values.Contains("AddressStopType"))
                        abContact.AddressStopType = AddressStopType.Delivery.Description();

                    lsMultiContacts.Add(abContact);

                    curJsonObjects++;

                    if (curJsonObjects >= csvObjectsChunkSize)
                    {
                        var chunkIsReady = new CsvFileChunkIsReadyArgs();
                        chunkIsReady.multiContacts = lsMultiContacts;
                        curJsonObjects = 0;

                        OnCsvFileChunkIsReady(chunkIsReady);
                        Thread.Sleep(chunkPause);

                        lsMultiContacts = new List<AddressBookContact>();
                    }
                }

                if (lsMultiContacts.Count > 0)
                {
                    var chunkIsReady = new CsvFileChunkIsReadyArgs();
                    chunkIsReady.multiContacts = lsMultiContacts;

                    OnCsvFileChunkIsReady(chunkIsReady);

                    Thread.Sleep(chunkPause);

                    lsMultiContacts = new List<AddressBookContact>();
                }

                var args = new CsvFileReadingIsDoneArgs {IsDone = true};

                OnCsvFileReadingIsDone(args);
            }
        }

        public void readingChunksFromLargeCsvFileOld(string fileName, out string errorString)
        {
            errorString = null;
            var curJsonObjects = 0;
            var sJsonAddressesChunk = "";
            var serializer = new JsonSerializer();

            using (TextReader reader = File.OpenText(fileName))
            {
                var csv = new CsvReader(reader);

                csv.ReadHeader();
                var csvHeaders = csv.FieldHeaders;

                foreach (var csvHeader in csvHeaders)
                    if (!csvAddressMapping.ContainsKey(csvHeader))
                    {
                        errorString = "CSV file header " + csvHeader + " is not specified in the CSV address mapping.";
                        return;
                    }

                foreach (var k1 in csvAddressMapping.Keys)
                    if (!csvHeaders.Contains(k1))
                    {
                        errorString = "The CSV address mapping key " + k1 + " is not found in the CSV header";
                        return;
                    }

                while (csv.Read())
                {
                    var abContact = new DataTypes.AddressBookContact();

                    foreach (var csvHeader in csvHeaders)
                    {
                        var fieldIndex = Array.IndexOf(csvHeaders, csvHeader);
                        var fieldValue = csv.GetField(fieldIndex);

                        if (fieldValue != null)
                        {
                            var fieldType = abContact.GetType().GetProperty(csvAddressMapping[csvHeader]).PropertyType
                                .Name;

                            switch (fieldType)
                            {
                                case "String":
                                    abContact
                                        .GetType()
                                        .GetProperty(csvAddressMapping[csvHeader])
                                        .SetValue(abContact, fieldValue);
                                    break;
                                case "Int32":
                                    if (int.TryParse(fieldValue, out var __32))
                                        abContact
                                            .GetType()
                                            .GetProperty(csvAddressMapping[csvHeader])
                                            .SetValue(abContact, Convert.ToInt32(fieldValue));
                                    break;
                                case "Int64":
                                    if (long.TryParse(fieldValue, out var __64))
                                        abContact
                                            .GetType()
                                            .GetProperty(csvAddressMapping[csvHeader])
                                            .SetValue(abContact, Convert.ToInt64(fieldValue));
                                    break;
                                case "Double":
                                    if (double.TryParse(fieldValue, out var __d))
                                        abContact
                                            .GetType()
                                            .GetProperty(csvAddressMapping[csvHeader])
                                            .SetValue(abContact, Convert.ToDouble(fieldValue));
                                    break;
                                case "Boolean":
                                    if (bool.TryParse(fieldValue, out var __b))
                                        abContact
                                            .GetType()
                                            .GetProperty(csvAddressMapping[csvHeader])
                                            .SetValue(abContact, Convert.ToBoolean(fieldValue));
                                    break;
                            }
                        }
                    }

                    sJsonAddressesChunk += JsonConvert.SerializeObject(abContact, Formatting.None) + ",";
                    curJsonObjects++;

                    if (curJsonObjects >= jsonObjectsChunkSize)
                    {
                        sJsonAddressesChunk = "{\"rows\":[" + sJsonAddressesChunk.TrimEnd(',') + "]}";
                        //JsonFileChunkIsReadyArgs chunkIsReady = new JsonFileChunkIsReadyArgs();
                        var chunkIsReady = new CsvFileChunkIsReadyArgs();
                        chunkIsReady.AddressesChunk = sJsonAddressesChunk;
                        sJsonAddressesChunk = "";
                        curJsonObjects = 0;

                        //manualResetEvent.Set();
                        //OnJsonFileChunkIsReady(chunkIsReady);
                        OnCsvFileChunkIsReady(chunkIsReady);
                        Thread.Sleep(5000);
                        //manualResetEvent.WaitOne();
                    }
                }

                if (sJsonAddressesChunk != "")
                {
                    sJsonAddressesChunk = "{\"rows\":[" + sJsonAddressesChunk.TrimEnd(',') + "]}";
                    //JsonFileChunkIsReadyArgs chunkIsReady = new JsonFileChunkIsReadyArgs();
                    var chunkIsReady = new CsvFileChunkIsReadyArgs();
                    chunkIsReady.AddressesChunk = sJsonAddressesChunk;
                    sJsonAddressesChunk = "";
                    //OnJsonFileChunkIsReady(chunkIsReady);
                    OnCsvFileChunkIsReady(chunkIsReady);

                    Thread.Sleep(5000);
                }

                //JsonFileReadingIsDoneArgs args = new JsonFileReadingIsDoneArgs() { IsDone = true };
                var args = new CsvFileReadingIsDoneArgs {IsDone = true};
                //OnJsonFileReadingIsDone(args);
                OnCsvFileReadingIsDone(args);
            }
        }


        public string readJsonTextFromFile(string sFileName)
        {
            if (!File.Exists(sFileName))
            {
                Console.WriteLine("The file " + sFileName + " doesn't exist...");
                return "";
            }

            var jsonContent = File.ReadAllText(sFileName);

            return jsonContent;
        }

        public List<T> getObjectsListfromJsonText<T>(string jsonText)
        {
            var lsObjects = JSON.ToObject<List<T>>(jsonText);

            return lsObjects;
        }

        public List<T> getObjectsListFromJsonFile<T>(string sFileName)
        {
            var jsonText = readJsonTextFromFile(sFileName);

            var lsObjects = getObjectsListfromJsonText<T>(jsonText);

            return lsObjects;
        }

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
            var handler = JsonFileChunkIsReady;

            if (handler != null) handler(this, e);
        }

        #endregion

        #region // Event handler for the JsonFileReadingIsDone event

        protected virtual void OnJsonFileReadingIsDone(JsonFileReadingIsDoneArgs e)
        {
            var handler = JsonFileReadingIsDone;

            if (handler != null) handler(this, e);
        }

        public delegate void JsonFileReadingIsDoneEventHandler(object sender, JsonFileReadingIsDoneArgs e);

        public class JsonFileReadingIsDoneArgs : EventArgs
        {
            public bool IsDone { get; set; }
        }

        #endregion

        #region // Event handler for the CsvFileChunkIsReady

        public event EventHandler<CsvFileChunkIsReadyArgs> CsvFileChunkIsReady;

        public event EventHandler<CsvFileReadingIsDoneArgs> CsvFileReadingIsDone;

        public delegate void CsvFileChunkIsReadyEventHandler(object sender, CsvFileChunkIsReadyArgs e);

        public class CsvFileChunkIsReadyArgs : EventArgs
        {
            public string AddressesChunk { get; set; }

            public List<AddressBookContact> multiContacts { get; set; }
        }

        protected virtual void OnCsvFileChunkIsReady(CsvFileChunkIsReadyArgs e)
        {
            var handler = CsvFileChunkIsReady;

            if (handler != null) handler(this, e);
        }

        #endregion

        #region // Event handler for the CsvFileReadingIsDone event

        protected virtual void OnCsvFileReadingIsDone(CsvFileReadingIsDoneArgs e)
        {
            var handler = CsvFileReadingIsDone;

            if (handler != null) handler(this, e);
        }

        public delegate void CsvFileReadingIsDoneEventHandler(object sender, CsvFileReadingIsDoneArgs e);

        public class CsvFileReadingIsDoneArgs : EventArgs
        {
            public bool IsDone { get; set; }
        }

        #endregion
    }
}