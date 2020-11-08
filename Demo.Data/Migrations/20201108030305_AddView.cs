using Microsoft.EntityFrameworkCore.Migrations;

namespace Demo.Data.Migrations
{
    public partial class AddView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE VIEW [dbo].[PlayerClubView]
    AS SELECT p.Id as PlayerId, p.Name as PlayerName, c.Name as ClubName
    FROM [dbo].[Players] as p
    INNER JOIN [dbo].[Clubs] as c
    ON p.ClubId = c.Id");

            migrationBuilder.Sql(
                @"CREATE PROCEDURE [dbo].[RemoveGamePlayersProcedure]
    @playerId int = 0
    AS 
    DELETE FROM [dbo].[GamePlayer] WHERE [PlayerId] = @playerId
    RETURN 0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"DROP PROCEDURE [dbo].[RemoveGamePlayersProcedure]");

            migrationBuilder.Sql(
                @"DROP VIEW [dbo].[PlayerClubView]");
        }
    }
}
