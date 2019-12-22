using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace fiQuiz.Migrations
{
    public partial class AddedAnsweredAt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AnsweredAt",
                table: "QuizQuestions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ShowedAt",
                table: "QuizQuestions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnsweredAt",
                table: "QuizQuestions");

            migrationBuilder.DropColumn(
                name: "ShowedAt",
                table: "QuizQuestions");
        }
    }
}
