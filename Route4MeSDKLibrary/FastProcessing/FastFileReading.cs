﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.MemoryMappedFiles;
using Newtonsoft.Json;
using Route4MeSDK.DataTypes;
using System.Threading;
using CsvHelper;
using System.Text.RegularExpressions;

namespace Route4MeSDK.FastProcessing
{
    /// <summary>
    /// THe class for asynchronous reading large JSON file by chunks (default chunk size = 100 addresses)
    /// </summary>
    public class FastFileReading
    {
        //const long offset = 0x10000000; // 256 megabytes
        //const long length = 0x20000000; // 512 megabytes

        //string jsonFileName;
        //string csvFileName;

        public int chunkPause { get; set; } = 2000;

        public int jsonObjectsChunkSize { get; set; } = 300;
        public int csvObjectsChunkSize { get; set; } = 300;

        public static Dictionary<string, string> csvAddressMapping { get; set; }

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
            EventHandler<JsonFileReadingIsDoneArgs> handler = JsonFileReadingIsDone;

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

        #region // Event handler for the CsvFileChunkIsReady

        public event EventHandler<CsvFileChunkIsReadyArgs> CsvFileChunkIsReady;

        public event EventHandler<CsvFileReadingIsDoneArgs> CsvFileReadingIsDone;

        public delegate void CsvFileChunkIsReadyEventHandler(object sender, CsvFileChunkIsReadyArgs e);

        public class CsvFileChunkIsReadyArgs : EventArgs
        {
            public string AddressesChunk { get; set; }

            public List<DataTypes.V5.AddressBookContact> multiContacts { get; set; }
        }

        protected virtual void OnCsvFileChunkIsReady(CsvFileChunkIsReadyArgs e)
        {
            EventHandler<CsvFileChunkIsReadyArgs> handler = CsvFileChunkIsReady;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #region // Event handler for the CsvFileReadingIsDone event
        protected virtual void OnCsvFileReadingIsDone(CsvFileReadingIsDoneArgs e)
        {
            EventHandler<CsvFileReadingIsDoneArgs> handler = CsvFileReadingIsDone;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        public delegate void CsvFileReadingIsDoneEventHandler(object sender, CsvFileReadingIsDoneArgs e);

        public class CsvFileReadingIsDoneArgs : EventArgs
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
                            Thread.Sleep(chunkPause);
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

                    System.Threading.Thread.Sleep(chunkPause);
                }

                JsonFileReadingIsDoneArgs args = new JsonFileReadingIsDoneArgs() { IsDone = true };
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
            int curJsonObjects = 0;
            //string sJsonAddressesChunk = "";
            //var serializer = new JsonSerializer();

            string wrongCoordPattern = @"^\-{0,1}\d{1,3}\.\d$"; // TO DO: API 5 contacts Batch uploading recognizes numbers with only one digit after point as wrong

            var lsMultiContacts = new List<DataTypes.V5.AddressBookContact>();

            using (TextReader reader = File.OpenText(fileName))
            {
                var csv = new CsvReader(reader);

                csv.ReadHeader();
                string[] csvHeaders = csv.FieldHeaders.Where(x => x.Length > 0).ToArray();

                foreach (var csvHeader in csvHeaders)
                {
                    if (!csvAddressMapping.ContainsKey(csvHeader))
                    {
                        errorString = "CSV file header " + csvHeader + " is not specified in the CSV address mapping.";
                        return;
                    }
                }

                foreach (string k1 in csvAddressMapping.Keys)
                {
                    if (!csvHeaders.Contains(k1))
                    {
                        errorString = "The CSV address mapping key " + k1 + " is not found in the CSV header";
                        return;
                    }
                }

                while (csv.Read())
                {
                    var abContact = new DataTypes.V5.AddressBookContact();

                    foreach (var csvHeader in csvHeaders)
                    {
                        int fieldIndex = Array.IndexOf(csvHeaders, csvHeader);
                        var fieldValue = csv.GetField(fieldIndex);
                        object oFieldValue = (object)fieldValue;

                        if ((fieldValue?.Length ?? 0) > 0)
                        {
                            var propinfo = abContact.GetType().GetProperty(csvAddressMapping[csvHeader]);
                            //string fieldType = propinfo.PropertyType.FullName;

                            //if (Nullable.GetUnderlyingType(propinfo.PropertyType) != null)
                            //{
                            //    fieldType = Nullable.GetUnderlyingType(propinfo.PropertyType).Name;
                            //}

                            string fieldType = Nullable.GetUnderlyingType(propinfo.PropertyType) != null
                                ? Nullable.GetUnderlyingType(propinfo.PropertyType).Name
                                : propinfo.PropertyType.Name;

                            switch (fieldType)
                            {
                                case "String":
                                    if (csvAddressMapping[csvHeader] == "AddressAlias" && fieldValue.ToString().Length > 59)
                                        fieldValue = fieldValue.ToString().Substring(0, 59);
                                    if (csvAddressMapping[csvHeader] == "AddressZip" && fieldValue.ToString().Length > 6)
                                        fieldValue = fieldValue.ToString().Substring(0, 5);

                                    abContact
                                        .GetType()
                                        .GetProperty(csvAddressMapping[csvHeader])
                                        .SetValue(abContact, fieldValue);
                                    break;
                                case "Int32":
                                    int? intVal = R4MeUtils.ConvertObjectToType<Int32>(ref oFieldValue);

                                    if (intVal != null)
                                    {
                                        abContact
                                            .GetType()
                                            .GetProperty(csvAddressMapping[csvHeader])
                                            .SetValue(abContact, intVal);
                                    }
                                    else
                                    {
                                        Console.WriteLine("The field " + csvHeader + " of the address book contact " + abContact.address_1 + " ommited");
                                    }
                                    break;
                                case "Int64":
                                    if ((new string[]
                                    {
                                        "LocalTimeWindowStart", "LocalTimeWindowEnd",
                                        "LocalTimeWindowStart2", "LocalTimeWindowEnd2"
                                    }).Contains(csvAddressMapping[csvHeader]) && fieldValue.Contains(":"))
                                    {
                                        string hmValue = fieldValue.Length < 5 ? "0" + fieldValue : fieldValue;
                                        hmValue = hmValue.Length == 7 ? "0" + hmValue : hmValue;
                                        hmValue = hmValue.Length < 8 ? "00:" + hmValue : hmValue;

                                        var secValue = R4MeUtils.DDHHMM2Seconds(hmValue, out string errorString2);

                                        long? seconds = secValue != null ? (long)secValue : default(long);

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
                                    {
                                        abContact
                                            .GetType()
                                            .GetProperty(csvAddressMapping[csvHeader])
                                            .SetValue(abContact, longVal);

                                        continue;
                                    }
                                    break;
                                case "Double":
                                    if ((new string[]
                                    {
                                        "CachedLat","CachedLng","CurbsideLat","CurbsideLng"
                                    }).Contains(csvAddressMapping[csvHeader]))
                                    {
                                        var problematic = Regex.IsMatch(fieldValue, wrongCoordPattern);
                                        if (problematic)
                                        {
                                            fieldValue += "0000001";
                                            oFieldValue = (object)fieldValue;
                                        }
                                    }

                                    double? dbVal = R4MeUtils.ConvertObjectToType<double>(ref oFieldValue);

                                    if (dbVal != null)
                                    {
                                        abContact
                                            .GetType()
                                            .GetProperty(csvAddressMapping[csvHeader])
                                            .SetValue(abContact, dbVal);
                                    }
                                    else
                                    {
                                        Console.WriteLine("The field " + csvHeader + " of the address book contact " + abContact.address_1 + " ommited");
                                    }
                                    break;
                                case "Boolean":
                                    bool? blVal = R4MeUtils.ConvertObjectToType<bool>(ref oFieldValue);

                                    if (blVal != null)
                                    {
                                        abContact
                                            .GetType()
                                            .GetProperty(csvAddressMapping[csvHeader])
                                            .SetValue(abContact, blVal);
                                    }
                                    else
                                    {
                                        Console.WriteLine("The field " + csvHeader + " of the address book contact " + abContact.address_1 + " ommited");
                                    }
                                    break;
                                default:
                                    if (fieldType == "Object" && propinfo.Name == "address_custom_data")
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

                    if (!csvAddressMapping.Values.Contains("AddressStopType")) abContact.AddressStopType = AddressStopType.Delivery.Description();

                    lsMultiContacts.Add(abContact);

                    curJsonObjects++;

                    if (curJsonObjects >= csvObjectsChunkSize)
                    {
                        CsvFileChunkIsReadyArgs chunkIsReady = new CsvFileChunkIsReadyArgs();
                        chunkIsReady.multiContacts = lsMultiContacts;
                        curJsonObjects = 0;

                        OnCsvFileChunkIsReady(chunkIsReady);
                        Thread.Sleep(chunkPause);

                        lsMultiContacts = new List<DataTypes.V5.AddressBookContact>();
                    }

                }

                if (lsMultiContacts.Count > 0)
                {
                    CsvFileChunkIsReadyArgs chunkIsReady = new CsvFileChunkIsReadyArgs();
                    chunkIsReady.multiContacts = lsMultiContacts;

                    OnCsvFileChunkIsReady(chunkIsReady);

                    System.Threading.Thread.Sleep(chunkPause);

                    lsMultiContacts = new List<DataTypes.V5.AddressBookContact>();
                }

                CsvFileReadingIsDoneArgs args = new CsvFileReadingIsDoneArgs() { IsDone = true };

                OnCsvFileReadingIsDone(args);
            }
        }

        public void readingChunksFromLargeCsvFileOld(string fileName, out string errorString)
        {
            errorString = null;
            int curJsonObjects = 0;
            string sJsonAddressesChunk = "";
            var serializer = new JsonSerializer();

            using (TextReader reader = File.OpenText(fileName))
            {
                var csv = new CsvReader(reader);

                csv.ReadHeader();
                string[] csvHeaders = csv.FieldHeaders;

                foreach (var csvHeader in csvHeaders)
                {
                    if (!csvAddressMapping.ContainsKey(csvHeader))
                    {
                        errorString = "CSV file header " + csvHeader + " is not specified in the CSV address mapping.";
                        return;
                    }
                }

                foreach (string k1 in csvAddressMapping.Keys)
                {
                    if (!csvHeaders.Contains(k1))
                    {
                        errorString = "The CSV address mapping key " + k1 + " is not found in the CSV header";
                        return;
                    }
                }

                while (csv.Read())
                {
                    var abContact = new AddressBookContact();

                    foreach (var csvHeader in csvHeaders)
                    {
                        int fieldIndex = Array.IndexOf(csvHeaders, csvHeader);
                        var fieldValue = csv.GetField(fieldIndex);

                        if (fieldValue != null)
                        {
                            string fieldType = abContact.GetType().GetProperty(csvAddressMapping[csvHeader]).PropertyType.Name;

                            switch (fieldType)
                            {
                                case "String":
                                    abContact
                                        .GetType()
                                        .GetProperty(csvAddressMapping[csvHeader])
                                        .SetValue(abContact, fieldValue);
                                    break;
                                case "Int32":
                                    if (Int32.TryParse(fieldValue.ToString(), out Int32 __32))
                                    {
                                        abContact
                                            .GetType()
                                            .GetProperty(csvAddressMapping[csvHeader])
                                            .SetValue(abContact, Convert.ToInt32(fieldValue));
                                    }
                                    break;
                                case "Int64":
                                    if (Int64.TryParse(fieldValue.ToString(), out Int64 __64))
                                    {
                                        abContact
                                            .GetType()
                                            .GetProperty(csvAddressMapping[csvHeader])
                                            .SetValue(abContact, Convert.ToInt64(fieldValue));
                                    }
                                    break;
                                case "Double":
                                    if (Double.TryParse(fieldValue.ToString(), out double __d))
                                    {
                                        abContact
                                            .GetType()
                                            .GetProperty(csvAddressMapping[csvHeader])
                                            .SetValue(abContact, Convert.ToDouble(fieldValue));
                                    }
                                    break;
                                case "Boolean":
                                    if (Boolean.TryParse(fieldValue.ToString(), out bool __b))
                                    {
                                        abContact
                                            .GetType()
                                            .GetProperty(csvAddressMapping[csvHeader])
                                            .SetValue(abContact, Convert.ToBoolean(fieldValue));
                                    }
                                    break;
                                default:

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
                        CsvFileChunkIsReadyArgs chunkIsReady = new CsvFileChunkIsReadyArgs();
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
                    CsvFileChunkIsReadyArgs chunkIsReady = new CsvFileChunkIsReadyArgs();
                    chunkIsReady.AddressesChunk = sJsonAddressesChunk;
                    sJsonAddressesChunk = "";
                    //OnJsonFileChunkIsReady(chunkIsReady);
                    OnCsvFileChunkIsReady(chunkIsReady);

                    System.Threading.Thread.Sleep(5000);
                }

                //JsonFileReadingIsDoneArgs args = new JsonFileReadingIsDoneArgs() { IsDone = true };
                CsvFileReadingIsDoneArgs args = new CsvFileReadingIsDoneArgs() { IsDone = true };
                //OnJsonFileReadingIsDone(args);
                OnCsvFileReadingIsDone(args);
            }
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

