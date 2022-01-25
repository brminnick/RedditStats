using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RedditStats.Functions.Migrations;

public partial class UpdateCreatedAtUpdatedAtDateTimeOffset : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterColumn<DateTimeOffset>(
			name: "UpdatedAt",
			table: "Submissions",
			type: "datetimeoffset",
			nullable: false,
			oldClrType: typeof(DateTimeOffset),
			oldType: "datetimeoffset",
			oldDefaultValueSql: "SYSDATETIMEOFFSET()");

		migrationBuilder.AlterColumn<DateTimeOffset>(
			name: "CreatedAt",
			table: "Submissions",
			type: "datetimeoffset",
			nullable: false,
			oldClrType: typeof(DateTimeOffset),
			oldType: "datetimeoffset",
			oldDefaultValueSql: "SYSDATETIMEOFFSET()");
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterColumn<DateTimeOffset>(
			name: "UpdatedAt",
			table: "Submissions",
			type: "datetimeoffset",
			nullable: false,
			defaultValueSql: "SYSDATETIMEOFFSET()",
			oldClrType: typeof(DateTimeOffset),
			oldType: "datetimeoffset");

		migrationBuilder.AlterColumn<DateTimeOffset>(
			name: "CreatedAt",
			table: "Submissions",
			type: "datetimeoffset",
			nullable: false,
			defaultValueSql: "SYSDATETIMEOFFSET()",
			oldClrType: typeof(DateTimeOffset),
			oldType: "datetimeoffset");
	}
}
