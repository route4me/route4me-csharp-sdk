USE [r4me_db];

/*
Database              : r4me_db
Target Server Type    : MSSQL
Script Date           : 3/28/2017 5:45:56 PM
*/

SET ANSI_NULLS ON;

SET QUOTED_IDENTIFIER ON;

IF OBJECT_ID('[dbo].[addressbook_v4]', 'U') IS NOT NULL
  DROP TABLE [dbo].[addressbook_v4]; 

CREATE TABLE [dbo].[addressbook_v4](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[territory_name] [nvarchar](256) NULL,
	[created_timestamp] [int] NULL,
	[address_id] [int] NULL,
	[address_1] [nvarchar](256) NULL,
	[address_2] [nvarchar](256) NULL,
	[address_alias] [nvarchar](256) NULL,
	[address_group] [nvarchar](50) NULL,
	[member_id] [int] NULL,
	[first_name] [nvarchar](64) NULL,
	[last_name] [nvarchar](64) NULL,
	[address_email] [nvarchar](64) NULL,
	[address_phone_number] [nvarchar](24) NULL,
	[cached_lat] [float] NULL,
	[cached_lng] [float] NULL,
	[curbside_lat] [float] NULL,
	[curbside_lng] [float] NULL,
	[schedule] [nvarchar](max) NULL,
	[address_city] [nvarchar](64) NULL,
	[address_state_id] [nvarchar](10) NULL,
	[address_country_id] [nvarchar](10) NULL,
	[address_zip] [varchar](16) NULL,
	[schedule_blacklist] [nvarchar](max) NULL,
	[in_route_count] [int] NULL,
	[last_visited_timestamp] [int] NULL,
	[last_routed_timestamp] [int] NULL,
	[local_time_window_start] [int] NULL,
	[local_time_window_end] [int] NULL,
	[local_time_window_start_2] [int] NULL,
	[local_time_window_end_2] [int] NULL,
	[service_time] [int] NULL,
	[local_timezone_string] [varchar](64) NULL,
	[color] [varchar](6) NULL,
	[address_icon] [nvarchar](128) NULL,
	[address_custom_data] [nvarchar](max) NULL,
	CONSTRAINT [PK_addressbook_v4] PRIMARY KEY CLUSTERED 
   ([id] ASC ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY])  ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
   
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'When addressbook contact was created (UNIX time)' , 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'addressbook_v4', 
	@level2type=N'COLUMN',
	@level2name=N'created_timestamp';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'Address book location ID in a Route4Me account', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'addressbook_v4', 
	@level2type=N'COLUMN',
	@level2name=N'address_id';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'Address 1', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'addressbook_v4', 
	@level2type=N'COLUMN',
	@level2name=N'address_1';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'Address 2', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'addressbook_v4', 
	@level2type=N'COLUMN',
	@level2name=N'address_2';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'An user ID in a Route4Me account', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'addressbook_v4', 
	@level2type=N'COLUMN',
	@level2name=N'member_id';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'A first name of a receiver at the location', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'addressbook_v4', 
	@level2type=N'COLUMN',
	@level2name=N'first_name';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'A last name of a receiver at the location', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'addressbook_v4', 
	@level2type=N'COLUMN',
	@level2name=N'last_name';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'Location email', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'addressbook_v4', 
	@level2type=N'COLUMN',
	@level2name=N'address_email';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'Location phone number', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'addressbook_v4', 
	@level2type=N'COLUMN',
	@level2name=N'address_phone_number';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'Cached latitude', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'addressbook_v4', 
	@level2type=N'COLUMN',
	@level2name=N'cached_lat';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'Cached longitude', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'addressbook_v4', 
	@level2type=N'COLUMN',
	@level2name=N'cached_lng';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'Curbside latitude', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'addressbook_v4', 
	@level2type=N'COLUMN',
	@level2name=N'curbside_lat';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'Curbside longitude', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'addressbook_v4', 
	@level2type=N'COLUMN',
	@level2name=N'curbside_lng';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'Schedule object represented by JSON string', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'addressbook_v4', 
	@level2type=N'COLUMN',
	@level2name=N'schedule';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'The city the location is located in', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'addressbook_v4', 
	@level2type=N'COLUMN',
	@level2name=N'address_city';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'The state ID the address is located in', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'addressbook_v4', 
	@level2type=N'COLUMN',
	@level2name=N'address_state_id';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'The country the address is located in', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'addressbook_v4', 
	@level2type=N'COLUMN',
	@level2name=N'address_country_id';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'The zip code the address is located in', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'addressbook_v4', 
	@level2type=N'COLUMN',
	@level2name=N'address_zip';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'comma-delimited list of the dates', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'addressbook_v4', 
	@level2type=N'COLUMN',
	@level2name=N'schedule_blacklist';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'In how many route is included the location', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'addressbook_v4', 
	@level2type=N'COLUMN',
	@level2name=N'in_route_count';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'UNIX time', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'addressbook_v4', 
	@level2type=N'COLUMN',
	@level2name=N'service_time';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'e.g.  fa573c', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'addressbook_v4', 
	@level2type=N'COLUMN',
	@level2name=N'color';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'e.g.  emoji/emoji-bank', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'addressbook_v4', 
	@level2type=N'COLUMN',
	@level2name=N'address_icon';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'Address custom data as dictionary', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'addressbook_v4', 
	@level2type=N'COLUMN',
	@level2name=N'address_custom_data';