/*
Database              : xe
Target Server Type    : ORACLE
Date: 2017-04-04 12:10 PM
*/

BEGIN
FOR tt IN (SELECT table_name as tname
                FROM all_tables WHERE TABLE_NAME='ORDERS' AND OWNER='R4ME')
        LOOP
          EXECUTE IMMEDIATE 'DROP TABLE ORDERS';
        END LOOP;
END;
/

-- ----------------------------
-- Table structure for orders
-- ----------------------------
CREATE TABLE orders (
  "ID" NUMBER(7,0) NOT NULL,
  "order_id" NUMBER(7,0),
  "order_status_id" NUMBER(7,0),
  "address_1" VARCHAR2(256),
  "address_2" VARCHAR2(256),
  "member_id" NUMBER(7,0),
  "cached_lat" NUMBER(10,7),
  "cached_lng" NUMBER(10,7),
  "color" VARCHAR2(10),
  "curbside_lat" NUMBER(10,7),
  "curbside_lng" NUMBER(10,7),
  "day_added_YYMMDD" VARCHAR2(16),
  "day_scheduled_for_YYMMDD" VARCHAR2(16),
  "EXT_FIELD_custom_data" NCLOB,
  "address_alias" VARCHAR2(128),
  "local_time_window_start" NUMBER(7,0),
  "local_time_window_end" NUMBER(7,0),
  "local_time_window_start_2" NUMBER(7,0),
  "local_time_window_end_2" NUMBER(7,0),
  "service_time" NUMBER(7,0),
  "EXT_FIELD_first_name" VARCHAR2(64),
  "EXT_FIELD_last_name" VARCHAR2(64),
  "EXT_FIELD_email" VARCHAR2(128),
  "EXT_FIELD_phone" VARCHAR2(24),
  "address_city" VARCHAR2(128),
  "address_state_id" VARCHAR2(10),
  "address_country_id" VARCHAR2(10),
  "address_zip" VARCHAR2(20),
  "order_icon" VARCHAR2(128),
  CONSTRAINT "ORDERS_PK" PRIMARY KEY ("ID") ENABLE
);

BEGIN
FOR cc IN (SELECT sequence_name as sequence_exists 
                FROM all_sequences
                WHERE SEQUENCE_OWNER='R4ME' and sequence_name = 'ORDERS_SEQ')
          LOOP
             EXECUTE IMMEDIATE 'DROP SEQUENCE ORDERS_SEQ';
          END LOOP;
END;
/

CREATE SEQUENCE   "ORDERS_SEQ"  MINVALUE 1 MAXVALUE 9999999999999999 INCREMENT BY 1 START WITH 1 NOCACHE  NOORDER  NOCYCLE ;

CREATE OR REPLACE TRIGGER  "ORDERS_T1" 
AFTER
insert on "ORDERS"
begin
if "ID" is null then 
    select "ORDERS_SEQ".nextval into "ID" from dual; 
  end if; 
end;
/
ALTER TRIGGER  "ORDERS_T1" ENABLE;

COMMENT ON COLUMN "R4ME"."ORDERS"."order_id" IS 'Order ID in a Route4Me account';
COMMENT ON COLUMN "R4ME"."ORDERS"."order_status_id" IS 'Order status ID';
COMMENT ON COLUMN "R4ME"."ORDERS"."address_1" IS 'Address 1';
COMMENT ON COLUMN "R4ME"."ORDERS"."address_2" IS 'Address 2';
COMMENT ON COLUMN "R4ME"."ORDERS"."member_id" IS 'An user ID in a Route4Me account';
COMMENT ON COLUMN "R4ME"."ORDERS"."cached_lat" IS 'Cached latitude';
COMMENT ON COLUMN "R4ME"."ORDERS"."cached_lng" IS 'Cached longitude';
COMMENT ON COLUMN "R4ME"."ORDERS"."color" IS 'e.g.  fa573c';
COMMENT ON COLUMN "R4ME"."ORDERS"."curbside_lat" IS 'Curbside latitude';
COMMENT ON COLUMN "R4ME"."ORDERS"."curbside_lng" IS 'Curbside longitude';
COMMENT ON COLUMN "R4ME"."ORDERS"."day_added_YYMMDD" IS 'When was inserted the order';
COMMENT ON COLUMN "R4ME"."ORDERS"."day_scheduled_for_YYMMDD" IS 'Date order was scheduled for';
COMMENT ON COLUMN "R4ME"."ORDERS"."EXT_FIELD_custom_data IS" 'A custom data of the order';
COMMENT ON COLUMN "R4ME"."ORDERS"."local_time_window_start" IS 'UNIX time';
COMMENT ON COLUMN "R4ME"."ORDERS"."local_time_window_end" IS 'UNIX time';
COMMENT ON COLUMN "R4ME"."ORDERS"."local_time_window_start_2" IS 'UNIX time';
COMMENT ON COLUMN "R4ME"."ORDERS"."local_time_window_end_2" IS 'UNIX time';
COMMENT ON COLUMN "R4ME"."ORDERS"."service_time" IS 'UNIX time';
COMMENT ON COLUMN "R4ME"."ORDERS"."EXT_FIELD_first_name" IS 'A first name of an order owner';
COMMENT ON COLUMN "R4ME"."ORDERS"."EXT_FIELD_last_name" IS 'A last name of an order owner';
COMMENT ON COLUMN "R4ME"."ORDERS"."EXT_FIELD_email" IS 'Order email';
COMMENT ON COLUMN "R4ME"."ORDERS"."EXT_FIELD_phone" IS 'Order phone number';
COMMENT ON COLUMN "R4ME"."ORDERS"."address_city" IS 'The city the location is located in';
COMMENT ON COLUMN "R4ME"."ORDERS"."address_state_id" IS 'The state ID the address is located in';
COMMENT ON COLUMN "R4ME"."ORDERS"."address_country_id" IS 'The country the address is located in';
COMMENT ON COLUMN "R4ME"."ORDERS"."address_zip" IS 'The zip code the address is located in';
COMMENT ON COLUMN "R4ME"."ORDERS"."order_icon" IS 'e.g.  emoji/emoji-bank';
