USE [r4me_db];

/*
Database              : r4me_db
Target Server Type    : MSSQL
Date: 3/29/2017 12:11:26 PM
*/

SET ANSI_NULLS ON;

SET QUOTED_IDENTIFIER ON;

IF OBJECT_ID('[dbo].[csv_to_api_dictionary]', 'U') IS NOT NULL
  DROP TABLE [dbo].[csv_to_api_dictionary];

CREATE TABLE [dbo].[csv_to_api_dictionary](
	[id] [int] NOT NULL,
	[csv_field_nom] [int] NULL,
	[r4m_csv_field_name] [varchar](64) NULL,
	[api_field_name] [varchar](64) NULL,
	[table_name] [varchar](64) NULL,
	[csv_field_type] [varchar](64) NULL,
	[api_field_type] [varchar](64) NULL,
	[comment] [varchar](32) NULL,
 CONSTRAINT [PK_csv_to_api_dictionary] PRIMARY KEY CLUSTERED 
(	[id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'field order number in csv exported file' , 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'csv_to_api_dictionary', 
	@level2type=N'COLUMN',
	@level2name=N'csv_field_nom';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'Field name in the exported csv file from Route4Me web UI' , 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'csv_to_api_dictionary', 
	@level2type=N'COLUMN',
	@level2name=N'r4m_csv_field_name';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'The field name in the Route4Me API' , 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'csv_to_api_dictionary', 
	@level2type=N'COLUMN',
	@level2name=N'api_field_name';
	
EXEC sys.sp_addextendedproperty @name=N'MS_Description', 
	@value=N'api_filed belongs to a table with table_name' , 
	@level0type=N'SCHEMA',
	@level0name=N'dbo', 
	@level1type=N'TABLE',
	@level1name=N'csv_to_api_dictionary', 
	@level2type=N'COLUMN',
	@level2name=N'table_name';