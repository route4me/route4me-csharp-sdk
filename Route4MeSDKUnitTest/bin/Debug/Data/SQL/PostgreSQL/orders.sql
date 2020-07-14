/*
Database              : r4me_db
Target Server Type    : PostgreSQL
Date: 2017-03-29 4:41 PM
*/

-- ----------------------------
-- Table structure for orders
-- ----------------------------
DROP TABLE IF EXISTS orders;
DROP SEQUENCE IF EXISTS orders_id_seq;

CREATE TABLE orders (
  id integer NOT NULL,
  order_id integer NULL,
  order_status_id integer,
  address_1 character varying(256),
  address_2 character varying(256),
  member_id integer,
  cached_lat double precision,
  cached_lng double precision,
  color character varying(10),
  curbside_lat double precision,
  curbside_lng double precision,
  day_added_YYMMDD character varying(16),
  day_scheduled_for_YYMMDD character varying(16),
  EXT_FIELD_custom_data text,
  address_alias character varying(128),
  local_time_window_start integer,
  local_time_window_end integer,
  local_time_window_start_2 integer,
  local_time_window_end_2 integer,
  service_time integer,
  EXT_FIELD_first_name character varying(64),
  EXT_FIELD_last_name character varying(64),
  EXT_FIELD_email character varying(128),
  EXT_FIELD_phone character varying(24),
  address_city character varying(128),
  address_state_id character varying(10),
  address_country_id character varying(10),
  address_zip character varying(20),
  order_icon character varying(128),
  CONSTRAINT "PK_orders_id" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);

CREATE SEQUENCE orders_id_seq;
ALTER TABLE orders ALTER id SET DEFAULT NEXTVAL('orders_id_seq');

ALTER TABLE orders
  OWNER TO postgres;
COMMENT ON COLUMN orders.order_id IS 'Order ID in a Route4Me account';
COMMENT ON COLUMN orders.order_status_id IS 'Order status ID';
COMMENT ON COLUMN orders.address_1 IS 'Address 1';
COMMENT ON COLUMN orders.address_2 IS 'Address 2';
COMMENT ON COLUMN orders.member_id IS 'An user ID in a Route4Me account';
COMMENT ON COLUMN orders.cached_lat IS 'Cached latitude';
COMMENT ON COLUMN orders.cached_lng IS 'Cached longitude';
COMMENT ON COLUMN orders.color IS 'e.g.  fa573c';
COMMENT ON COLUMN orders.curbside_lat IS 'Curbside latitude';
COMMENT ON COLUMN orders.curbside_lng IS 'Curbside longitude';
COMMENT ON COLUMN orders.day_added_YYMMDD IS 'When was inserted the order';
COMMENT ON COLUMN orders.day_scheduled_for_YYMMDD IS 'Date order was scheduled for';
COMMENT ON COLUMN orders.EXT_FIELD_custom_data IS 'A custom data of the order';
COMMENT ON COLUMN orders.local_time_window_start IS 'UNIX time';
COMMENT ON COLUMN orders.local_time_window_end IS 'UNIX time';
COMMENT ON COLUMN orders.local_time_window_start_2 IS 'UNIX time';
COMMENT ON COLUMN orders.local_time_window_end_2 IS 'UNIX time';
COMMENT ON COLUMN orders.service_time IS 'UNIX time';
COMMENT ON COLUMN orders.EXT_FIELD_first_name IS 'A first name of an order owner';
COMMENT ON COLUMN orders.EXT_FIELD_last_name IS 'A last name of an order owner';
COMMENT ON COLUMN orders.EXT_FIELD_email IS 'Order email';
COMMENT ON COLUMN orders.EXT_FIELD_phone IS 'Order phone number';
COMMENT ON COLUMN orders.address_city IS 'The city the location is located in';
COMMENT ON COLUMN orders.address_state_id IS 'The state ID the address is located in';
COMMENT ON COLUMN orders.address_country_id IS 'The country the address is located in';
COMMENT ON COLUMN orders.address_zip IS 'The zip code the address is located in';
COMMENT ON COLUMN orders.order_icon IS 'e.g.  emoji/emoji-bank';
