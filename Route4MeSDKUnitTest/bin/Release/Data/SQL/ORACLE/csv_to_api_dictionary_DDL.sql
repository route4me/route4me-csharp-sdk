/*
Database              : xe
Target Server Type    : ORACLE
Date: 2017-04-04 4:05 PM
*/

BEGIN
FOR tt IN (SELECT table_name as tname
                FROM all_tables WHERE TABLE_NAME='CSV_TO_API_DICTIONARY' AND OWNER='R4ME')
        LOOP
          EXECUTE IMMEDIATE 'DROP TABLE CSV_TO_API_DICTIONARY';
        END LOOP;
END;
/

-- ----------------------------
-- Table structure for csv_to_api_dictionary
-- ----------------------------

CREATE TABLE CSV_TO_API_DICTIONARY (
  "ID" NUMBER(7,0) NOT NULL,
  "csv_field_nom" NUMBER(7,0),
  "r4m_csv_field_name" VARCHAR2(64),
  "api_field_name" VARCHAR2(64),
  "table_name" VARCHAR2(64),
  "csv_field_type" VARCHAR2(64),
  "api_field_type" VARCHAR2(64),
  "comment" VARCHAR2(32),
  CONSTRAINT "CSV_TO_API_DICTIONARY_PK" PRIMARY KEY ("ID") ENABLE
);

COMMENT ON COLUMN "R4ME"."CSV_TO_API_DICTIONARY"."csv_field_nom" IS 'field order number in csv exported file';
COMMENT ON COLUMN "R4ME"."CSV_TO_API_DICTIONARY"."r4m_csv_field_name" IS 'Field name in the exported csv file from Route4Me web UI';
COMMENT ON COLUMN "R4ME"."CSV_TO_API_DICTIONARY"."api_field_name" IS 'The field name in the Route4Me API';
COMMENT ON COLUMN "R4ME"."CSV_TO_API_DICTIONARY"."table_name" IS 'api_filed belongs to a table with table_name';
