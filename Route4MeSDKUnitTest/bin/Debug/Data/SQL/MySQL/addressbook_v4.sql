/*
Database              : r4me_db
Target Server Type    : MySQL
Date: 2017-03-27 19:26:02
*/

SET FOREIGN_KEY_CHECKS=0;
-- ----------------------------
-- Table structure for `addressbook_v4`
-- ----------------------------
DROP TABLE IF EXISTS `addressbook_v4`;
CREATE TABLE `addressbook_v4` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `territory_name` varchar(256) DEFAULT NULL,
  `created_timestamp` bigint(11) DEFAULT NULL COMMENT 'When addressbook contact was created (UNIX time)',
  `address_id` int(11) DEFAULT NULL COMMENT 'Address book location ID in a Route4Me account',
  `address_1` varchar(256) DEFAULT NULL COMMENT 'Address 1',
  `address_2` varchar(256) DEFAULT NULL COMMENT 'Address 2',
  `address_alias` varchar(256) DEFAULT NULL,
  `address_group` varchar(64) DEFAULT NULL,
  `member_id` int(11) DEFAULT NULL COMMENT 'An user ID in a Route4Me account',
  `first_name` varchar(64) DEFAULT NULL COMMENT 'A first name of a receiver at the location',
  `last_name` varchar(64) DEFAULT NULL COMMENT 'A last name of a receiver at the location',
  `address_email` varchar(64) DEFAULT NULL COMMENT 'Location email',
  `address_phone_number` varchar(24) DEFAULT NULL COMMENT 'Location phone number',
  `cached_lat` double DEFAULT NULL COMMENT 'Cached latitude',
  `cached_lng` double DEFAULT NULL COMMENT 'Cached longitude',
  `curbside_lat` double DEFAULT NULL COMMENT 'Curbside latitude',
  `curbside_lng` double DEFAULT NULL COMMENT 'Curbside longitude',
  `schedule` text COMMENT 'Schedule object represented by JSON string',
  `address_city` varchar(4) DEFAULT NULL COMMENT 'The city the location is located in',
  `address_state_id` varchar(10) DEFAULT NULL COMMENT 'The state ID the address is located in',
  `address_country_id` varchar(10) DEFAULT NULL COMMENT 'The country the address is located in',
  `address_zip` varchar(16) DEFAULT NULL COMMENT 'The zip code the address is located in',
  `schedule_blacklist` text COMMENT 'comma-delimited list of the dates',
  `in_route_count` int(11) DEFAULT NULL COMMENT 'In how many route is included the location',
  `last_visited_timestamp` int(11) DEFAULT NULL, 
  `last_routed_timestamp` int(11) DEFAULT NULL,
  `local_time_window_start` int(11) DEFAULT NULL,
  `local_time_window_end` int(11) DEFAULT NULL,
  `local_time_window_start_2` int(11) DEFAULT NULL,
  `local_time_window_end_2` int(11) DEFAULT NULL,
  `service_time` int(11) DEFAULT NULL COMMENT 'UNIX time',
  `local_timezone_string` varchar(64) DEFAULT NULL,
  `color` varchar(6) DEFAULT NULL COMMENT 'e.g.  fa573c',
  `address_icon` varchar(128) DEFAULT NULL COMMENT 'e.g.  emoji/emoji-bank',
  `address_custom_data` text COMMENT 'Address custom data as dictionary',
  PRIMARY KEY (`id`),
  KEY `contact_id` (`id`)
) AUTO_INCREMENT=0;

-- ----------------------------
-- Records of addressbook_v4
-- ----------------------------
