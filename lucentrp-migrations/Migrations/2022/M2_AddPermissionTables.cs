using FluentMigrator;

namespace LucentRP.Migrations
{
    // Created on 08.10.2022
    [Migration(2)]
    public class M2_AddPermissionTables : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE `permission_categories` (
	                `id` BIGINT(20) NOT NULL AUTO_INCREMENT,
	                `name` VARCHAR(255) CHARACTER SET 'utf8mb4' NOT NULL COLLATE 'utf8mb4_unicode_ci',
	                PRIMARY KEY (`id`) USING BTREE
                )
                DEFAULT CHARSET=utf8mb4
                COLLATE utf8mb4_unicode_ci
                ENGINE=InnoDB;
            ");

            Execute.Sql(@"
                CREATE TABLE `permission_nodes` (
	                `id` BIGINT(20) NOT NULL,
	                `category_id` BIGINT(20) NOT NULL,
	                `name` VARCHAR(255) CHARACTER SET 'utf8mb4' NOT NULL COLLATE 'utf8mb4_unicode_ci',
	                PRIMARY KEY (`id`) USING BTREE,
	                INDEX `FK__permission_categories` (`category_id`) USING BTREE,
	                CONSTRAINT `FK__permission_categories` FOREIGN KEY (`category_id`) REFERENCES `lucentrp`.`permission_categories` (`id`) ON UPDATE NO ACTION ON DELETE CASCADE
                )
                DEFAULT CHARSET=utf8mb4
                COLLATE utf8mb4_unicode_ci
                ENGINE=InnoDB;
            ");

            Execute.Sql(@"
                CREATE TABLE `permission_assignments` (
	                `id` BIGINT(20) NOT NULL,
	                `user_account_id` BIGINT(20) NOT NULL,
	                `permission_node_id` BIGINT(20) NOT NULL,
	                PRIMARY KEY (`id`) USING BTREE,
	                CONSTRAINT `FK__permission_nodes` FOREIGN KEY (`id`) REFERENCES `lucentrp`.`permission_nodes` (`id`) ON UPDATE NO ACTION ON DELETE CASCADE,
	                CONSTRAINT `FK__user_accounts` FOREIGN KEY (`id`) REFERENCES `lucentrp`.`user_accounts` (`id`) ON UPDATE NO ACTION ON DELETE CASCADE
                )
                DEFAULT CHARSET=utf8mb4
                COLLATE utf8mb4_unicode_ci
                ENGINE=InnoDB;
            ");
        }

        public override void Down()
        {
            Delete.Table("permission_categories");
            Delete.Table("permission_nodes");
            Delete.Table("permission_assignments");
        }
    }
}
