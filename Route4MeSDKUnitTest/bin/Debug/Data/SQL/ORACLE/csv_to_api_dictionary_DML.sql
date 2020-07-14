/*
Database            : xe
Target Server Type    : ORACLE
Date: 2017-04-04 4:17 PM
*/

-- ----------------------------
-- Records of CSV_TO_API_DICTIONARY
-- ----------------------------
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('1', '0', 'Territory Name', 'territory_name', 'addressbook_v4', 'string', 'string', 'not api');
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('2', '1', 'created_timestamp', 'created_timestamp', 'addressbook_v4', 'datetime', 'int', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('3', '2', 'address_id', 'address_id', 'addressbook_v4', 'int', 'int', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('4', '3', 'group', 'address_group', 'addressbook_v4', 'string', 'string', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('5', '4', 'alias', 'address_alias', 'addressbook_v4', 'string', 'string', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('6', '5', 'address', 'address_1', 'addressbook_v4', 'string', 'string', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('7', '6', 'address_2', 'address_2', 'addressbook_v4', 'string', 'string', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('8', '7', 'member_id', 'member_id', 'addressbook_v4', 'int', 'int', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('9', '8', 'first_name', 'first_name', 'addressbook_v4', 'string', 'string', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('10', '9', 'last_name', 'last_name', 'addressbook_v4', 'string', 'string', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('11', '10', 'email', 'address_email', 'addressbook_v4', 'string', 'string', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('12', '11', 'phone', 'address_phone_number', 'addressbook_v4', 'string', 'string', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('13', '12', 'city', 'address_city', 'addressbook_v4', 'string', 'string', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('14', '13', 'state', 'address_state_id', 'addressbook_v4', 'string', 'string', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('15', '14', 'country_id', 'address_country_id', 'addressbook_v4', 'string', 'string', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('16', '15', 'zipcode', 'address_zip', 'addressbook_v4', 'string', 'string', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('17', '16', 'lat', 'cached_lat', 'addressbook_v4', 'double', 'double', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('18', '17', 'lng', 'cached_lng', 'addressbook_v4', 'double', 'double', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('19', '18', 'curbside_lat', 'curbside_lat', 'addressbook_v4', 'double', 'double', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('20', '19', 'curbside_lng', 'curbside_lng', 'addressbook_v4', 'double', 'double', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('21', '20', 'schedule', 'schedule', 'addressbook_v4', 'string', 'string', 'JSON string');
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('22', '21', 'schedule_blacklist', 'schedule_blacklist', 'addressbook_v4', 'string', 'string', 'date string array');
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('23', '22', 'in_route_count', 'in_route_count', 'addressbook_v4', 'int', 'int', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('24', '23', 'last_visited_timestamp', 'last_visited_timestamp', 'addressbook_v4', 'datetime', 'int', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('25', '24', 'last_routed_timestamp', 'last_routed_timestamp', 'addressbook_v4', 'datetime', 'int', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('26', '25', 'time_window_start', 'local_time_window_start', 'addressbook_v4', 'datetime', 'int', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('27', '26', 'time_window_end', 'local_time_window_end', 'addressbook_v4', 'datetime', 'int', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('28', '27', 'time_window_start_2', 'local_time_window_start_2', 'addressbook_v4', 'datetime', 'int', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('29', '28', 'time_window_end_2', 'local_time_window_end_2', 'addressbook_v4', 'datetime', 'int', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('30', '29', 'service_time', 'service_time', 'addressbook_v4', 'datetime', 'int', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('31', '30', 'timezone', 'local_timezone_string', 'addressbook_v4', 'string', 'string', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('32', '31', 'color', 'color', 'addressbook_v4', 'string', 'string', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('33', '32', 'address_icon', 'address_icon', 'addressbook_v4', 'string', 'string', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('34', null, 'order_id', 'order_id', 'orders', 'int', 'int', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('35', null, 'order_status_id', 'order_status_id', 'orders', 'int', 'int', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('36', '3', 'Address', 'address_1', 'orders', 'string', 'string', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('37', null, 'address_2', 'address_2', 'orders', 'string', 'string', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('38', null, 'member_id', 'member_id', 'orders', 'int', 'int', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('39', '1', 'Latitude', 'cached_lat', 'orders', 'double', 'double', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('40', '0', 'Longitude', 'cached_lng', 'orders', 'double', 'double', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('41', null, 'color', 'color', 'orders', 'string', 'string', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('42', null, 'curbside_lat', 'curbside_lat', 'orders', 'double', 'double', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('43', null, 'curbside_lng', 'curbside_lng', 'orders', 'double', 'double', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('44', '8', 'Schedule Date', 'day_scheduled_for_YYMMDD', 'orders', 'string', 'string', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('45', null, 'EXT_FIELD_custom_data', 'EXT_FIELD_custom_data', 'orders', 'string', 'string', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('46', '2', 'Address Alias', 'address_alias', 'orders', 'string', 'string', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('47', null, 'local_time_window_start', 'local_time_window_start', 'orders', 'datetime', 'int', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('48', null, 'local_time_window_end', 'local_time_window_end', 'orders', 'datetime', 'int', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('49', null, 'local_time_window_start_2', 'local_time_window_start_2', 'orders', 'datetime', 'int', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('50', null, 'local_time_window_end_2', 'local_time_window_end_2', 'orders', 'datetime', 'int', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('51', null, 'service_time', 'service_time', 'orders', 'datetime', 'int', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('52', '-1', 'EXT_FIELD_first_name', 'EXT_FIELD_first_name', 'orders', 'string', 'string', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('53', '-1', 'EXT_FIELD_last_name', 'EXT_FIELD_last_name', 'orders', 'string', 'string', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('54', '-1', 'EXT_FIELD_email', 'EXT_FIELD_email', 'orders', 'string', 'string', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('55', '7', 'Phone', 'EXT_FIELD_phone', 'orders', 'string', 'string', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('56', '4', 'City', 'address_city', 'orders', 'string', 'string', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('57', '5', 'State', 'address_state_id', 'orders', 'string', 'string', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('58', '-1', 'address_country_id', 'address_country_id', 'orders', 'string', 'string', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('59', '6', 'Zip Code', 'address_zip', 'orders', 'string', 'string', null);
INSERT INTO "R4ME"."CSV_TO_API_DICTIONARY" VALUES ('60', '-1', 'order_icon', 'order_icon', 'orders', 'string', 'string', null);
