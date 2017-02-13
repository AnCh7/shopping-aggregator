DROP DATABASE IF EXISTS shop_aggregator;
CREATE DATABASE shop_aggregator;
USE shop_aggregator;

GRANT USAGE ON *.* TO 'adminuser'@'localhost';
DROP USER 'adminuser'@'localhost';

CREATE USER 'adminuser'@'localhost' IDENTIFIED BY 'adminuserpass';
GRANT ALL PRIVILEGES ON shop_aggregator.* To 'adminuser'@'localhost' IDENTIFIED BY 'adminuserpass';

DROP TABLE IF EXISTS shop;
CREATE TABLE  shop (
  shop_id SMALLINT UNSIGNED NOT NULL auto_increment,
  name varchar(50) CHARACTER SET 'utf8' NOT NULL,
  address varchar(100) CHARACTER SET 'utf8' NOT NULL,
  working_hours varchar(20) CHARACTER SET 'utf8' NOT NULL,
  PRIMARY KEY  (shop_id)
)
ENGINE=INNODB;
GRANT SELECT, UPDATE ON shop_aggregator.shop TO 'adminuser'@'%' IDENTIFIED BY 'adminuserpass';
GRANT SELECT, UPDATE ON shop_aggregator.shop TO 'adminuser'@'localhost' IDENTIFIED BY 'adminuserpass';


DROP TABLE IF EXISTS product;
CREATE TABLE  product (
  product_id SMALLINT UNSIGNED NOT NULL auto_increment,
  name varchar(100) CHARACTER SET 'utf8' NOT NULL,
  description varchar(1000) CHARACTER SET 'utf8' NOT NULL,
  PRIMARY KEY  (product_id)
)
ENGINE=INNODB;
GRANT SELECT ON shop_aggregator.product TO 'adminuser'@'%' IDENTIFIED BY 'adminuserpass';
GRANT SELECT ON shop_aggregator.product TO 'adminuser'@'localhost' IDENTIFIED BY 'adminuserpass';


DROP TABLE IF EXISTS shop_product;
CREATE TABLE  shop_product (
  id SMALLINT UNSIGNED NOT NULL auto_increment,
  shop_id SMALLINT UNSIGNED NOT NULL,
  product_id SMALLINT UNSIGNED NOT NULL,
  PRIMARY KEY  (id),
  CONSTRAINT `fk_shop_id` FOREIGN KEY (shop_id) REFERENCES shop (shop_id) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `fk_product_id` FOREIGN KEY (product_id) REFERENCES product (product_id) ON DELETE RESTRICT ON UPDATE CASCADE
)
ENGINE=INNODB;
GRANT SELECT, UPDATE ON shop_aggregator.shop_product TO 'adminuser'@'%' IDENTIFIED BY 'adminuserpass';
GRANT SELECT, UPDATE ON shop_aggregator.shop_product TO 'adminuser'@'localhost' IDENTIFIED BY 'adminuserpass';
