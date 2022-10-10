using FluentMigrator;

namespace LucentRP.Migrations.Migrations._2022
{
    // Created on 10.10.2022
    [Migration(4)]
    public class M4_FixAutoincrementOnPrimaryKeyIDColumns : Migration
    {
        public override void Up()
        {
            Execute.Sql(
                "SET FOREIGN_KEY_CHECKS = 0;"
            );

            Execute.Sql(
                @"ALTER TABLE 
                    `permission_assignments`
	              CHANGE COLUMN 
                    `id` `id` BIGINT(20) NOT NULL AUTO_INCREMENT FIRST;"
            );

            Execute.Sql(
                "SET FOREIGN_KEY_CHECKS = 1;"
            );
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
