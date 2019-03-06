/*
Database              : r4me_db
Target Server Type    : SQL and MS Access
Date: 2017-03-27 19:26:20
*/

SET FOREIGN_KEY_CHECKS=0;
-- ----------------------------
-- Table structure for `orders`
-- ----------------------------
DROP TABLE IF EXISTS `orders`;
CREATE TABLE `orders` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `order_id` int(11) DEFAULT NULL COMMENT 'Order ID in a Route4Me account',
  `order_status_id` int(11) DEFAULT NULL COMMENT 'Order status ID',
  `address_1` varchar(512) DEFAULT NULL COMMENT 'Address 1',
  `address_2` varchar(512) DEFAULT NULL COMMENT 'Address 2',
  `member_id` int(11) DEFAULT NULL COMMENT 'An user ID in a Route4Me account',
  `cached_lat` double DEFAULT NULL COMMENT 'Cached latitude',
  `cached_lng` double DEFAULT NULL COMMENT 'Cached longitude',
  `color` varchar(10) DEFAULT NULL COMMENT 'e.g.  fa573c',
  `curbside_lat` double DEFAULT NULL COMMENT 'Curbside latitude',
  `curbside_lng` double DEFAULT NULL COMMENT 'Curbside longitude',
  `day_added_YYMMDD` varchar(16) DEFAULT NULL COMMENT 'When was inserted the order',
  `day_scheduled_for_YYMMDD` varchar(16) DEFAULT NULL COMMENT 'Date order was scheduled for',
  `EXT_FIELD_custom_data` text COMMENT 'A custom data of the order',
  `address_alias` varchar(128) DEFAULT NULL,
  `local_time_window_start` int(11) DEFAULT NULL COMMENT 'UNIX time',
  `local_time_window_end` int(11) DEFAULT NULL COMMENT 'UNIX time',
  `local_time_window_start_2` int(11) DEFAULT NULL COMMENT 'UNIX time',
  `local_time_window_end_2` int(11) DEFAULT NULL COMMENT 'UNIX time',
  `service_time` int(11) DEFAULT NULL COMMENT 'UNIX time',
  `EXT_FIELD_first_name` varchar(64) DEFAULT NULL COMMENT 'A first name of an order owner',
  `EXT_FIELD_last_name` varchar(64) DEFAULT NULL COMMENT 'A last name of an order owner',
  `EXT_FIELD_email` varchar(128) DEFAULT NULL COMMENT 'Order email',
  `EXT_FIELD_phone` varchar(24) DEFAULT NULL COMMENT 'Order phone number',
  `address_city` varchar(128) DEFAULT NULL COMMENT 'The city the location is located in',
  `address_state_id` varchar(10) DEFAULT NULL COMMENT 'The state ID the address is located in',
  `address_country_id` varchar(10) DEFAULT NULL COMMENT 'The country the address is located in',
  `address_zip` varchar(20) DEFAULT NULL COMMENT 'The zip code the address is located in',
  `order_icon` varchar(128) DEFAULT NULL COMMENT 'e.g.  emoji/emoji-bank',
  PRIMARY KEY (`id`),
  KEY `ord_sql_id` (`id`)
) AUTO_INCREMENT=0;

-- ----------------------------
-- Records of orders
-- ----------------------------
