/*
Database              : r4me_db
Target Server Type    : MSSQL
Date: 3/29/2017 12:11:26 PM
*/

CREATE TABLE [csv_to_api_dictionary](
	[id] int NOT NULL IDENTITY PRIMARY KEY,
	[csv_field_nom] int,
	[r4m_csv_field_name] nvarchar(64),
	[api_field_name] nvarchar(64),
	[table_name] nvarchar(64),
	[csv_field_type] nvarchar(64),
	[api_field_type] nvarchar(64),
	[comment] nvarchar(32));