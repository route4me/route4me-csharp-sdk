using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.FastProcessing
{
    public class FastValidateData
    {
        public static Connection con = new Connection();
        public string Message;

        private static List<List<AddressBookContact>> _threadPackage;
        private FastFileReading _fileReading;

        public FastValidateData(string ApiKey, bool EnableTraceSource = false)
        {
            if (ApiKey != "") apiKey = ApiKey;
            _threadPackage = new List<List<AddressBookContact>>();
        }

        public string apiKey { get; set; }

        public int CsvChunkSize { get; set; } = 300;
        public int JsonChunkSize { get; set; } = 300;
        public int ChunkPause { get; set; } = 2000;

        public string[] MandatoryFields { get; set; }

        /// <summary>
        ///     If true the messages is written in the console.
        /// </summary>
        public bool ConsoleWriteMessage { get; set; } = false;

        /// <summary>
        ///     Read and validate CSV file with large address book contacts.
        /// </summary>
        /// <param name="fileName">CSV file name</param>
        /// <param name="errorString">Error string</param>
        public void readLargeContactsCsvFile(string fileName, out string errorString)
        {
            errorString = "";

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

            _fileReading.CsvFileReadingIsDone += FileReading_CsvFileReadingIsDone;

            _fileReading.readingChunksFromLargeCsvFile(fileName, out errorString);
        }

        private void FileReading_CsvFileReadingIsDone(object sender, FastFileReading.CsvFileReadingIsDoneArgs e)
        {
            Parallel.ForEach(_threadPackage, new ParallelOptions {MaxDegreeOfParallelism = Environment.ProcessorCount},
                CsvFileChunkIsReady);

            _threadPackage = new List<List<AddressBookContact>>();
        }

        /// <summary>
        ///     CsvFileChunkIsReady event handler
        /// </summary>
        /// <param name="sender">Event sender object</param>
        /// <param name="e">Sent args</param>
        private void FileReading_CsvFileChunkIsReady(object sender, FastFileReading.CsvFileChunkIsReadyArgs e)
        {
            _threadPackage.Add(e.multiContacts);

            if (_threadPackage.Count > 15)
            {
                Parallel.ForEach(_threadPackage,
                    new ParallelOptions {MaxDegreeOfParallelism = Environment.ProcessorCount}, CsvFileChunkIsReady);

                _threadPackage = new List<List<AddressBookContact>>();
            }
        }

        /// <summary>
        ///     Validate a chunk of the address book contacts
        /// </summary>
        /// <param name="contactsChunk"></param>
        private void CsvFileChunkIsReady(List<AddressBookContact> contactsChunk)
        {
            foreach (var contact in contactsChunk)
            {
                var errorMessages = ValidateContact(contact);
                if (errorMessages.Count > 0 && ConsoleWriteMessage)
                    Console.WriteLine("===================================");
            }
        }

        /// <summary>
        ///     Validate API 5 address book contact
        /// </summary>
        /// <param name="contact">Address book contact</param>
        /// <returns>List of the error messages</returns>
        private List<string> ValidateContact(AddressBookContact contact)
        {
            var errorMessages = new List<string>();
            var err = "";

            foreach (var fieldName in MandatoryFields)
            {
                var oval = typeof(AddressBookContact).GetProperty(fieldName).GetValue(contact);
                switch (fieldName)
                {
                    case "CachedLat":
                    case "CurbsideLat":
                        var latok = PropertyValidation.ValidateLatitude(oval);
                        if (latok != null && latok != ValidationResult.Success)
                        {
                            err = $"Wrong {fieldName}: {oval} in the address: {contact.Address1}";
                            errorMessages.Add(err);
                            if (ConsoleWriteMessage) Console.WriteLine(err);
                        }

                        break;
                    case "CachedLng":
                    case "CurbsideLng":
                        var lngok = PropertyValidation.ValidateLongitude(oval);
                        if (lngok != null && lngok != ValidationResult.Success)
                        {
                            err = $"Wrong {fieldName}: {oval} in the address: {contact.Address1}";
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
                            err = $"Wrong {fieldName}: {oval} in the address: {contact.Address1}";
                            errorMessages.Add(err);
                            if (ConsoleWriteMessage) Console.WriteLine(err);
                        }

                        break;
                    case "ServiceTime":
                        var stok = PropertyValidation.ValidateServiceTime(oval);
                        if (stok != null && stok != ValidationResult.Success)
                        {
                            err = $"Wrong {fieldName}: {oval} in the address: {contact.Address1}";
                            errorMessages.Add(err);
                            if (ConsoleWriteMessage) Console.WriteLine(err);
                        }

                        break;
                    case "AddressStateId":
                        var stateok = PropertyValidation.ValidationStateId(oval);
                        if (stateok != null && stateok != ValidationResult.Success)
                        {
                            err =
                                $"Wrong {fieldName}: {oval} in the address: {contact.Address1}. Error: {stateok.ErrorMessage}";
                            errorMessages.Add(err);
                            if (ConsoleWriteMessage) Console.WriteLine(err);
                        }

                        break;
                    case "AddressCountryId":
                        var aciok = PropertyValidation.ValidateCountryId(oval);
                        if (aciok != null && aciok != ValidationResult.Success)
                        {
                            err =
                                $"Wrong {fieldName}: {oval} in the address: {contact.Address1}. Error: {aciok.ErrorMessage}";
                            errorMessages.Add(err);
                            if (ConsoleWriteMessage) Console.WriteLine(err);
                        }

                        break;
                    case "AddressStopType":
                        var astok = PropertyValidation.ValidateAddressStopType(oval);
                        if (astok != null && astok != ValidationResult.Success)
                        {
                            err =
                                $"Wrong {fieldName}: {oval} in the address: {contact.Address1}. Error: {astok.ErrorMessage}";
                            errorMessages.Add(err);
                            if (ConsoleWriteMessage) Console.WriteLine(err);
                        }

                        break;
                    case "AddressZip":
                        var azok = PropertyValidation.ValidateZipCode(oval);
                        if (azok != null && azok != ValidationResult.Success)
                        {
                            err =
                                $"Wrong {fieldName}: {oval} in the address: {contact.Address1}. Error: {azok.ErrorMessage}";
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
                            err =
                                $"Wrong {fieldName}: {oval} in the address: {contact.Address1}. Error: {numbok.ErrorMessage}";
                            errorMessages.Add(err);
                            if (ConsoleWriteMessage) Console.WriteLine(err);
                        }

                        break;
                    case "Color":
                        var colok = PropertyValidation.ValidateColorValue(oval);
                        if (colok != null && colok != ValidationResult.Success)
                        {
                            err =
                                $"Wrong {fieldName}: {oval} in the address: {contact.Address1}. Error: {colok.ErrorMessage}";
                            errorMessages.Add(err);
                            if (ConsoleWriteMessage) Console.WriteLine(err);
                        }

                        break;
                    case "EligibleDepot":
                    case "EligiblePickup":
                        var boolok = PropertyValidation.ValidateIsBoolean(oval);
                        if (boolok != null && boolok != ValidationResult.Success)
                        {
                            err =
                                $"Wrong {fieldName}: {oval} in the address: {contact.Address1}. Error: {boolok.ErrorMessage}";
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