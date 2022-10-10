using FluentMigrator;

namespace LucentRP.Migrations
{
    // Created on 08.10.2022
    [Migration(1)]
    public class M1_AddUserAccountsTable : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE `user_accounts` (
	                `id` BIGINT(20) NOT NULL AUTO_INCREMENT,
	                `account_created` DATETIME NOT NULL,
	                `email` VARCHAR(320) CHARACTER SET 'utf8mb4' NOT NULL COLLATE 'utf8mb4_unicode_ci',
	                `username` VARCHAR(255) CHARACTER SET 'utf8mb4' NOT NULL COLLATE 'utf8mb4_unicode_ci',
	                `password` VARCHAR(255) CHARACTER SET 'utf8mb4' NOT NULL COLLATE 'utf8mb4_unicode_ci',
	                `email_verified` BOOLEAN NOT NULL,
	                `password_reset_required` BOOLEAN NOT NULL,
	                `account_locked` BOOLEAN NOT NULL,
	                `account_banned` BOOLEAN NOT NULL,
	                PRIMARY KEY (`id`) USING BTREE
                )
                DEFAULT CHARSET=utf8mb4
                COLLATE utf8mb4_unicode_ci
                ENGINE=InnoDB;
            ");
        }

        public override void Down()
        {
            Delete.Table("user_accounts");
        }
    }
}
