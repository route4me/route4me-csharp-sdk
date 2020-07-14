/*
Database              : r4me_db
Target Server Type    : PostgreSQL
Script Date           : 3/28/2017 3:54:56 PM
*/

DROP TABLE IF EXISTS addressbook_v4;
DROP SEQUENCE IF EXISTS addressbook_id_seq;

CREATE TABLE addressbook_v4
(
  id integer NOT NULL,
  territory_name character varying(256),
  created_timestamp integer,
  address_id integer,
  address_1 character varying(256),
  address_2 character varying(256),
  address_alias character varying(256),
  address_group character varying(50),
  member_id integer,
  first_name character varying(64),
  last_name character varying(64),
  address_email character varying(64),
  address_phone_number character varying(24),
  cached_lat double precision,
  cached_lng double precision,
  curbside_lat double precision,
  curbside_lng double precision,
  schedule text,
  address_city character varying(64),
  address_state_id character varying(10),
  address_country_id character varying(10),
  address_zip character varying(16),
  schedule_blacklist text,
  in_route_count integer,
  last_visited_timestamp integer,
  last_routed_timestamp integer,
  local_time_window_start integer,
  local_time_window_end integer,
  local_time_window_start_2 integer,
  local_time_window_end_2 integer,
  service_time integer,
  local_timezone_string character varying(64),
  color character varying(6),
  address_icon character varying(128),
  address_custom_data text,
  CONSTRAINT "PK_addressbook_id" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);

CREATE SEQUENCE addressbook_id_seq;
ALTER TABLE addressbook_v4 ALTER id SET DEFAULT NEXTVAL('addressbook_id_seq');

ALTER TABLE addressbook_v4
  OWNER TO postgres;
COMMENT ON COLUMN addressbook_v4.created_timestamp IS 'When addressbook contact was created (UNIX time)';
COMMENT ON COLUMN addressbook_v4.address_id IS 'Address book location ID in a Route4Me account';
COMMENT ON COLUMN addressbook_v4.address_1 IS 'Address 1';
COMMENT ON COLUMN addressbook_v4.address_2 IS 'Address 2';
COMMENT ON COLUMN addressbook_v4.member_id IS 'An user ID in a Route4Me account';
COMMENT ON COLUMN addressbook_v4.first_name IS 'A first name of a receiver at the location';
COMMENT ON COLUMN addressbook_v4.last_name IS 'A last name of a receiver at the location';
COMMENT ON COLUMN addressbook_v4.address_email IS 'Location email';
COMMENT ON COLUMN addressbook_v4.address_phone_number IS 'Location phone number';
COMMENT ON COLUMN addressbook_v4.cached_lat IS 'Cached latitude';
COMMENT ON COLUMN addressbook_v4.cached_lng IS 'Cached longitude';
COMMENT ON COLUMN addressbook_v4.curbside_lat IS 'Curbside latitude';
COMMENT ON COLUMN addressbook_v4.curbside_lng IS 'Curbside longitude';
COMMENT ON COLUMN addressbook_v4.schedule IS 'Schedule object represented by JSON string';
COMMENT ON COLUMN addressbook_v4.address_city IS 'The city the location is located in';
COMMENT ON COLUMN addressbook_v4.address_state_id IS 'The state ID the address is located in';
COMMENT ON COLUMN addressbook_v4.address_country_id IS 'The country the address is located in';
COMMENT ON COLUMN addressbook_v4.address_zip IS 'The zip code the address is located in';
COMMENT ON COLUMN addressbook_v4.schedule_blacklist IS 'comma-delimited list of the dates';
COMMENT ON COLUMN addressbook_v4.in_route_count IS 'In how many route is included the location';
COMMENT ON COLUMN addressbook_v4.service_time IS 'UNIX time';
COMMENT ON COLUMN addressbook_v4.color IS 'Location color on a map';