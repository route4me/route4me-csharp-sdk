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
        /// Preparing csv file for addressbook data uploading.
        /// </summary>
        public void TestExample()
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            cDadtabase mysqlDB = new cDadtabase();

            try
            {
                mysqlDB.OpenConnection();

                Console.WriteLine("Connection opened");
                string sCom = "SELECT * FROM addressbook_v4 INTO OUTFILE 'addressbook_v4.txt';";

                mysqlDB.ExecuteNon(sCom);
                
            }
            catch (Exception ex) { Console.WriteLine("Making of a addressbook csv file failured!.."); }
            finally
            {
                mysqlDB.CloseConnection();
            }

        }
    }
}


