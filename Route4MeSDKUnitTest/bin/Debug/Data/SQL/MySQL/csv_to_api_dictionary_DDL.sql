/*
Database              : r4me_db
Target Server Type    : SQL and MS Access
Date: 2017-03-27 19:27:10
*/

SET FOREIGN_KEY_CHECKS=0;
-- ----------------------------
-- Table structure for `csv_to_api_dictionary`
-- ----------------------------
DROP TABLE IF EXISTS `csv_to_api_dictionary`;
CREATE TABLE `csv_to_api_dictionary` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `csv_field_nom` int(11) DEFAULT NULL COMMENT 'field order number in csv exported file',
  `r4m_csv_field_name` varchar(64) DEFAULT NULL COMMENT 'Field name in the exported csv file from Route4Me web UI',
  `api_field_name` varchar(64) DEFAULT NULL COMMENT 'The field name in the Route4Me API',
  `table_name` varchar(64) DEFAULT NULL COMMENT 'api_filed belongs to a table with table_name',
  `csv_field_type` varchar(64) DEFAULT NULL,
  `api_field_type` varchar(64) DEFAULT NULL,
  `comment` varchar(32) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `dict_id` (`id`)
) AUTO_INCREMENT=0;
