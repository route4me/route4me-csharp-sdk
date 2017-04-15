/*
Database              : xe
Target Server Type    : ORACLE
Date: 2017-04-04 12:10 PM
*/

BEGIN
FOR tt IN (SELECT table_name as tname
                FROM all_tables WHERE TABLE_NAME='ADDRESSBOOK_V4' AND OWNER='R4ME')
        LOOP
          EXECUTE IMMEDIATE 'DROP TABLE ADDRESSBOOK_V4';
        END LOOP;
END;
/

CREATE TABLE  "ADDRESSBOOK_V4" 
   (	"ID" NUMBER(7,0) NOT NULL ENABLE, 
	"territory_name" VARCHAR2(256), 
	"created_timestamp" NUMBER(7,0), 
	"address_id" NUMBER(7,0), 
	"address_1" VARCHAR2(256), 
	"address_2" VARCHAR2(256), 
	"address_alias" VARCHAR2(256), 
	"address_group" VARCHAR2(50), 
	"member_id" NUMBER(7,0), 
	"first_name" VARCHAR2(64), 
	"last_name" VARCHAR2(64), 
	"address_email" VARCHAR2(64), 
	"address_phone_number" VARCHAR2(24), 
	"cached_lat" NUMBER(10,7), 
	"cached_lng" NUMBER(10,7), 
	"curbside_lat" NUMBER(10,7), 
	"curbside_lng" NUMBER(10,7), 
	"schedule" CLOB, 
	"address_city" VARCHAR2(64), 
	"address_state_id" VARCHAR2(10), 
	"address_country_id" VARCHAR2(10), 
	"address_zip" VARCHAR2(16), 
	"schedule_blacklist" CLOB, 
	"in_route_count" NUMBER(7,0), 
	"last_visited_timestamp" NUMBER(7,0), 
	"last_routed_timestamp" NUMBER(7,0), 
	"local_time_window_start" NUMBER(7,0), 
	"local_time_window_end" NUMBER(7,0), 
	"local_time_window_start_2" NUMBER(7,0), 
	"local_time_window_end_2" NUMBER(7,0), 
	"service_time" NUMBER(7,0), 
	"local_timezone_string" VARCHAR2(64), 
	"color" VARCHAR2(6), 
	"address_icon" VARCHAR2(128), 
	"address_custom_data" NCLOB, 
	 CONSTRAINT "ADDRESSBOOK_V4_PK" PRIMARY KEY ("ID") ENABLE
   ) ;
   
BEGIN
FOR cc IN (SELECT sequence_name as sequence_exists 
                FROM all_sequences
                WHERE SEQUENCE_OWNER='R4ME' and sequence_name = 'ADDRESSBOOK_V4_SEQ')
          LOOP
             EXECUTE IMMEDIATE 'DROP SEQUENCE ADDRESSBOOK_V4_SEQ';
          END LOOP;
END;
/

CREATE SEQUENCE   "ADDRESSBOOK_V4_SEQ"  MINVALUE 1 MAXVALUE 9999999999999999 INCREMENT BY 1 START WITH 1 NOCACHE  NOORDER  NOCYCLE ;
   
CREATE OR REPLACE TRIGGER  "ADDRESSBOOK_V4_T1" 
AFTER
insert on "ADDRESSBOOK_V4"
begin
if "ID" is null then 
    select "ADDRESSBOOK_V4_SEQ".nextval into "ID" from dual; 
  end if; 
end;
/
ALTER TRIGGER  "ADDRESSBOOK_V4_T1" ENABLE;

COMMENT ON COLUMN "R4ME"."ADDRESSBOOK_V4"."created_timestamp" IS 'When addressbook contact was created (UNIX time)';
COMMENT ON COLUMN "R4ME"."ADDRESSBOOK_V4"."address_id" IS 'Address book location ID in a Route4Me account';
COMMENT ON COLUMN "R4ME"."ADDRESSBOOK_V4"."address_1" IS 'Address 1';
COMMENT ON COLUMN "R4ME"."ADDRESSBOOK_V4"."address_2" IS 'Address 2';
COMMENT ON COLUMN "R4ME"."ADDRESSBOOK_V4"."member_id" IS 'An user ID in a Route4Me account';
COMMENT ON COLUMN "R4ME"."ADDRESSBOOK_V4"."first_name" IS 'A first name of a receiver at the location';
COMMENT ON COLUMN "R4ME"."ADDRESSBOOK_V4"."last_name" IS 'A last name of a receiver at the location';
COMMENT ON COLUMN "R4ME"."ADDRESSBOOK_V4"."address_email" IS 'Location email';
COMMENT ON COLUMN "R4ME"."ADDRESSBOOK_V4"."address_phone_number" IS 'Location phone number';
COMMENT ON COLUMN "R4ME"."ADDRESSBOOK_V4"."cached_lat" IS 'Cached latitude';
COMMENT ON COLUMN "R4ME"."ADDRESSBOOK_V4"."cached_lng" IS 'Cached longitude';
COMMENT ON COLUMN "R4ME"."ADDRESSBOOK_V4"."curbside_lat" IS 'Curbside latitude';
COMMENT ON COLUMN "R4ME"."ADDRESSBOOK_V4"."curbside_lng" IS 'Curbside longitude';
COMMENT ON COLUMN "R4ME"."ADDRESSBOOK_V4"."schedule" IS 'Schedule object represented by JSON string';
COMMENT ON COLUMN "R4ME"."ADDRESSBOOK_V4"."address_city" IS 'The city the location is located in';
COMMENT ON COLUMN "R4ME"."ADDRESSBOOK_V4"."address_state_id" IS 'The state ID the address is located in';
COMMENT ON COLUMN "R4ME"."ADDRESSBOOK_V4"."address_country_id" IS 'The country the address is located in';
COMMENT ON COLUMN "R4ME"."ADDRESSBOOK_V4"."address_zip" IS 'The zip code the address is located in';
COMMENT ON COLUMN "R4ME"."ADDRESSBOOK_V4"."schedule_blacklist" IS 'comma-delimited list of the dates';
COMMENT ON COLUMN "R4ME"."ADDRESSBOOK_V4"."in_route_count" IS 'In how many route is included the location';
COMMENT ON COLUMN "R4ME"."ADDRESSBOOK_V4"."service_time" IS 'UNIX time';
COMMENT ON COLUMN "R4ME"."ADDRESSBOOK_V4"."color" IS 'Location color on a map';
/