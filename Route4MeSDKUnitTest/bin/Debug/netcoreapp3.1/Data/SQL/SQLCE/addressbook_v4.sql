/*
Database              : r4me_db
Target Server Type    : SQLCE
Script Date           : 4/15/2017 10:46 PM
*/

CREATE TABLE [addressbook_v4](
	[id] int NOT NULL IDENTITY PRIMARY KEY,
	[territory_name] nvarchar(256),
	[created_timestamp] int,
	[address_id] int,
	[address_1] nvarchar(256),
	[address_2] nvarchar(256),
	[address_alias] nvarchar(256),
	[address_group] nvarchar(50),
	[member_id] int,
	[first_name] nvarchar(64),
	[last_name] nvarchar(64),
	[address_email] nvarchar(64),
	[address_phone_number] nvarchar(24),
	[cached_lat] float,
	[cached_lng] float,
	[curbside_lat] float,
	[curbside_lng] float,
	[schedule] ntext,
	[address_city] nvarchar(64),
	[address_state_id] nvarchar(10),
	[address_country_id] nvarchar(10),
	[address_zip] nvarchar(16),
	[schedule_blacklist] ntext,
	[in_route_count] int,
	[last_visited_timestamp] int,
	[last_routed_timestamp] int,
	[local_time_window_start] int,
	[local_time_window_end] int,
	[local_time_window_start_2] int,
	[local_time_window_end_2] int,
	[service_time] int,
	[local_timezone_string] nvarchar(64),
	[color] nvarchar(6),
	[address_icon] nvarchar(128),
	[address_custom_data] ntext);
	