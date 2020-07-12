/*
Database              : r4me_db
Target Server Type    : MSSQL
Script Date           : 3/28/2017 5:45:56 PM
*/

CREATE TABLE [orders](
	[id] int NOT NULL IDENTITY PRIMARY KEY,
	[order_id] int,
	[order_status_id] int,
	[address_1] nvarchar(256),
	[address_2] nvarchar(256),
	[member_id] int,
	[cached_lat] float,
	[cached_lng] float,
	[color] nvarchar(6),
	[curbside_lat] float,
	[curbside_lng] float,
	[day_added_YYMMDD] nvarchar(16),
	[day_scheduled_for_YYMMDD] nvarchar(16),
	[EXT_FIELD_custom_data] ntext,
	[address_alias] nvarchar(128),
	[local_time_window_start] int,
	[local_time_window_end] int,
	[local_time_window_start_2] int,
	[local_time_window_end_2] int,
	[service_time] int,
	[EXT_FIELD_first_name] nvarchar(64),
	[EXT_FIELD_last_name] nvarchar(64),
	[EXT_FIELD_email] nvarchar(128),
	[EXT_FIELD_phone] nvarchar(24),
	[address_city] nvarchar(128),
	[address_state_id] nvarchar(10),
	[address_country_id] nvarchar(10),
	[address_zip] nvarchar(10),
	[order_icon] nvarchar(128));