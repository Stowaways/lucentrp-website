using FluentMigrator;

namespace LucentRP.Migrations.Migrations._2022
{
    // Created on 10.10.2022
    [Migration(5)]
    public class M5_ModifyUserAccountConstraints : Migration
    {
        public override void Up()
        {
            Execute.Sql(
                @"ALTER TABLE 
                    `user_accounts`
	              ADD UNIQUE INDEX 
                    `UN__email` (`email`) USING BTREE,
	              ADD UNIQUE INDEX 
                    `UN__username` (`username`) USING BTREE;"
            );
        }

        public override void Down()
        {
            Execute.Sql(
                @"ALTER TABLE 
                    `user_accounts`
	              DROP INDEX 
                    `UN__username`,
	              DROP 
                    INDEX `UN__email`;"
            );
        }
    }
}
