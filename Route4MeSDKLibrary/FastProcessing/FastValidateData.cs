using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Route4MeSDK.FastProcessing
{
    public class FastValidateData
    {
        private ManualResetEvent manualResetEvent = null;
        private ManualResetEvent mainResetEvent = null;
        //private Socket socket;
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

        bool largeCsvFileProcessingIsDone;
        bool uploadContactsIsDone;

        int totalCsvChunks;

        JsonSerializer jsSer = new JsonSerializer();

        public string apiKey { get; set; }

        public int CsvChankSize { get; set; } = 300;
        public int JsonChankSize { get; set; } = 300;
        public int ChankPause { get; set; } = 2000;

        static List<Task> taskList;

        static List<List<DataTypes.V5.AddressBookContact>> threadPackage;

        public string[] MandatoryFields { get; set; }

        /// <summary>
        /// If true the messages is written in the console.
        /// </summary>
        public Boolean ConsoleWriteMessage { get; set; } = false;

        public FastValidateData(string ApiKey, bool EnableTraceSource = false)
        {
            if (ApiKey != "") apiKey = ApiKey;
            //Quobject.SocketIoClientDotNet.TraceSourceTools.LogTraceSource.TraceSourceLogging(EnableTraceSource);

            taskList = new List<Task>();
            threadPackage = new List<List<DataTypes.V5.AddressBookContact>>();
        }

        /// <summary>
        /// Read and validate CSV file with large address book contacts.
        /// </summary>
        /// <param name="fileName">CSV file name</param>
        /// <param name="errorString">Error string</param>
        public void readLargeContactsCsvFile(string fileName, out string errorString)
        {
            errorString = "";
            totalCsvChunks = 0;

            if (!File.Exists(fileName))
            {
                errorString = "The file " + fileName + " doesn't exist.";
                return;
            }

            var route4Me = new Route4MeManager(apiKey);

            largeCsvFileProcessingIsDone = false;

            fileReading = new FastFileReading();

            fileReading.csvObjectsChunkSize = CsvChankSize;
            fileReading.chunkPause = ChankPause;
            fileReading.jsonObjectsChunkSize = JsonChankSize;

            fileReading.CsvFileChunkIsReady += FileReading_CsvFileChunkIsReady;

            fileReading.CsvFileReadingIsDone += FileReading_CsvFileReadingIsDone;

            mainResetEvent = new ManualResetEvent(false);

            fileReading.readingChunksFromLargeCsvFile(fileName, out errorString);
        }

        private void FileReading_CsvFileReadingIsDone(object sender, FastFileReading.CsvFileReadingIsDoneArgs e)
        {
            Parallel.ForEach(threadPackage, chunk =>
            {
                CsvFileChunkIsReady(chunk);
            });

            threadPackage = new List<List<DataTypes.V5.AddressBookContact>>();
        }

        /// <summary>
        /// CsvFileChunkIsReady event handler
        /// </summary>
        /// <param name="sender">Event sender object</param>
        /// <param name="e">Sent args</param>
        private void FileReading_CsvFileChunkIsReady(object sender, FastFileReading.CsvFileChunkIsReadyArgs e)
        {
            threadPackage.Add(e.multiContacts);

            if (threadPackage.Count > 15)
            {
                Parallel.ForEach(threadPackage, chunk =>
                {
                    CsvFileChunkIsReady(chunk);
                });

                threadPackage = new List<List<DataTypes.V5.AddressBookContact>>();
            }
        }

        /// <summary>
        /// Validate a chunk of the address book contacts 
        /// </summary>
        /// <param name="contactsChunk"></param>
        private async void CsvFileChunkIsReady(List<DataTypes.V5.AddressBookContact> contactsChunk)
        {
            var route4Me = new Route4MeManagerV5(apiKey);

            var contactParams = new Route4MeManagerV5.BatchCreatingAddressBookContactsRequest()
            {
                Data = contactsChunk.ToArray()
            };

            foreach (var contact in contactsChunk)
            {
                var errorMessages = ValidateContact(contact);
                if (errorMessages.Count > 0 && ConsoleWriteMessage) Console.WriteLine("===================================");
            }
        }

        /// <summary>
        /// Validate API 5 address book contact
        /// </summary>
        /// <param name="contact">Address book contact</param>
        /// <returns>List of the error messages</returns>
        private List<string> ValidateContact(DataTypes.V5.AddressBookContact contact)
        {
            var errorMessages = new List<string>();
            string err = "";

            foreach (var fieldName in MandatoryFields)
            {
                object oval = typeof(DataTypes.V5.AddressBookContact).GetProperty(fieldName).GetValue(contact);
                switch (fieldName)
                {
                    case "CachedLat":
                    case "CurbsideLat":
                        var latok = PropertyValidation.ValidateLatitude(oval);
                        if (latok != null && latok != ValidationResult.Success)
                        {
                            err = $"Wrong {fieldName}: {oval.ToString()} in the address: {contact.address_1}";
                            errorMessages.Add(err);
                            if (ConsoleWriteMessage) Console.WriteLine(err);
                        }
                        break;
                    case "CachedLng":
                    case "CurbsideLng":
                        var lngok = PropertyValidation.ValidateLongitude(oval);
                        if (lngok != null && lngok != ValidationResult.Success)
                        {
                            err = $"Wrong {fieldName}: {oval.ToString()} in the address: {contact.address_1}";
                            errorMessages.Add(err);
                            if (ConsoleWriteMessage) Console.WriteLine(err);
                        }
                        break;
                    case "LocalTimeWindowStart":
                    case "LocalTimeWindowEnd":
                    case "LocalTimeWindowStart2":
                    case "LocalTimeWindowEnd2":
                        var twok = PropertyValidation.ValidateTimeWindow(oval);
                        if (twok != null && twok != ValidationResult.Success)
                        {
                            err = $"Wrong {fieldName}: {oval.ToString()} in the address: {contact.address_1}";
                            errorMessages.Add(err);
                            if (ConsoleWriteMessage) Console.WriteLine(err);
                        }
                        break;
                    case "ServiceTime":
                        var stok = PropertyValidation.ValidateServiceTime(oval);
                        if (stok != null && stok != ValidationResult.Success)
                        {
                            err = $"Wrong {fieldName}: {oval.ToString()} in the address: {contact.address_1}";
                            errorMessages.Add(err);
                            if (ConsoleWriteMessage) Console.WriteLine(err);
                        }
                        break;
                    case "AddressStateId":
                        var stateok = PropertyValidation.ValidationStateId(oval);
                        if (stateok != null && stateok != ValidationResult.Success)
                        {
                            err = $"Wrong {fieldName}: {oval.ToString()} in the address: {contact.address_1}. Error: {stateok.ErrorMessage}";
                            errorMessages.Add(err);
                            if (ConsoleWriteMessage) Console.WriteLine(err);
                        }
                        break;
                    case "AddressCountryId":
                        var aciok = PropertyValidation.ValidateCountryId(oval);
                        if (aciok != null && aciok != ValidationResult.Success)
                        {
                            err = $"Wrong {fieldName}: {oval.ToString()} in the address: {contact.address_1}. Error: {aciok.ErrorMessage}";
                            errorMessages.Add(err);
                            if (ConsoleWriteMessage) Console.WriteLine(err);
                        }
                        break;
                    case "AddressStopType":
                        var astok = PropertyValidation.ValidateAddressStopType(oval);
                        if (astok != null && astok != ValidationResult.Success)
                        {
                            err = $"Wrong {fieldName}: {oval.ToString()} in the address: {contact.address_1}. Error: {astok.ErrorMessage}";
                            errorMessages.Add(err);
                            if (ConsoleWriteMessage) Console.WriteLine(err);
                        }
                        break;
                    case "AddressZip":
                        var azok = PropertyValidation.ValidateZipCode(oval);
                        if (azok != null && azok != ValidationResult.Success)
                        {
                            err = $"Wrong {fieldName}: {oval.ToString()} in the address: {contact.address_1}. Error: {azok.ErrorMessage}";
                            errorMessages.Add(err);
                            if (ConsoleWriteMessage) Console.WriteLine(err);
                        }
                        break;
                    case "AddressCube":
                    case "AddressPieces":
                    case "AddressPriority":
                    case "AddressRevenue":
                    case "AddressWeight":
                        var numbok = PropertyValidation.ValidateIsNumber(oval);
                        if (numbok != null && numbok != ValidationResult.Success)
                        {
                            err = $"Wrong {fieldName}: {oval.ToString()} in the address: {contact.address_1}. Error: {numbok.ErrorMessage}";
                            errorMessages.Add(err);
                            if (ConsoleWriteMessage) Console.WriteLine(err);
                        }
                        break;
                    case "Color":
                        var colok = PropertyValidation.ValidateColorValue(oval);
                        if (colok != null && colok != ValidationResult.Success)
                        {
                            err = $"Wrong {fieldName}: {oval.ToString()} in the address: {contact.address_1}. Error: {colok.ErrorMessage}";
                            errorMessages.Add(err);
                            if (ConsoleWriteMessage) Console.WriteLine(err);
                        }
                        break;
                    case "EligibleDepot":
                    case "EligiblePickup":
                        var boolok = PropertyValidation.ValidateIsBoolean(oval);
                        if (boolok != null && boolok != ValidationResult.Success)
                        {
                            err = $"Wrong {fieldName}: {oval.ToString()} in the address: {contact.address_1}. Error: {boolok.ErrorMessage}";
                            errorMessages.Add(err);
                            if (ConsoleWriteMessage) Console.WriteLine(err);
                        }
                        break;
                }
            }

            return errorMessages;
        }
    }
}

