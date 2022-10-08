﻿using FluentMigrator;

namespace LucentRP.Migrations
{
    // Created on 08/10/2022.
    [Migration(1)]
    public class M1_AddUserAccountsTable : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
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
            ");
        }

        public override void Down()
        {
            Delete.Table("user_accounts");
        }
    }
}
