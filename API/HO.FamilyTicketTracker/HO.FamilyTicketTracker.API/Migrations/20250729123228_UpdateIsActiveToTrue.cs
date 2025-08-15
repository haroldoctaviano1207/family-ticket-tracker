using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HO.FamilyTicketTracker.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIsActiveToTrue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "LookupValues",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 5,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 6,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 7,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 8,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 9,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 10,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 11,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 12,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 13,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 14,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 15,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 16,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 17,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 18,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 19,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 20,
                column: "IsActive",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "LookupValues",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 5,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 6,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 7,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 8,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 9,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 10,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 11,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 12,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 13,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 14,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 15,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 16,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 17,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 18,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 19,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 20,
                column: "IsActive",
                value: false);
        }
    }
}
