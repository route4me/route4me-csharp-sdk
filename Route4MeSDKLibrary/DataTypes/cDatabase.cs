using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlServerCe;
using System.Web.Script;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// Route4Me data types
    /// </summary>
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

    /// <summary>
    /// Database types
    /// </summary>
    public enum DB_Type
    {
        MSSQL,
        SQLCE,
        MySQL,
        PostgreSQL,
        SQLite,
        MS_Access
    }

    /// <summary>
    /// The databases wrapper class
    /// </summary>
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

        /// <summary>
        /// The class constructor.
        /// </summary>
        /// <param name="db_type">See <see cref="DB_Type"/></param>
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
                case DB_Type.SQLCE:
                    _conStngInstitute = ConfigurationManager.ConnectionStrings["conSQLCE"];
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

        /// <summary>
        /// True if a cDatabase type object is disposed.
        /// </summary>
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

        /// <summary>
        /// Disposes a cDatabase type object.
        /// </summary>
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

        /// <summary>
        /// Cleans up the cDatabase object variables.
        /// </summary>
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

        /// <summary>
        /// Opens a database connection.
        /// </summary>
        public void OpenConnection()
        {
            try
            {
                if (_con.State != ConnectionState.Open) _con.Open();
            }
            catch (Exception ex) { Console.WriteLine("Connection not established!.. "+ex.Message); }
        }

        /// <summary>
        /// Closes a database connection.
        /// </summary>
        public void CloseConnection()
        {
            if (_con.State != ConnectionState.Closed) _con.Close();
        }

        /// <summary>
        /// Parsing of the multi-command SQL texts, omiting commentaries and extracting of pure SQL commands.
        /// <para>Note:</para>
        /// <para>- after semicolon ';' shouldn't be written anything (blank spaces allowed)</para>
        /// <para>- befor '/*' shouldn't be written anything (blank spaces allowed)</para>
        /// </summary>
        /// <param name="sQuery">The database request SQL string.</param>
        /// <returns></returns>
        public int ExecuteMulticoomandSql(string sQuery)
        {
            int iRet = 0;
            try
            {
                sQuery = sQuery.Replace(";", ";^");
                string[] arCommands = sQuery.Split('^');
                if (_con.State != ConnectionState.Open) OpenConnection();
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

                iRet = 1;

                return iRet;
            }
            catch (Exception ex)
            {
                Console.WriteLine(":( Transaction failed... " + ex.Message); _transaction.Rollback();
                iRet = 0; return iRet;
            }
            
        }

        /// <summary>
        /// Table for correspondance between Route4Me CSV exported file fields and Route4Me API fields.
        /// </summary>
        /// <param name="sTableName">The dictionary table name.</param>
        /// <returns>The dictionary datatable.</returns>
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

        /// <summary>
        /// Method for importing an addressbook CSV file 
        /// (with structure equal to exported by Route4Me web UI CSV file)
        /// to an addressbook table on the SQL type server.
        /// </summary>
        /// <param name="sFileName">CSV file name</param>
        /// <param name="sTableName">Server addressbook table name</param>
        /// <param name="sIdName">The name of the ID column of the server addressbook table (it differs from address_id and you need it for editing prior uploading to the Route4Me server).</param>
        /// <param name="iFieldsNumber">The fields number of the CSV file.</param>
        /// <param name="isFirstRowHeader">If true, the first row of the CSV file is a header.</param>
        public void Csv2Table(string sFileName, string sTableName, string sIdName, int iFieldsNumber, bool isFirstRowHeader)
        {
            if (!File.Exists(sFileName))
            {
                Console.WriteLine("The file " + sFileName + " doesn't exist...");
                return;
            }

            string header = isFirstRowHeader ? "Yes" : "No";

            string pathOnly = Path.GetDirectoryName(sFileName);
            string fileName = Path.GetFileName(sFileName);

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

                                    sList += (oSchedule == null) ? sFieldApiName + "=null," 
                                        : sFieldApiName + "='" + oSchedule + "',";
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

                                if (sFieldApiName == "day_scheduled_for_YYMMDD")
                                {
                                    sList += sFieldApiName + ",";

                                    var oSchedule = ExceptFields2QueryValue(sFieldApiName, row[iCol]);

                                    sValues += (oSchedule == null) ? "null" : "'" + oSchedule + "',";
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
                                                    sValues += (_conStngInstitute.ProviderName == "System.Data.OleDb") 
                                                        ? "#" + dt1900.ToString("yyyy-MM-dd HH:mm:ss") + "#," 
                                                        : "'" + dt1900.ToString("yyyy-MM-dd HH:mm:ss") + "',";
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
                        Console.WriteLine(":( Can not create new row in the table "+sTableName);
                    }
                }
            }
        }

        /// <summary>
        /// Method for exporting addressbook data from SQL type server to the CSV file
        /// (with structure equal to the CSV file exported by Route4Me web UI).
        /// </summary>
        /// <param name="sFileName">CSV file name</param>
        /// <param name="sTableName">Server addressbook table name</param>
        /// <param name="WithId">If true, CSV file will have first ID of SQL addressbook table 
        /// (you need it for editing in CSV file and updating server table using Csv2Table method).</param>
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

            #region Convert JSON string of the custom data to the CSV fields (as they are represented in the exported CSV file from Route4Me).
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
                                case "System.DateTime":
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

            Console.WriteLine("The file "+sFileName+" was created. You can fill it with data for uploading to the server.");
        }

        /// <summary>
        /// Creates the data folder if it does not exist.
        /// </summary>
        /// <param name="file_name">A file name the folder contains.</param>
        /// <param name="blCreateIfNotExists">If true and the folder does not exist, it will be created.</param>
        /// <returns>True if the folder exists (or created successfully).</returns>
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

        /// <summary>
        /// Converts the value of the type ttype to a value for SQL query operations (update, insert).
        /// </summary>
        /// <param name="ttype">The value type.</param>
        /// <param name="oValue">The field value.</param>
        /// <returns></returns>
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
                        sQueryValue = (_conStngInstitute.ProviderName == "System.Data.OleDb") 
                            ? "#" + dt1900.ToString("yyyy-MM-dd HH:mm:ss") + "#" 
                            : "'" + dt1900.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    }
                    break;
            }

            return sQueryValue;
        }

        /// <summary>
        /// Converts complex fields to the SQL query value.
        /// </summary>
        /// <param name="PropertyName">The object property name.</param>
        /// <param name="oValue">The object value.</param>
        /// <returns>The SQL query value.</returns>
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
                    }
                    else sQueryValue=null;
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
                    foreach (KeyValuePair<string, string> kvpair in (Dictionary<string, string>)oValue)
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

        /* Upload the JSON response file generated by the process of getting address book contacts through Route4Me API to the SQL type server.
         * */
        /// <summary>
        /// Converts JSON file to the database table according to the Route4Me object type.
        /// </summary>
        /// <param name="sFileName">The JSON filename.</param>
        /// <param name="sTableName">The database table name.</param>
        /// <param name="sIdName">The name of the ID column.</param>
        /// <param name="r4m_dtype">A Route4Me object type.</param>
        public void Json2Table(string sFileName, string sTableName, string sIdName, R4M_DataType r4m_dtype)
        {
            if (!File.Exists(sFileName))
            {
                Console.WriteLine("The file " + sFileName + " doesn't exist...");
                return;
            }

            string jsonContent = File.ReadAllText(sFileName);

            var jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            switch (r4m_dtype)
            {
                case R4M_DataType.Addressbook:
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

                                Console.WriteLine("Property type=" + prop.PropertyType.Name);

                                sFields += prop.Name + ",";

                                sValues += (prop.Name == "address_custom_data" || prop.Name == "schedule" || prop.Name == "schedule_blacklist") 
                                    ? "'" + ExceptFields2QueryValue(prop.Name, vValue) + "'," 
                                    : FieldValue2QueryValue(vValue.GetType(), vValue) + ",";
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
                                
                                Console.WriteLine("Property type=" + prop.PropertyType.Name);

                                sSet += (prop.Name == "address_custom_data" || prop.Name == "schedule" || prop.Name == "schedule_blacklist") 
                                    ? prop.Name + "='" + ExceptFields2QueryValue(prop.Name, vValue) + "',"
                                    : prop.Name + "=" + FieldValue2QueryValue(vValue.GetType(), vValue) + ",";
                            }

                            sSet = sSet.TrimEnd(',');
                            sQuery += sSet + " WHERE address_id=" + address_id;
                        }
                        ExecuteNon(sQuery);
                        
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
                                if (prop.Name == "ConvertBooleansToInteger") continue;
                                if (prop.MemberType != System.Reflection.MemberTypes.Property) continue;

                                var vValue = order.GetType().GetProperty(prop.Name).GetValue(order, null);
                                if (vValue == null) continue;

                                Console.WriteLine("Property type=" + prop.PropertyType.Name);

                                sFields += prop.Name + ",";

                                sValues += (prop.Name == "EXT_FIELD_custom_data") 
                                    ? "'" + ExceptFields2QueryValue(prop.Name, vValue) + "'," 
                                    : FieldValue2QueryValue(vValue.GetType(), vValue) + ",";
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

                                Console.WriteLine("Property type=" + prop.PropertyType.Name);

                                sSet += (prop.Name == "EXT_FIELD_custom_data") 
                                    ? prop.Name + "='" + ExceptFields2QueryValue(prop.Name, vValue) + "'," 
                                    : prop.Name + "=" + FieldValue2QueryValue(vValue.GetType(), vValue) + ",";
                            }

                            sSet = sSet.TrimEnd(',');
                            sQuery += sSet + " WHERE order_id=" + order_id;
                        }

                        ExecuteNon(sQuery);
                    }
                    
                    break;
                case R4M_DataType.Route:

                    break;
            }
        }

        /// <summary>
        /// Checks if the address is new in the datatable.
        /// </summary>
        /// <param name="sTableName">The table name.</param>
        /// <param name="sIdName">The ID column name.</param>
        /// <param name="AddressId">The address ID.</param>
        /// <returns>True if the address is new.</returns>
        public bool IsNewAddress(string sTableName, string sIdName, int AddressId)
        {
            string sCom = @"SELECT COUNT(*) as rba FROM "+sTableName+ " WHERE "+sIdName+"="+AddressId;

            object result = ExecuteScalar(sCom);

            int iRows = -1;
            if (int.TryParse(result.ToString(), out iRows)) iRows = Convert.ToInt32(result);

            return (iRows > 0) ? false : true;
        }

        /// <summary>
        /// Checks if the address ID is new in the datatable.
        /// </summary>
        /// <param name="sTableName">The table name.</param>
        /// <param name="oAddressId">The address ID.</param>
        /// <returns>True if the address is new.</returns>
        public bool IsNewAddressID(string sTableName, object oAddressId)
        {
            int AddressId = -1;
            if (int.TryParse(oAddressId.ToString(), out AddressId)) AddressId = Convert.ToInt32(oAddressId); else return true;

            string sCom = @"SELECT COUNT(*) as rba FROM " + sTableName + " WHERE address_id=" + AddressId;

            object result = ExecuteScalar(sCom);

            int iRows = -1;
            if (int.TryParse(result.ToString(), out iRows)) iRows = Convert.ToInt32(result);

            return (iRows > 0) ? false : true;
        }

        /// <summary>
        /// Checks if the order ID is new in the datatable.
        /// </summary>
        /// <param name="sTableName">The table name.</param>
        /// <param name="oOrderId">The order ID.</param>
        /// <returns>True if the order is new.</returns>
        public bool IsNewOrderID(string sTableName, object oOrderId)
        {
            int OrderId = -1;
            if (int.TryParse(oOrderId.ToString(), out OrderId)) OrderId = Convert.ToInt32(oOrderId); else return true;

            string sCom = @"SELECT COUNT(*) as rba FROM " + sTableName + " WHERE order_id=" + OrderId;

            object result = ExecuteScalar(sCom);

            int iRows = -1;
            if (int.TryParse(result.ToString(), out iRows)) iRows = Convert.ToInt32(result);

            return (iRows > 0) ? false : true;
        }

        /// <summary>
        /// Executes scalar SQL request.
        /// </summary>
        /// <param name="sQuery">SQL query text.</param>
        /// <returns>Scalar object</returns>
        public object ExecuteScalar(string sQuery)
        {
            object result = null;

            try
            {
                OpenConnection();
                _cmd.CommandText = sQuery;
                result = _cmd.ExecuteScalar();

                int iResult = -1;
                if (int.TryParse(result.ToString(), out iResult)) iResult = Convert.ToInt32(result);

                return iResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); return 0;
            }
            finally
            {
                CloseConnection();
            }

        }

        /// <summary>
        /// Executes a non-query request.
        /// </summary>
        /// <param name="sQuery">SQL request text.</param>
        /// <returns>The number of affected rows.</returns>
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

        /// <summary>
        /// Checks if the object is a valid type value.
        /// </summary>
        /// <param name="col">Datacolumn</param>
        /// <param name="value">An object value.</param>
        /// <returns>True if the value is a valid type.</returns>
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

        /// <summary>
        /// Creates and fills a datatable by SQL query.
        /// </summary>
        /// <param name="sSQLSelect">SQL query text.</param>
        /// <returns>The datatable.</returns>
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
            catch (Exception ex) { Console.WriteLine(ex.Message); return dtbElements; }
            finally
            {
                CloseConnection();
            }
            
        }
    }
}
