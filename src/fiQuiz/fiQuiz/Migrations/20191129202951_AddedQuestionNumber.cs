using Microsoft.EntityFrameworkCore.Migrations;

namespace fiQuiz.Migrations
{
    public partial class AddedQuestionNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionNumber",
                table: "QuizQuestions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionNumber",
                table: "QuizQuestions");
        }
    }
}
