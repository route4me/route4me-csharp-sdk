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
        /// Generating of the SQL server database tables from the SQL script text files.
        /// </summary>
        public void GenerateSqlDatabase(DB_Type db_type)
        {
            cDatabase sqlDB = new cDatabase(db_type);
            
            try
            {
                string sAddressbookSqlCom = "";
                string sOrdersSqlCom = "";
                string sDictionaryDDLSqlCom = "";
                string sDictionaryDMLSqlCom = "";

                switch (db_type)
                {
                    case DB_Type.MySQL:
                        sAddressbookSqlCom = File.ReadAllText(@"Data/SQL/MySQL/addressbook_v4.sql");
                        sOrdersSqlCom = File.ReadAllText(@"Data/SQL/MySQL/orders.sql");
                        sDictionaryDDLSqlCom = File.ReadAllText(@"Data/SQL/MySQL/csv_to_api_dictionary_DDL.sql");
                        sDictionaryDMLSqlCom = File.ReadAllText(@"Data/SQL/MySQL/csv_to_api_dictionary_DML.sql");
                        break;
                    case DB_Type.MSSQL:
                        sAddressbookSqlCom = File.ReadAllText(@"Data/SQL/MSSQL/addressbook_v4.sql");
                        sOrdersSqlCom = File.ReadAllText(@"Data/SQL/MSSQL/orders.sql");
                        sDictionaryDDLSqlCom = File.ReadAllText(@"Data/SQL/MSSQL/csv_to_api_dictionary_DDL.sql");
                        sDictionaryDMLSqlCom = File.ReadAllText(@"Data/SQL/MSSQL/csv_to_api_dictionary_DML.sql");
                        break;
                    case DB_Type.PostgreSQL:
                        sAddressbookSqlCom = File.ReadAllText(@"Data/SQL/PostgreSQL/addressbook_v4.sql");
                        sOrdersSqlCom = File.ReadAllText(@"Data/SQL/PostgreSQL/orders.sql");
                        sDictionaryDDLSqlCom = File.ReadAllText(@"Data/SQL/PostgreSQL/csv_to_api_dictionary_DDL.sql");
                        sDictionaryDMLSqlCom = File.ReadAllText(@"Data/SQL/PostgreSQL/csv_to_api_dictionary_DML.sql");
                        break;
                    case DB_Type.SQLite:

                        break;
                    case DB_Type.MS_Access:

                        break;
                }

                sqlDB.OpenConnection();

                Console.WriteLine("Connection opened");

                int iResult = sqlDB.ExecuteMulticoomandSql(sAddressbookSqlCom);
                if (iResult > 0) Console.WriteLine(":) The SQL table 'addressbook_v4' created successfuly!!!"); else Console.WriteLine(":( Creating of the SQL table 'addressbook_v4' failed...");
                
                iResult = sqlDB.ExecuteMulticoomandSql(sOrdersSqlCom);
                if (iResult > 0) Console.WriteLine(":) The SQL table 'orders' created successfuly!!!"); else Console.WriteLine(":( Creating of the SQL table 'orders' failed...");

                iResult = sqlDB.ExecuteMulticoomandSql(sDictionaryDDLSqlCom);
                if (iResult > 0)
                {
                    Console.WriteLine(":) The SQL table 'csv_to_api_dictionary' created successfuly!!!");

                    iResult = sqlDB.ExecuteMulticoomandSql(sDictionaryDMLSqlCom);
                    if (iResult > 0) Console.WriteLine(":) The data was inserted into SQL table 'csv_to_api_dictionary' successfuly!!!"); else Console.WriteLine(":( Inserting of the data in the SQL table 'csv_to_api_dictionary' failed...");
                }
                else Console.WriteLine(":( Creating of the SQL table 'csv_to_api_dictionary' failed...");
               
            }
            catch (Exception ex) { Console.WriteLine("Generating of the SQL tables failed!.. "+ex.Message); }
            finally
            {
                sqlDB.CloseConnection();
            }
            
        }
    }
}

