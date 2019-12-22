using Microsoft.EntityFrameworkCore.Migrations;

namespace fiQuiz.Migrations
{
    public partial class Joker1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuizQuestionId",
                table: "QuizUsedJokers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_QuizUsedJokers_QuizQuestionId",
                table: "QuizUsedJokers",
                column: "QuizQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizUsedJokers_QuizQuestions_QuizQuestionId",
                table: "QuizUsedJokers",
                column: "QuizQuestionId",
                principalTable: "QuizQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizUsedJokers_QuizQuestions_QuizQuestionId",
                table: "QuizUsedJokers");

            migrationBuilder.DropIndex(
                name: "IX_QuizUsedJokers_QuizQuestionId",
                table: "QuizUsedJokers");

            migrationBuilder.DropColumn(
                name: "QuizQuestionId",
                table: "QuizUsedJokers");
        }
    }
}
