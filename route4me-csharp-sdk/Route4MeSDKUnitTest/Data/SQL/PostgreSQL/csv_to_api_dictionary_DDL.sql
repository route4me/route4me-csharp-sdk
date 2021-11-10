/*
Database              : r4me_db
Target Server Type    : PostgreSQL
Date: 2017-03-29 5:26 PM
*/

-- ----------------------------
-- Table structure for csv_to_api_dictionary
-- ----------------------------

DROP TABLE IF EXISTS csv_to_api_dictionary;
DROP SEQUENCE IF EXISTS dictionary_id_seq;

CREATE TABLE csv_to_api_dictionary (
  id integer NOT NULL,
  csv_field_nom integer,
  r4m_csv_field_name character varying(64),
  api_field_name character varying(64),
  table_name character varying(64),
  csv_field_type character varying(64),
  api_field_type character varying(64),
  comment character varying(32),
  CONSTRAINT "PK_dictionary_id" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);

CREATE SEQUENCE dictionary_id_seq;
ALTER TABLE csv_to_api_dictionary ALTER id SET DEFAULT NEXTVAL('dictionary_id_seq');

ALTER TABLE csv_to_api_dictionary
  OWNER TO postgres;
COMMENT ON COLUMN csv_to_api_dictionary.csv_field_nom IS 'field order number in csv exported file';
COMMENT ON COLUMN csv_to_api_dictionary.r4m_csv_field_name IS 'Field name in the exported csv file from Route4Me web UI';
COMMENT ON COLUMN csv_to_api_dictionary.api_field_name IS 'The field name in the Route4Me API';
COMMENT ON COLUMN csv_to_api_dictionary.table_name IS 'api_filed belongs to a table with table_name';
