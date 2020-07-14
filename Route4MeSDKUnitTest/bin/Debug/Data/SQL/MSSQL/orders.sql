USE [r4me_db];

/*
Database              : r4me_db
Target Server Type    : MSSQL
Script Date           : 3/28/2017 5:45:56 PM
*/

SET ANSI_NULLS ON;

SET QUOTED_IDENTIFIER ON;

IF OBJECT_ID('[dbo].[orders]', 'U') IS NOT NULL
  DROP TABLE [dbo].[orders];

CREATE TABLE [dbo].[orders](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[order_id] [int] NULL,
	[order_status_id] [int] NULL,
	[address_1] [nvarchar](256) NULL,
	[address_2] [nvarchar](256) NULL,
	[member_id] [int] NULL,
	[cached_lat] [float] NULL,
	[cached_lng] [float] NULL,
	[color] [varchar](6) NULL,
	[curbside_lat] [float] NULL,
	[curbside_lng] [float] NULL,
	[day_added_YYMMDD] [varchar](16) NULL,
	[day_scheduled_for_YYMMDD] [varchar](16) NULL,
	[EXT_FIELD_custom_data] [nvarchar](max) NULL,
	[address_alias] [nvarchar](128) NULL,
	[local_time_window_start] [int] NULL,
	[local_time_window_end] [int] NULL,
	[local_time_window_start_2] [int] NULL,
	[local_time_window_end_2] [int] NULL,
	[service_time] [int] NULL,
	[EXT_FIELD_first_name] [nvarchar](64) NULL,
	[EXT_FIELD_last_name] [nvarchar](64) NULL,
	[EXT_FIELD_email] [varchar](128) NULL,
	[EXT_FIELD_phone] [nvarchar](24) NULL,
	[address_city] [varchar](128) NULL,
	[address_state_id] [varchar](10) NULL,
	[address_country_id] [varchar](10) NULL,
	[address_zip] [varchar](10) NULL,
	[order_icon] [varchar](128) NULL,
 CONSTRAINT [PK_orders] PRIMARY KEY CLUSTERED 
(	[id] ASC ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'Order ID in a Route4Me account' , 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'orders', 
	@level2type=N'COLUMN',
	@level2name=N'order_id';

EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'Order status ID' , 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'orders', 
	@level2type=N'COLUMN',
	@level2name=N'order_status_id';

EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'Address 1' , 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'orders', 
	@level2type=N'COLUMN',
	@level2name=N'address_1';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'Address 2' , 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'orders', 
	@level2type=N'COLUMN',
	@level2name=N'address_2';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'An user ID in a Route4Me account' , 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'orders', 
	@level2type=N'COLUMN',
	@level2name=N'member_id';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'Cached latitude' , 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'orders', 
	@level2type=N'COLUMN',
	@level2name=N'cached_lat';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'Cached longitude' , 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'orders', 
	@level2type=N'COLUMN',
	@level2name=N'cached_lng';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'e.g. fa573c' , 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'orders', 
	@level2type=N'COLUMN',
	@level2name=N'color';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'Curbside latitude' , 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'orders', 
	@level2type=N'COLUMN',
	@level2name=N'curbside_lat';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'Curbside longitude' , 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'orders', 
	@level2type=N'COLUMN',
	@level2name=N'curbside_lng';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'When was inserted the order' , 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'orders', 
	@level2type=N'COLUMN',
	@level2name=N'day_added_YYMMDD';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'Date order was scheduled for' , 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'orders', 
	@level2type=N'COLUMN',
	@level2name=N'day_scheduled_for_YYMMDD';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'A custom data of the order' , 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'orders', 
	@level2type=N'COLUMN',
	@level2name=N'EXT_FIELD_custom_data';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'UNIX time' , 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'orders', 
	@level2type=N'COLUMN',
	@level2name=N'local_time_window_start';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'UNIX time' , 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'orders', 
	@level2type=N'COLUMN',
	@level2name=N'local_time_window_end';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'UNIX time' , 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'orders', 
	@level2type=N'COLUMN',
	@level2name=N'local_time_window_start_2';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'UNIX time' , 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'orders', 
	@level2type=N'COLUMN',
	@level2name=N'local_time_window_end_2';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'UNIX time' , 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'orders', 
	@level2type=N'COLUMN',
	@level2name=N'service_time';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'A first name of an order owner' , 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'orders', 
	@level2type=N'COLUMN',
	@level2name=N'EXT_FIELD_first_name';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'A last name of an order owner' , 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'orders', 
	@level2type=N'COLUMN',
	@level2name=N'EXT_FIELD_last_name';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'Order email', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'orders', 
	@level2type=N'COLUMN',
	@level2name=N'EXT_FIELD_email';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'Order phone number', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'orders', 
	@level2type=N'COLUMN',
	@level2name=N'EXT_FIELD_phone';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'The city the location is located in', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'orders', 
	@level2type=N'COLUMN',
	@level2name=N'address_city';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'The state ID the address is located in', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'orders', 
	@level2type=N'COLUMN',
	@level2name=N'address_state_id';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'The country the address is located in', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'orders', 
	@level2type=N'COLUMN',
	@level2name=N'address_country_id';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'The zip code the address is located in', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'orders', 
	@level2type=N'COLUMN',
	@level2name=N'address_zip';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'e.g.  emoji/emoji-bank', 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'orders', 
	@level2type=N'COLUMN',
	@level2name=N'order_icon';