using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Text;
using System.IO;
using System.Data;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Uploading CSV file to the SQL server's database table.
        /// </summary>
        public void UploadCsvToAddressbookV4(DB_Type db_type)
        {
            cDatabase sqlDB = new cDatabase(db_type);

            try
            {
                sqlDB.OpenConnection();

                Console.WriteLine("Connection opened");

                sqlDB.Csv2Table(@"Data/CSV/Route4Me Address Book 03-09-2017.csv", "addressbook_v4", "id", 33, true);

                Console.WriteLine("The file orders.csv was uploaded to the SQL server.");
            }
            catch (Exception ex) { Console.WriteLine("Uploading of the CSV file to the SQL server failed!.. " + ex.Message); }
            finally
            {
                sqlDB.CloseConnection();
            }


        }
    }
}

