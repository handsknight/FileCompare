using Microsoft.EntityFrameworkCore.Migrations;

namespace FileCompare.Infastructure.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PercentageSimilarity",
                table: "FileCompareHistory");

            migrationBuilder.RenameColumn(
                name: "SimilarContent",
                table: "FileCompareHistory",
                newName: "SimilarityPercentage");

            migrationBuilder.RenameColumn(
                name: "SecondStudentFile",
                table: "FileCompareHistory",
                newName: "SimilarityContent");

            migrationBuilder.RenameColumn(
                name: "SecondStudentCode",
                table: "FileCompareHistory",
                newName: "SecondStudentName");

            migrationBuilder.RenameColumn(
                name: "FirstStudentFile",
                table: "FileCompareHistory",
                newName: "SecondStudentFileName");

            migrationBuilder.RenameColumn(
                name: "FirstStudentCode",
                table: "FileCompareHistory",
                newName: "FirstStudentName");

            migrationBuilder.AddColumn<string>(
                name: "FirstStudentFileName",
                table: "FileCompareHistory",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstStudentFileName",
                table: "FileCompareHistory");

            migrationBuilder.RenameColumn(
                name: "SimilarityPercentage",
                table: "FileCompareHistory",
                newName: "SimilarContent");

            migrationBuilder.RenameColumn(
                name: "SimilarityContent",
                table: "FileCompareHistory",
                newName: "SecondStudentFile");

            migrationBuilder.RenameColumn(
                name: "SecondStudentName",
                table: "FileCompareHistory",
                newName: "SecondStudentCode");

            migrationBuilder.RenameColumn(
                name: "SecondStudentFileName",
                table: "FileCompareHistory",
                newName: "FirstStudentFile");

            migrationBuilder.RenameColumn(
                name: "FirstStudentName",
                table: "FileCompareHistory",
                newName: "FirstStudentCode");

            migrationBuilder.AddColumn<double>(
                name: "PercentageSimilarity",
                table: "FileCompareHistory",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
