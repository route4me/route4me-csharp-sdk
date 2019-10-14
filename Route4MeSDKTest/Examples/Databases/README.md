## Export/import the Route4Me account data to/from SQL database

In this folder you can find the examples of the export/import processes from/to the Route4Me data to/from SQL database.

These examples use cDatabase class (see link <a target="_blank" href="https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/cDatabase.cs">here</a>). You can use this class with all the SQL database engine types: MsSql, MySql, PostgreSql, Oracle, SQLite. You can use it with MS Access too.

You can see the sample files (the address book locations and orders in the CSV and JSON formats) for examples and testing in this <a target="_blank" href="https://github.com/route4me/route4me-csharp-sdk/tree/master/Route4MeSDKTest/bin/Debug/Data">folder</a>. 

In above mentioned folder you can find SQL script files for generating server side tables:
- addressbook_v4.sql - for the address book locations table;
- orders.sql - for the orders table;
- csv_to_api_dictionary.sql - for mapping sql table fields with the local file fileds.

In this folder you can find the examples:
- UploadCsvToAddressbookV4.cs (see link <a target="_blank" href="https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKTest/Examples/Databases/UploadCsvToAddressbookV4.cs">here</a>) - for uploading the exported CSV file from the Route4Me address book locations to the SQL server table **addressbook_v4**. If in the CSV table row is definded address_id field value and there is a row in the table **addressbook_v4** with same address_id, row will be updated, otherwise row will be inserted in the table **addressbook_v4**.
- UploadCsvToOrders.cs (see link <a target="_blank" href="https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKTest/Examples/Databases/UploadCsvToOrders.cs">here</a>) - for uploading the exported CSV file from the Route4Me orders to the SQL server table **orders**. If in the CSV table row is definded order_id field value and there is a row in the table **orders** with the same order_id, row will be updated, otherwise a row will be inserted in the table **orders**.
- MakeAddressbookCSVsample.cs (see link <a target="_blank" href="https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKTest/Examples/Databases/MakeAddressbookCSVsample.cs">here</a>) - for exporting SQL table **addressbook_v4** to the local csv file, which you can upload to your Route4Me address book locations.
- UploadAddressbookJSONtoSQL.cs (see link <a target="_blank" href="https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKTest/Examples/Databases/UploadAddressbookJSONtoSQL.cs">here</a>) - for uploading a downloaded JSON file from the Route4Me address book locations to the SQL table **addressbook_v4** - algorithm will define whether to insert or to update data as it's described above.
- UploadOrdersJSONtoSQL.cs (see link <a target="_blank" href="https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKTest/Examples/Databases/UploadOrdersJSONtoSQL.cs">here</a>) - for uploading a downloaded JSON file from the Route4Me orders to the SQL table **orders** - algorithm will define whether to insert or to update data as it's described above.
