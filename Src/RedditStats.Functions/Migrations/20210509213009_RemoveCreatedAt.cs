using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RedditStats.Functions.Migrations;

public partial class RemoveCreatedAt : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			name: "CreatedAt",
			table: "Submissions");
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<DateTimeOffset>(
			name: "CreatedAt",
			table: "Submissions",
			type: "datetimeoffset",
			nullable: false,
			defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
	}
}