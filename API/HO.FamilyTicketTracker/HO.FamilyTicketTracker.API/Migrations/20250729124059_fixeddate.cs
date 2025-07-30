using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HO.FamilyTicketTracker.API.Migrations
{
    /// <inheritdoc />
    public partial class fixeddate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "LookupValues",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3876),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "LookupValues",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3580),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3580), new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3876) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3580), new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3876) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3580), new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3876) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3580), new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3876) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3580), new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3876) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3580), new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3876) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3580), new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3876) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3580), new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3876) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3580), new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3876) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3580), new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3876) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3580), new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3876) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3580), new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3876) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3580), new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3876) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3580), new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3876) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3580), new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3876) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3580), new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3876) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3580), new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3876) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3580), new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3876) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3580), new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3876) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3580), new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3876) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "LookupValues",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3876));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "LookupValues",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 7, 29, 12, 40, 59, 435, DateTimeKind.Utc).AddTicks(3580));

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "LookupValues",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
