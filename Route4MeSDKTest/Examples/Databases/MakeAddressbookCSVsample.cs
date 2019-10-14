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
        /// Exporting a SQL server database table to the local CSV file.
        /// </summary>
        public void MakeAddressbookCSVsample(DB_Type db_type)
        {
            cDatabase sqlDB = new cDatabase(db_type);

            try
            {
                sqlDB.OpenConnection();

                Console.WriteLine("Connection opened");

                sqlDB.Table2Csv(@"Data/CSV/addressbook v4.csv", "addressbook_v4", true);
                Console.WriteLine("The file addressbook v4.csv was created.");
               
            }
            catch (Exception ex) { Console.WriteLine("Making of a addressbook csv file failed!.. "+ex.Message); }
            finally
            {
                sqlDB.CloseConnection();
            }
            
        }
    }
}

