using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Data.OleDb;
using System.Web.Script;

namespace Route4MeSDK.DataTypes
{
    public enum R4M_DataType
    {
        Activity,
        Addressbook,
        AvoidanceZone,
        Member,
        Note,
        Optimization,
        Order,
        Route,
        Telematics,
        Territory,
        Vehicle
    }

    public enum DB_Type
    {
        MSSQL,
        MySQL,
        PostgreSQL,
        SQLite,
        MS_Access
    }

    public class cDatabase : IDisposable 
    {
        private IDbConnection _con;
        private IDbCommand _cmd;

        private IDbTransaction _transaction;
        private DbDataAdapter _adapter;
        private IDataReader _dr;
        private DbProviderFactory _factory;
        private ConnectionStringSettings _conStngInstitute;

        private bool _isDisposed;

        private string sStartupFolder;


        public cDatabase(DB_Type db_type)
        {
            switch (db_type)
            {
                case DB_Type.MySQL:
                    _conStngInstitute = ConfigurationManager.ConnectionStrings["conMySQL"];
                    break;
                case DB_Type.MSSQL:
                    _conStngInstitute = ConfigurationManager.ConnectionStrings["conMSSQL"];
                    break;
                case DB_Type.PostgreSQL:
                    _conStngInstitute = ConfigurationManager.ConnectionStrings["conPostgreSQL"];
                    break;
                case DB_Type.SQLite:
                    //_conStngInstitute = ConfigurationManager.ConnectionStrings["conInstitute"];
                    break;
                case DB_Type.MS_Access:
                    _conStngInstitute = ConfigurationManager.ConnectionStrings["conOLEDB"];
                    break;
            }

            _factory = DbProviderFactories.GetFactory(_conStngInstitute.ProviderName);

            _con = _factory.CreateConnection();
            _con.ConnectionString = _conStngInstitute.ConnectionString;
            _cmd = _con.CreateCommand();
            
            _adapter = _factory.CreateDataAdapter();

            _isDisposed = false;

            sStartupFolder = AppDomain.CurrentDomain.BaseDirectory;
        }

        public bool IsDisposed
        {
            get
            {
                lock (this)
                {
                    return _isDisposed;
                }
            }
        }

        public void Dispose()
        {
            if (!IsDisposed)
            {
                lock (this)
                {
                    CleanUp();
                    _isDisposed = true;
                    GC.SuppressFinalize(this);
                }
            }
        }

        protected virtual void CleanUp()
        {
            if (_con != null)
            {
                _con.Close();
                _con.Dispose();
            }

            if (_cmd != null)
            {
                _cmd.Dispose();
            }

            if (_dr != null)
            {
                _dr.Close();
                _dr.Dispose();
            }
        } 

        public void OpenConnection()
        {
            try
            {
                if (_con.State != ConnectionState.Open) _con.Open();
            }
            catch (Exception ex) { Console.WriteLine("Connection not established!.."); }
        }

        public void CloseConnection()
        {
            if (_con.State != ConnectionState.Closed) _con.Close();
        }

        /* Parsing of the multi-command SQL texts, ommiting commentaries and extracting of the puare SQL commands;
         * Note: 
         * - after semicolon ';' shouldn't be written anything (blank spaces allowed).
         * - befor '/*' shouldn't be written anything (blank spaces allowed).
         * */
        public int ExecuteMulticoomandSql(string sQuery)
        {
            try
            {
                sQuery = sQuery.Replace(";", ";^");
                string[] arCommands = sQuery.Split('^');
                int iRet = 0; 
                _transaction = _con.BeginTransaction(IsolationLevel.Unspecified);
                _cmd.Connection = _con;
                _cmd.CommandType = CommandType.Text;
                _cmd.Transaction = _transaction;
                bool blComment = false;
                foreach (string s0 in arCommands)
                {
                    string[] arLines = s0.Split(new[] {"\r\n", "\r", "\n"}, StringSplitOptions.None);
                    string sCurCommand = "";
                    foreach (string s1 in arLines)
                    {
                        string s2 = s1.Trim();
                        if (s2.Length < 1) continue;
                        if (s2.IndexOf("--") == 0) continue;
                        if (s2.IndexOf(@"/*") == 0)
                        {
                            if (s2.IndexOf(@"*/") == s1.Length - 2) blComment = false; else blComment = true; 
                            continue;
                        }
                        if (s2.Length>=2 && s2.IndexOf(@"*/") == s1.Length - 2)
                        {
                            blComment = false; continue;
                        }

                        if (!blComment)
                        {
                            if (s2.IndexOf(";") != s2.Length - 1)
                            {
                                sCurCommand += s2 + System.Environment.NewLine;
                            }
                            else
                            {
                                sCurCommand += s2;
                                _cmd.CommandText = sCurCommand;
                                iRet = _cmd.ExecuteNonQuery();
                                sCurCommand = "";
                            }
                            
                        }

                    }
                }
                _transaction.Commit();
                return 1;
            }
            catch (Exception ex) { Console.WriteLine(":( Transaction failed... " + ex.Message); _transaction.Rollback(); return 0; }
            
        }

        // Table for correspondance between Route4Me CSV exported file fields and Route4Me API fields
        public DataTable GetCsv2ApiDictionary(string sTableName)
        {
            DataTable tblDictionary = new DataTable();

            try
            {
                tblDictionary = fillTable("SELECT * FROM csv_to_api_dictionary WHERE table_name='" + sTableName+"'");
                return tblDictionary;
            }
            catch (Exception ex) { Console.WriteLine(":( csv_to_api_dictionary table reading failed!.. "+ex.Message); }

            return tblDictionary;
        }

        /* Method for importing an addressbook CSV file (with structure equal to exported by Route4Me web UI CSV file) to an addressbook table on the SQL type server.
         * sFileName --- CSV file name.
         * sTableName --- Server addressbook table name.
         * sIdName --- The name of id column of the server addressbook table (it's differs from address_id, you need it for editing prior updloading to the Route4Me server)
         * isFirstRowHeader --- If true, first column of the CSV file is header.
         * */
        public void Csv2Table(string sFileName, string sTableName, string sIdName, int iFieldsNumber, bool isFirstRowHeader)
        {
            if (!File.Exists(sFileName))
            {
                Console.WriteLine("The file " + sFileName + " doesn't exist..."); return;
            }

            string header = isFirstRowHeader ? "Yes" : "No";

            string pathOnly = System.IO.Path.GetDirectoryName(sFileName);
            string fileName = System.IO.Path.GetFileName(sFileName);

            string csvCom = @"SELECT * FROM [" + fileName + "]";

            DataTable tblDictionary = GetCsv2ApiDictionary(sTableName);

            DataTable tblTempTable = new DataTable();

            using (OleDbConnection csvCon = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathOnly + ";Extended Properties=\"Text;HDR=" + header + "\""))
            using (OleDbCommand comCsv = new OleDbCommand(csvCom, csvCon))
            using (OleDbDataAdapter csvAdapter = new OleDbDataAdapter(comCsv))
            {
                tblTempTable.Locale = System.Globalization.CultureInfo.CurrentCulture;
                csvAdapter.Fill(tblTempTable);
            }

            foreach (DataRow row in tblTempTable.Rows)
            {
                int id = -1;

                bool blNew = true;

                if (row.Table.Columns.Contains(sIdName))
                {
                    if (int.TryParse(row[sIdName].ToString(), out id)) id = Convert.ToInt32(row[sIdName]);

                    if (id > 0) blNew = IsNewAddress(sTableName, sIdName, id);
                }
                string sList = "(";
                string sValues = "Values (";

                if (!blNew)
                {
                    sList = "SET ";

                    for (int iCol = 0; iCol < iFieldsNumber; iCol++)
                    {
                        bool isValid = IsValidValue(tblTempTable.Columns[iCol], row[iCol]);
                        if (isValid)
                        {
                            string sCsvFieldName = tblTempTable.Columns[iCol].ColumnName;

                            if (sCsvFieldName == sIdName) continue;

                            DataRow[] arRows = tblDictionary.Select("r4m_csv_field_name='" + sCsvFieldName + "'");
                            if (arRows.Length == 1)
                            {
                                string sFieldApiName= arRows[0]["api_field_name"].ToString();
                                string sApiFieldType = arRows[0]["api_field_type"].ToString();
                                string sCsvFieldType = arRows[0]["csv_field_type"].ToString();

                                if (sFieldApiName == "day_scheduled_for_YYMMDD")
                                {
                                    
                                    var oSchedule = ExceptFields2QueryValue(sFieldApiName, row[iCol]);
                                    if (oSchedule == null)
                                    {
                                        sList += sFieldApiName + "=null,";
                                    }
                                    else sList += sFieldApiName + "='" + oSchedule + "',";
                                }
                                else
                                {
                                    switch (tblTempTable.Columns[iCol].DataType.Name)
                                    {
                                        case "String":
                                            sList += sFieldApiName + "=N'" + row[iCol].ToString() + "',";
                                            break;
                                        case "Int32":
                                            sList += sFieldApiName + "=" + row[iCol].ToString() + ","; ;
                                            break;
                                        case "Double":
                                            sList += sFieldApiName + "=" + row[iCol].ToString() + ",";
                                            break;
                                        case "DateTime":
                                            DateTime dt1900 = new DateTime(1900, 1, 1, 0, 0, 0);
                                            if (DateTime.TryParse(row[iCol].ToString(), out dt1900))
                                            {
                                                dt1900 = Convert.ToDateTime(row[iCol]);
                                                if (sApiFieldType != sCsvFieldType && sApiFieldType == "int")
                                                {
                                                    long iUnixTime = R4MeUtils.ConvertToUnixTimestamp(dt1900);
                                                    sList += sFieldApiName + "=" + iUnixTime + ",";
                                                }
                                                else
                                                {
                                                    if (_conStngInstitute.ProviderName == "System.Data.OleDb")
                                                    {
                                                        sList += sFieldApiName + "=#" + dt1900.ToString("yyyy-MM-dd HH:mm:ss") + "#,";
                                                    }
                                                    else sList += sFieldApiName + "='" + dt1900.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                                                }
                                            }
                                            break;
                                    }
                                }
                                
                            }
                            else continue;
                        }
                    }
                    sList = sList.TrimEnd(',');

                    if (tblTempTable.Columns.Count > 33)
                    {
                        System.Text.StringBuilder sbCustom = new System.Text.StringBuilder();
                        sbCustom.Append("{");
                        for (int iCol = 33; iCol < tblTempTable.Columns.Count; iCol++)
                        {
                            System.Text.StringBuilder sbCustom1 = new System.Text.StringBuilder();
                            sbCustom1.Append("{");
                            for (int iCol1 = 33; iCol1 < tblTempTable.Columns.Count; iCol1++)
                            {
                                bool isValid = IsValidValue(tblTempTable.Columns[iCol1], row[iCol1]);
                                if (isValid)
                                {
                                    if (tblTempTable.Columns[iCol1].DataType.Name == "String")
                                    {
                                        string sFieldName = tblTempTable.Columns[iCol1].ColumnName;
                                        string sValue = row[iCol1].ToString();
                                        sbCustom1.Append("\"" + sFieldName + "\": \"" + sValue + "\",");
                                    }
                                }
                            }
                            string sCustom = sbCustom.ToString();
                            sCustom = sCustom.TrimEnd(',');
                            sCustom += "}";
                            if (sCustom.Length > 3)
                            {
                                sList += "address_custom_data='" + " N'" + sCustom + "'";
                            }
                        }
                    }

                    string sQuery2 = "UPDATE " + sTableName + " " + sList + " WHERE " + sIdName + "=" + id;

                    int iResult2 = ExecuteNon(sQuery2);

                    if (iResult2 > 0)
                    {
                        Console.WriteLine(":) The row with "+sIdName+"d=" + id + " was updated in the table "+sTableName);
                    }
                    else
                    {
                        Console.WriteLine(":( Can not updated the row in the table "+sTableName);
                    }
                }
                else
                {
                    for (int iCol = 0; iCol < iFieldsNumber; iCol++)
                    {
                        bool isValid = IsValidValue(tblTempTable.Columns[iCol], row[iCol]);

                        if (isValid)
                        {
                            string sCvsFieldName = tblTempTable.Columns[iCol].ColumnName;
                            DataRow[] arRows = tblDictionary.Select("r4m_csv_field_name='" + sCvsFieldName + "'");
                            if (arRows.Length == 1)
                            {
                                string sFieldApiName = arRows[0]["api_field_name"].ToString();
                                string sApiFieldType = arRows[0]["api_field_type"].ToString();
                                string sCsvFieldType = arRows[0]["csv_field_type"].ToString();

                                //sFields += prop.Name + ",";
                                if (sFieldApiName == "day_scheduled_for_YYMMDD")
                                {
                                    sList += sFieldApiName + ",";
                                    var oSchedule = ExceptFields2QueryValue(sFieldApiName, row[iCol]);
                                    if (oSchedule == null)
                                    {
                                        sValues += "null";
                                    }
                                    else sValues += "'" + oSchedule + "',";
                                }
                                else
                                {
                                    switch (tblTempTable.Columns[iCol].DataType.Name)
                                    {
                                        case "String":
                                            sList += sFieldApiName + ",";
                                            sValues += "N'" + row[iCol].ToString() + "',";
                                            break;
                                        case "Int32":
                                            sList += sFieldApiName + ",";
                                            sValues += row[iCol].ToString() + ",";
                                            break;
                                        case "Double":
                                            sList += sFieldApiName + ",";
                                            sValues += row[iCol].ToString() + ",";
                                            break;
                                        case "DateTime":
                                            DateTime dt1900 = new DateTime(1900, 1, 1, 0, 0, 0);
                                            if (DateTime.TryParse(row[iCol].ToString(), out dt1900))
                                            {
                                                dt1900 = Convert.ToDateTime(row[iCol]);
                                                sList += sFieldApiName + ",";
                                                if (sApiFieldType != sCsvFieldType && sApiFieldType == "int")
                                                {
                                                    long iUnixTime = R4MeUtils.ConvertToUnixTimestamp(dt1900);
                                                    sValues += iUnixTime + ",";
                                                }
                                                else
                                                {
                                                    if (_conStngInstitute.ProviderName == "System.Data.OleDb")
                                                    {
                                                        sValues += "#" + dt1900.ToString("yyyy-MM-dd HH:mm:ss") + "#,";
                                                    }
                                                    else sValues += "'" + dt1900.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                                                }
                                            }
                                            break;
                                    }
                                }

                            }
                        }
                    }

                    #region custom fields in case of the addressbook contact, added in csv export as additional columns.
                    if (tblTempTable.Columns.Count > 33)
                    {
                        System.Text.StringBuilder sbCustom = new System.Text.StringBuilder();
                        sbCustom.Append("{");
                        for (int iCol = 33; iCol < tblTempTable.Columns.Count; iCol++)
                        {
                            bool isValid = IsValidValue(tblTempTable.Columns[iCol], row[iCol]);
                            if (isValid)
                            {
                                if (tblTempTable.Columns[iCol].DataType.Name == "String")
                                {
                                    string sFieldName = tblTempTable.Columns[iCol].ColumnName;
                                    string sValue = row[iCol].ToString();
                                    sbCustom.Append("\"" + sFieldName + "\": \"" + sValue + "\",");
                                }
                            }
                        }
                        string sCustom = sbCustom.ToString();
                        sCustom = sCustom.TrimEnd(',');
                        sCustom += "}";
                        if (sCustom.Length > 3)
                        {
                            sList += "address_custom_data,";
                            sValues += " N'" + sCustom + "'";
                        }

                    }
                    #endregion

                    sList = sList.TrimEnd(','); sList += ")";
                    sValues = sValues.TrimEnd(','); sValues += ")";
                    string sQuery1 = "INSERT INTO "+sTableName+" " + sList + " " + sValues;

                    int iResult = ExecuteNon(sQuery1);

                    if (iResult > 0)
                    {
                        Console.WriteLine(":) New row with "+sIdName+ "=" + id + " was added to the table "+sTableName);
                    }
                    else
                    {
                        Console.WriteLine(":( Can not created new row in the table "+sTableName);
                    }
                }
            }
        }

        /* Method for exporting addressbook data from SQL type server to the CSV file (with structure equal to the exported by Route4Me web UI CSV file)
         * sFileName --- CSV file name.
         * sTableName --- Server addressbook table name.
         * WithId --- If true, CSV file will have first ID of SQL addressbook table (you need it for editing in CSV file and updating server table using Csv2Table method.
         * */
        public void Table2Csv(string sFileName, string sTableName, bool WithId)
        {
            if (!CheckDataFolder(sFileName, true)) return;

            DataTable tblTemp = fillTable("SELECT * FROM "+sTableName);

            List<string> lsCsvContent = new List<string>();

            string sFileHeader = "";
            if (WithId) sFileHeader += "\"" + tblTemp.Columns[0].ColumnName + "\",";

            DataTable tblDictionary = GetCsv2ApiDictionary(sTableName);

            foreach (DataRow dictRow in tblDictionary.Rows)
            {
                sFileHeader += "\"" + dictRow["r4m_csv_field_name"].ToString() + "\",";
            }

            #region Convert JSON string of the custom data to the csv fields (as they are represented in the exported from Route4Me csv file
            foreach (DataRow row in tblTemp.Rows)
            {
                if (IsValidValue(tblTemp.Columns["address_custom_Data"], row["address_custom_Data"]))
                {
                    var jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    Dictionary<string, object> dict = (Dictionary<string, object>)jsSerializer.DeserializeObject(row["address_custom_Data"].ToString());

                    foreach (KeyValuePair<string, object> kvpair in dict)
                    {
                        DataRow[] fRows = tblDictionary.Select("r4m_csv_field_name='" + kvpair.Key + "'");

                        if (fRows.Length < 1)
                        {
                            DataRow newRow = tblDictionary.NewRow();

                            newRow["r4m_csv_field_name"] = kvpair.Key;
                            newRow["table_name"] = "addressbook_v4";
                            newRow["csv_field_nom"] = tblDictionary.Rows.Count;
                            newRow["api_field_name"] = "_cf__" + kvpair.Key;

                            tblDictionary.Rows.Add(newRow);

                            sFileHeader += "\"" + kvpair.Key + "\",";
                        }

                    }
                }
            }
            #endregion

            sFileHeader = sFileHeader.TrimEnd(',');

            lsCsvContent.Add(sFileHeader);

            foreach (DataRow row in tblTemp.Rows)
            {
                string sRow = "";

                if (WithId)
                {
                    if (IsValidValue(tblTemp.Columns[0], row[0]))
                    {
                        sRow += row[0].ToString() + ",";
                    }
                }

                foreach (DataRow dictRow in tblDictionary.Rows)
                {
                    string sCsvFieldName = dictRow["r4m_csv_field_name"].ToString();

                    string sApiFieldName = dictRow["api_field_name"].ToString();

                    if (sApiFieldName.IndexOf("_cf__") == 0)
                    {
                        string sKeyName = sApiFieldName.Substring(5);

                        if (IsValidValue(tblTemp.Columns["address_custom_Data"], row["address_custom_Data"]))
                        {
                            var jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                            Dictionary<string, object> dict = (Dictionary<string, object>)jsSerializer.DeserializeObject(row["address_custom_Data"].ToString());

                            bool blExists = false;
                            foreach (KeyValuePair<string, object> kvpair in dict)
                            {
                                if (kvpair.Key == sKeyName)
                                {
                                    string sVal = kvpair.Value.ToString();
                                    sVal = sVal.Replace("\"", "\"\"");
                                    sRow += "\"" + sVal + "\",";
                                    blExists = true;
                                    break;
                                }
                            }

                            if (!blExists) sRow += ",";
                        }
                        else sRow += ",";
                    }
                    else
                    {
                        DataColumn apiCol = tblTemp.Columns[sApiFieldName];

                        if (IsValidValue(apiCol, row[apiCol.ColumnName]))
                        {
                            switch (apiCol.DataType.ToString())
                            {
                                case "System.String":
                                    string sVal = row[apiCol.ColumnName].ToString();
                                    sVal = sVal.Replace("\"", "\"\"");
                                    sRow += "\"" + sVal + "\",";
                                    break;
                                case "System.DdateTime":
                                    sRow += "\"" + Convert.ToDateTime(row[apiCol.ColumnName]).ToString("yyyy-MM-dd HH:mm:ss") + "\",";
                                    break;
                                default:
                                    sRow += row[apiCol.ColumnName] + ",";
                                    break;
                            }

                        }
                        else sRow += ",";
                    }

                }

                sRow = sRow.TrimEnd(',');
                lsCsvContent.Add(sRow);
            }
            
            File.WriteAllLines(sFileName, lsCsvContent.ToArray());

            Console.WriteLine("The file "+sFileName+" was created. You can fill it with data for upoloading on the server.");
        }

        /* Create data folder if it doesn't exist.
         * */
        private bool CheckDataFolder(string file_name, bool blCreateIfNotExists)
        {
            try
            {
                DirectoryInfo iDir = Directory.GetParent(file_name);
                if (File.Exists(iDir.FullName))
                {
                    return true;
                    
                }
                else
                {
                    if (blCreateIfNotExists) Directory.CreateDirectory(iDir.FullName);
                    return true;
                }
                
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Creation of the data folder failed... "+ex.Message);
                return false;
            }
        }

        /* Method FieldValue2QueryValue converts value of the type ttype to value for sqlquery operations (update, insert)
         **/
        private string FieldValue2QueryValue(Type ttype, object oValue)
        {
            string sQueryValue = "";

            switch (ttype.Name)
            {
                case "String":
                    sQueryValue = "N'" + oValue.ToString() + "'";
                    break;
                case "Int32":
                    sQueryValue = oValue.ToString();
                    break;
                case "Double":
                    sQueryValue = oValue.ToString();
                    break;
                case "DateTime":
                    DateTime dt1900 = new DateTime(1900, 1, 1, 0, 0, 0);
                    if (DateTime.TryParse(oValue.ToString(), out dt1900))
                    {
                        dt1900 = Convert.ToDateTime(oValue);
                        if (_conStngInstitute.ProviderName == "System.Data.OleDb")
                        {
                            sQueryValue = "#" + dt1900.ToString("yyyy-MM-dd HH:mm:ss") + "#";
                        }
                        else sQueryValue = "'" + dt1900.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    }
                    break;
            }

            return sQueryValue;
        }

        private string ExceptFields2QueryValue(string PropertyName, object oValue)
        {
            string sQueryValue = "";
            if (oValue == null) return "null";

            switch (PropertyName)
            {
                case "day_scheduled_for_YYMMDD":
                    DateTime dt1900 = new DateTime(1900, 1, 1, 0, 0, 0);
                    if (DateTime.TryParse(oValue.ToString(), out dt1900))
                    {
                        dt1900 = Convert.ToDateTime(oValue);
                        return dt1900.ToShortDateString();
                    } else return null;
                    break;
                case "EXT_FIELD_custom_data":
                    System.Text.StringBuilder sbOrderCustom = new System.Text.StringBuilder();
                    sbOrderCustom.Append("{");
                    foreach (KeyValuePair<string, object> kvpair in (Dictionary<string, object>)oValue)
                    {
                        if (kvpair.Value == null)
                        {
                            sbOrderCustom.Append("\"" + kvpair.Key + "\": null,");
                        }
                        else sbOrderCustom.Append("\"" + kvpair.Key + "\": \"" + kvpair.Value.ToString() + "\",");
                    }
                    sQueryValue = sbOrderCustom.ToString().TrimEnd(',');
                    sQueryValue += "}";
                    break;
                case "address_custom_data":
                    System.Text.StringBuilder sbCustom = new System.Text.StringBuilder();
                    sbCustom.Append("{");
                    foreach (KeyValuePair<string, object> kvpair in (Dictionary<string, object>)oValue)
                    {
                        if (kvpair.Value == null)
                        {
                            sbCustom.Append("\"" + kvpair.Key + "\": null,");
                        }
                        else sbCustom.Append("\"" + kvpair.Key + "\": \"" + kvpair.Value.ToString() + "\",");
                    }
                    sQueryValue = sbCustom.ToString().TrimEnd(',');
                    sQueryValue += "}";
                    break;
                case "schedule":
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(IList<Schedule>));
                    
                    using (MemoryStream ms = new MemoryStream())
                    {
                        serializer.WriteObject(ms, oValue);
                        sQueryValue = System.Text.Encoding.Default.GetString(ms.ToArray());
                    }
                    break;
                case "schedule_blacklist":
                     System.Text.StringBuilder sbBlackList = new System.Text.StringBuilder();
                     foreach (string dt1 in (string[])oValue)
                     {
                         sbBlackList.Append("\""+dt1+"\",");
                     }
                     sQueryValue = sbBlackList.ToString();
                     sQueryValue = sQueryValue.TrimEnd(',');
                    break;

            }

            return sQueryValue;
        }

        /* Upload JSON response file, generated by the process of getting addressbook contacts by Route4Me API, to the SQL type server.
         * */
        public void Json2Table(string sFileName, string sTableName, string sIdName, R4M_DataType r4m_dtype)
        {
            if (!File.Exists(sFileName))
            {
                Console.WriteLine("The file " + sFileName + " doesn't exist..."); return;
            }

            string pathOnly = System.IO.Path.GetDirectoryName(sFileName);
            string fileName = System.IO.Path.GetFileName(sFileName);

            string jsonContent = File.ReadAllText(sFileName);

            DataTable tblTempTable = new DataTable();
            
            var jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            switch (r4m_dtype)
            {
                case R4M_DataType.Addressbook:
                    //var jsSerializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(AddressBookContactsResponse));
                    AddressBookContactsResponse Data = jsSerializer.Deserialize<AddressBookContactsResponse>(jsonContent);
                    if (Data.total == 0) break;

                    foreach (AddressBookContact contact in Data.results)
                    {
                        string sQuery = "";
                        if (IsNewAddressID("addressbook_v4", contact.address_id ))
                        {
                            sQuery = "INSERT INTO addressbook_v4 ";
                            string sFields = "";
                            string sValues = "";
                            foreach (System.Reflection.PropertyInfo prop in typeof(AddressBookContact).GetProperties())
                            {
                                if (prop.Name=="address_id") continue;
                                if (prop.Name == "ConvertBooleansToInteger") continue;
                                if (prop.MemberType != System.Reflection.MemberTypes.Property) continue;
                                var vValue = contact.GetType().GetProperty(prop.Name).GetValue(contact, null);
                                if (vValue == null) continue;

                                Console.WriteLine("Properyt type=" + prop.PropertyType.Name);

                                sFields += prop.Name + ",";
                                if (prop.Name == "address_custom_data" || prop.Name == "schedule" || prop.Name == "schedule_blacklist")
                                {
                                    sValues += "'" + ExceptFields2QueryValue(prop.Name, vValue) + "',";
                                }
                                else
                                {
                                    sValues += FieldValue2QueryValue(vValue.GetType(), vValue) + ",";
                                }

                            }
                            sFields = sFields.TrimEnd(','); 
                            sValues=sValues.TrimEnd(',');

                            sFields = "("+sFields+")";
                            sValues = "(" + sValues + ")";

                            sQuery += sFields + " VALUES " +sValues;
                        }
                        else
                        {
                            int address_id = (int)contact.address_id;
                            sQuery = "UPDATE addressbook_v4 SET ";
                            string sSet = "";
                            foreach (System.Reflection.PropertyInfo prop in typeof(AddressBookContact).GetProperties())
                            {
                                if (prop.Name == "address_id") continue;
                                if (prop.Name == "ConvertBooleansToInteger") continue;
                                if (prop.MemberType != System.Reflection.MemberTypes.Property) continue;
                                var vValue = contact.GetType().GetProperty(prop.Name).GetValue(contact,null);
                                if (vValue == null) continue;
                                
                                Console.WriteLine("Properyt type=" + prop.PropertyType.Name);
                                if (prop.Name == "address_custom_data" || prop.Name == "schedule" || prop.Name == "schedule_blacklist")
                                {
                                    sSet += prop.Name + "='" + ExceptFields2QueryValue(prop.Name, vValue) + "',";
                                }
                                else sSet += prop.Name + "=" + FieldValue2QueryValue(vValue.GetType(), vValue) + ",";
                            }
                            sSet = sSet.TrimEnd(',');
                            sQuery += sSet + " WHERE address_id=" + address_id;
                        }
                        int iSuccess = ExecuteNon(sQuery);
                        
                    }
                    break;
                case R4M_DataType.Order:
                    OrdersResponse ordersData = jsSerializer.Deserialize<OrdersResponse>(jsonContent);
                    if (ordersData.total == 0) break;

                    foreach (Order order in ordersData.results)
                    {
                        string sQuery = "";
                        if (IsNewOrderID("orders", order.order_id))
                        {
                            sQuery = "INSERT INTO orders ";
                            string sFields = "";
                            string sValues = "";

                            foreach (System.Reflection.PropertyInfo prop in typeof(Order).GetProperties())
                            {
                                //if (prop.Name == "order_id") continue;
                                if (prop.Name == "ConvertBooleansToInteger") continue;
                                if (prop.MemberType != System.Reflection.MemberTypes.Property) continue;
                                var vValue = order.GetType().GetProperty(prop.Name).GetValue(order, null);
                                if (vValue == null) continue;

                                Console.WriteLine("Properyt type=" + prop.PropertyType.Name);

                                sFields += prop.Name + ",";
                                if (prop.Name == "EXT_FIELD_custom_data")
                                {
                                    sValues += "'" + ExceptFields2QueryValue(prop.Name, vValue) + "',";
                                }
                                else
                                {
                                    sValues += FieldValue2QueryValue(vValue.GetType(), vValue) + ",";
                                }

                            }
                            sFields = sFields.TrimEnd(',');
                            sValues = sValues.TrimEnd(',');

                            sFields = "(" + sFields + ")";
                            sValues = "(" + sValues + ")";

                            sQuery += sFields + " VALUES " + sValues;

                        }
                        else
                        {
                            int order_id = (int)order.order_id;
                            sQuery = "UPDATE orders SET ";
                            string sSet = "";

                            foreach (System.Reflection.PropertyInfo prop in typeof(Order).GetProperties())
                            {
                                if (prop.Name == "order_id") continue;
                                if (prop.Name == "ConvertBooleansToInteger") continue;
                                if (prop.MemberType != System.Reflection.MemberTypes.Property) continue;
                                var vValue = order.GetType().GetProperty(prop.Name).GetValue(order, null);
                                if (vValue == null) continue;

                                Console.WriteLine("Properyt type=" + prop.PropertyType.Name);
                                if (prop.Name == "EXT_FIELD_custom_data")
                                {
                                    sSet += prop.Name + "='" + ExceptFields2QueryValue(prop.Name, vValue) + "',";
                                }
                                else sSet += prop.Name + "=" + FieldValue2QueryValue(vValue.GetType(), vValue) + ",";
                            }
                            sSet = sSet.TrimEnd(',');
                            sQuery += sSet + " WHERE order_id=" + order_id;
                        }
                        int iOrderSuccess = ExecuteNon(sQuery);
                    }
                    
                    break;
                case R4M_DataType.Route:

                    break;
            }
        }

        public bool IsNewAddress(string sTableName, string sIdName, int AddressId)
        {
            bool blNew = true;
            string sCom = @"SELECT COUNT(*) as rba FROM "+sTableName+ " WHERE "+sIdName+"="+AddressId;
            object result = ExecuteScalar(sCom);
            int iRows = -1;
            if (int.TryParse(result.ToString(), out iRows)) iRows = Convert.ToInt32(result);
            if (iRows > 0) blNew = false;
            return blNew;
        }

        public bool IsNewAddressID(string sTableName, object oAddressId)
        {
            bool blNew = true;
            int AddressId = -1;
            if (int.TryParse(oAddressId.ToString(), out AddressId)) AddressId = Convert.ToInt32(oAddressId); else return true;

            string sCom = @"SELECT COUNT(*) as rba FROM " + sTableName + " WHERE address_id=" + AddressId;
            object result = ExecuteScalar(sCom);
            int iRows = -1;
            if (int.TryParse(result.ToString(), out iRows)) iRows = Convert.ToInt32(result);
            if (iRows > 0) blNew = false;
            return blNew;
        }

        public bool IsNewOrderID(string sTableName, object oOrderId)
        {
            bool blNew = true;
            int OrderId = -1;
            if (int.TryParse(oOrderId.ToString(), out OrderId)) OrderId = Convert.ToInt32(oOrderId); else return true;

            string sCom = @"SELECT COUNT(*) as rba FROM " + sTableName + " WHERE order_id=" + OrderId;
            object result = ExecuteScalar(sCom);
            int iRows = -1;
            if (int.TryParse(result.ToString(), out iRows)) iRows = Convert.ToInt32(result);
            if (iRows > 0) blNew = false;
            return blNew;
        }

        public object ExecuteScalar(string sQuery)
        {
            object result = null;
            try
            {
                int iResult = -1;
                OpenConnection();
                _cmd.CommandText = sQuery;
                result = _cmd.ExecuteScalar();

                if (int.TryParse(result.ToString(), out iResult)) iResult = Convert.ToInt32(result);
                return iResult;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return 0; }
            finally
            {
                CloseConnection();
            }

        }

        public int ExecuteNon(string sQuery)
        {
            try
            {
                int iResult = -1;
                _cmd.CommandText = sQuery;
                OpenConnection();
                iResult = _cmd.ExecuteNonQuery();

                return iResult;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return 0; }
            finally
            {
                CloseConnection();
             }
            
        }

        public bool IsValidValue(DataColumn col, object value)
        {
            bool isValid = false;

            string sType = col.DataType.Name;

            switch (sType)
            {
                case "Int32":
                    int i_val = -1;
                    if (int.TryParse(value.ToString(), out i_val)) isValid = true;
                    break;
                case "String":
                    if (value.ToString().Length>0) isValid = true;
                    break;
                case "Double":
                    double d_val = 0;
                    if (double.TryParse(value.ToString(), out d_val)) isValid = true;
                    break;
                case "DateTime":
                    DateTime dt1908 = new DateTime(1899,1,1,0,0,0);
                    if (DateTime.TryParse(value.ToString(), out dt1908)) isValid = true;
                    break;
            }

            return isValid;
        }

        public DataTable fillTable(string sSQLSelect)
        {
            DataTable dtbElements = new DataTable();
            
            try
            {
                OpenConnection();

                _cmd.CommandText = sSQLSelect;
                _adapter.SelectCommand = (DbCommand)_cmd;

                _adapter.Fill(dtbElements);

                return dtbElements;
            }
            catch (Exception ex) { Console.WriteLine(""); return dtbElements; }
            finally
            {
                CloseConnection();
            }
            
        }
    }
}
