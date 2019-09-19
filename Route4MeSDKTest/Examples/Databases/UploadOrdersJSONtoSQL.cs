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
        /// Uploading JSON orders file to the SQL server's database table.
        /// </summary>
        public void UploadOrdersJSONtoSQL(DB_Type db_type)
        {
            cDatabase sqlDB = new cDatabase(db_type);

            try
            {
                sqlDB.OpenConnection();

                Console.WriteLine("Connection opened");

                sqlDB.Json2Table(@"Data/JSON/get orders RESPONSE.json", R4M_DataType.Order);

                Console.WriteLine("The JSON file was uploaded to the SQL server.");
            }
            catch (Exception ex) { Console.WriteLine("Uploading of the JSON file to the SQL server failed!.. "+ex.Message); }
            finally
            {
                sqlDB.CloseConnection();
            }
        }


    }
}
