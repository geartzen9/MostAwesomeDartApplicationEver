using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MostAwesomeDartApplicationEver.Migrations
{
    public partial class FK_Improvments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Legs_Darters_WinnerId",
                table: "Legs");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Darters_WinnerId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Rounds_Darters_WinnerId",
                table: "Rounds");

            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Darters_WinnerId",
                table: "Sets");

            migrationBuilder.AlterColumn<int>(
                name: "WinnerId",
                table: "Sets",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "WinnerId",
                table: "Rounds",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "WinnerId",
                table: "Matches",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "WinnerId",
                table: "Legs",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Legs_Darters_WinnerId",
                table: "Legs",
                column: "WinnerId",
                principalTable: "Darters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Darters_WinnerId",
                table: "Matches",
                column: "WinnerId",
                principalTable: "Darters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rounds_Darters_WinnerId",
                table: "Rounds",
                column: "WinnerId",
                principalTable: "Darters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_Darters_WinnerId",
                table: "Sets",
                column: "WinnerId",
                principalTable: "Darters",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Legs_Darters_WinnerId",
                table: "Legs");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Darters_WinnerId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Rounds_Darters_WinnerId",
                table: "Rounds");

            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Darters_WinnerId",
                table: "Sets");

            migrationBuilder.AlterColumn<int>(
                name: "WinnerId",
                table: "Sets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WinnerId",
                table: "Rounds",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WinnerId",
                table: "Matches",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WinnerId",
                table: "Legs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Legs_Darters_WinnerId",
                table: "Legs",
                column: "WinnerId",
                principalTable: "Darters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Darters_WinnerId",
                table: "Matches",
                column: "WinnerId",
                principalTable: "Darters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rounds_Darters_WinnerId",
                table: "Rounds",
                column: "WinnerId",
                principalTable: "Darters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_Darters_WinnerId",
                table: "Sets",
                column: "WinnerId",
                principalTable: "Darters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
