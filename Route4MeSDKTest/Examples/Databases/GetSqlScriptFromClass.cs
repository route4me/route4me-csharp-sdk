using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route4MeSDK.DataTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void GetSqlScriptFromClass()
        {
            cDatabase sqlDB = new cDatabase(DB_Type.MSSQL);

            DataTable tblActivity = cDatabase.ClassToDatatable<AddressManifest>();

            String sqlScript = cDatabase.CreateTableSqlScript(tblActivity);

            Console.WriteLine(sqlScript);

        }
    }
}
