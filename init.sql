CREATE TABLE `user_accounts` (
	`id` BIGINT(20) NOT NULL AUTO_INCREMENT,
	`account_created` DATETIME NOT NULL,
	`email` VARCHAR(320) NOT NULL COLLATE 'utf8mb4_general_ci',
	`username` VARCHAR(255) NOT NULL COLLATE 'utf8mb3_general_ci',
	`password` VARCHAR(255) NOT NULL COLLATE 'utf8mb4_general_ci',
	`email_verified` BOOLEAN NOT NULL,
	`password_reset_required` BOOLEAN NOT NULL,
	`account_locked` BOOLEAN NOT NULL,
	`account_banned` BOOLEAN NOT NULL,
	PRIMARY KEY (`id`) USING BTREE
)
COLLATE='utf8mb3_general_ci'
ENGINE=InnoDB;

CREATE TABLE `permission_categories` (
	`id` BIGINT(20) NOT NULL AUTO_INCREMENT,
	`name` VARCHAR(255) NOT NULL COLLATE 'utf8mb3_general_ci',
	PRIMARY KEY (`id`) USING BTREE
)
COLLATE='utf8mb3_general_ci'
ENGINE=InnoDB;

CREATE TABLE `permission_nodes` (
	`id` BIGINT(20) NOT NULL,
	`category_id` BIGINT(20) NOT NULL DEFAULT '0',
	`name` VARCHAR(255) NOT NULL DEFAULT '' COLLATE 'utf8mb3_general_ci',
	PRIMARY KEY (`id`) USING BTREE,
	INDEX `FK__permission_categories` (`category_id`) USING BTREE,
	CONSTRAINT `FK__permission_categories` FOREIGN KEY (`category_id`) REFERENCES `lucentrp`.`permission_categories` (`id`) ON UPDATE NO ACTION ON DELETE CASCADE
)
COLLATE='utf8mb3_general_ci'
ENGINE=InnoDB;

CREATE TABLE `permission_assignments` (
	`id` BIGINT(20) NOT NULL,
	`user_account_id` BIGINT(20) NOT NULL,
	`permission_node_id` BIGINT(20) NOT NULL,
	PRIMARY KEY (`id`) USING BTREE,
	CONSTRAINT `FK__permission_nodes` FOREIGN KEY (`id`) REFERENCES `lucentrp`.`permission_nodes` (`id`) ON UPDATE NO ACTION ON DELETE CASCADE,
	CONSTRAINT `FK__user_accounts` FOREIGN KEY (`id`) REFERENCES `lucentrp`.`user_accounts` (`id`) ON UPDATE NO ACTION ON DELETE CASCADE
)
COLLATE='utf8mb3_general_ci'
ENGINE=InnoDB;
