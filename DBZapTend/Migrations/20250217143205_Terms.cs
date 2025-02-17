using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBZapTend.Migrations
{
    /// <inheritdoc />
    public partial class Terms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Plan_PlanId",
                schema: "mydb",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "PlanId",
                schema: "mydb",
                table: "User",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<bool>(
                name: "TermsAccepted",
                schema: "mydb",
                table: "User",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TermsVersion",
                schema: "mydb",
                table: "User",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Plan_PlanId",
                schema: "mydb",
                table: "User",
                column: "PlanId",
                principalSchema: "mydb",
                principalTable: "Plan",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Plan_PlanId",
                schema: "mydb",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TermsAccepted",
                schema: "mydb",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TermsVersion",
                schema: "mydb",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "PlanId",
                schema: "mydb",
                table: "User",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Plan_PlanId",
                schema: "mydb",
                table: "User",
                column: "PlanId",
                principalSchema: "mydb",
                principalTable: "Plan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
