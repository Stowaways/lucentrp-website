using FluentMigrator;

namespace LucentRP.Migrations.Migrations._2022
{
    // Created on 10.10.2022
    [Migration(3)]
    public class M3_ModifyPermissionConstraints : Migration
    {
        public override void Up()
        {
            Execute.Sql(
                @"ALTER TABLE 
                    `permission_assignments`
                  DROP FOREIGN KEY 
                    `FK__permission_nodes`,
                  DROP FOREIGN KEY 
                    `FK__user_accounts`;"
            );

            Execute.Sql(
                @"ALTER TABLE 
                    `permission_assignments`
	              ADD CONSTRAINT 
                    `FK__permission_nodes` FOREIGN KEY(`permission_node_id`) REFERENCES `lucentrp`.`permission_nodes` (`id`) ON UPDATE NO ACTION ON DELETE CASCADE,
	              ADD CONSTRAINT 
                    `FK__user_accounts` FOREIGN KEY(`user_account_id`) REFERENCES `lucentrp`.`user_accounts` (`id`) ON UPDATE NO ACTION ON DELETE CASCADE;"
            );

            Execute.Sql(
                @"ALTER TABLE 
                    `permission_nodes`
	              ADD UNIQUE INDEX 
                    `UN__name` (`name`) USING BTREE;"
            );

            Execute.Sql(
                @"ALTER TABLE 
                    `permission_categories`
	              ADD UNIQUE INDEX 
                    `UN__name` (`name`) USING BTREE;"
            );
        }

        public override void Down()
        {
            Execute.Sql(
                @"ALTER TABLE
                    `permission_assignments`
                  DROP FOREIGN KEY
                    `FK__permission_nodes`
                  DROP FOREIGN KEY
                    `FK__user_accounts`;"
            );

            Execute.Sql(
                @"ALTER TABLE
                    `permission_assignments`
                  ADD CONSTRAINT 
                    `FK__permission_nodes` FOREIGN KEY (`id`) REFERENCES `lucentrp`.`permission_nodes` (`id`) ON UPDATE NO ACTION ON DELETE CASCADE,
	              ADD CONSTRAINT 
                    `FK__user_accounts` FOREIGN KEY (`id`) REFERENCES `lucentrp`.`user_accounts` (`id`) ON UPDATE NO ACTION ON DELETE CASCADE;"
            );

            Execute.Sql(
                @"ALTER TABLE 
                    `permission_nodes`
	              DROP INDEX 
                     `UN__name`;"
            );

            Execute.Sql(
                @"ALTER TABLE 
                    `permission_categories`
	              DROP INDEX 
                     `UN__name`;"
            );
        }
    }
}
