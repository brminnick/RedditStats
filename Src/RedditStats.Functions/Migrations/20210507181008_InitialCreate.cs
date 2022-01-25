using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RedditStats.Functions.Migrations;

public partial class InitialCreate : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			name: "Submissions",
			columns: table => new
			{
				RedditUri = table.Column<string>(type: "nvarchar(450)", nullable: false),
				Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
				Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
				Subreddit = table.Column<string>(type: "nvarchar(max)", nullable: false),
				SubmittedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
				UpVoteRatio = table.Column<double>(type: "float", nullable: false),
				UpVotes = table.Column<int>(type: "int", nullable: false),
				DownVotes = table.Column<int>(type: "int", nullable: false),
				CommentCount = table.Column<int>(type: "int", nullable: false),
				IsAwarded = table.Column<bool>(type: "bit", nullable: false),
				CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
				UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Submissions", x => x.RedditUri);
			});
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "Submissions");
	}
}