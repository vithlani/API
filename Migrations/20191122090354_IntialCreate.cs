using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseCRUD.Migrations
{
    public partial class IntialCreate : Migration
    { 
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "courseDetails",
                columns: table => new
                {
                    CourseDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Duration = table.Column<string>(nullable: true),
                    Fees = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courseDetails", x => x.CourseDetailId);
                });

            migrationBuilder.CreateTable(
                name: "subjects",
                columns: table => new
                {
                    SubjectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sname = table.Column<string>(nullable: true),
                    SCredit = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CourseDetailId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjects", x => x.SubjectId);
                    table.ForeignKey(
                        name: "FK_subjects_courseDetails_CourseDetailId",
                        column: x => x.CourseDetailId,
                        principalTable: "courseDetails",
                        principalColumn: "CourseDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_subjects_CourseDetailId",
                table: "subjects",
                column: "CourseDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "subjects");

            migrationBuilder.DropTable(
                name: "courseDetails");
        }
    }
}
