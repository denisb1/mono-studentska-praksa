using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Day6.Migrations
{
    public partial class TestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentDbs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentDbs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeacherDbs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Department = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherDbs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseDbs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseName = table.Column<string>(type: "text", nullable: true),
                    Ects = table.Column<double>(type: "double precision", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseDbs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseDbs_TeacherDbs_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "TeacherDbs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnrollmentDb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollmentDb_CourseDbs_CourseId",
                        column: x => x.CourseId,
                        principalTable: "CourseDbs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrollmentDb_StudentDbs_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentDbs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "StudentDbs",
                columns: new[] { "Id", "Age", "FirstName", "LastName", "Year" },
                values: new object[,]
                {
                    { new Guid("a4b20494-5489-4eef-ac76-eb30120b7fc5"), 21, "Peter", "Johnson", 2 },
                    { new Guid("fdcb6ced-7ccc-4591-8d4a-4deaa35a9d53"), 22, "Gregory", "Armstrong", 2 },
                    { new Guid("873cd274-2499-4c51-9140-630bbda12030"), 20, "Poe", "Wilson", 1 },
                    { new Guid("51e9879c-7082-4f19-87f9-723c3ba0b56b"), 25, "Luke", "Washington", 3 }
                });

            migrationBuilder.InsertData(
                table: "TeacherDbs",
                columns: new[] { "Id", "Department", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("29fcd3e8-b38f-46ea-ac43-aff0eac767f2"), "Data visualization", "Richard", "Jameson" },
                    { new Guid("263e12ef-4686-4695-bece-33220222a7f0"), "Robotics", "John", "Green" }
                });

            migrationBuilder.InsertData(
                table: "CourseDbs",
                columns: new[] { "Id", "CourseName", "Ects", "TeacherId" },
                values: new object[,]
                {
                    { new Guid("04dfa922-76cb-460c-91c5-c80cba7a35f3"), "Data structures", 6.0, new Guid("29fcd3e8-b38f-46ea-ac43-aff0eac767f2") },
                    { new Guid("29706822-8e3d-4e6f-a37e-7bd8e66524df"), "Mathematics", 5.0, new Guid("29fcd3e8-b38f-46ea-ac43-aff0eac767f2") },
                    { new Guid("4d303483-1232-4017-8c8c-8e1ac6cfcbe8"), "Programming 1", 4.5, new Guid("263e12ef-4686-4695-bece-33220222a7f0") }
                });

            migrationBuilder.InsertData(
                table: "EnrollmentDb",
                columns: new[] { "Id", "CourseId", "StudentId" },
                values: new object[,]
                {
                    { new Guid("d1db4e93-58ca-453c-8bc5-304aabaca885"), new Guid("04dfa922-76cb-460c-91c5-c80cba7a35f3"), new Guid("a4b20494-5489-4eef-ac76-eb30120b7fc5") },
                    { new Guid("0cce2076-068f-4ec2-b101-881cc1fa494f"), new Guid("04dfa922-76cb-460c-91c5-c80cba7a35f3"), new Guid("873cd274-2499-4c51-9140-630bbda12030") },
                    { new Guid("deaad0e9-5833-4447-b16a-1d9a36dedbfd"), new Guid("04dfa922-76cb-460c-91c5-c80cba7a35f3"), new Guid("51e9879c-7082-4f19-87f9-723c3ba0b56b") },
                    { new Guid("c47ac108-4738-401e-99b1-9a228c6eab86"), new Guid("29706822-8e3d-4e6f-a37e-7bd8e66524df"), new Guid("fdcb6ced-7ccc-4591-8d4a-4deaa35a9d53") },
                    { new Guid("aa8c2f58-dfaf-4722-8324-87ebe6113973"), new Guid("29706822-8e3d-4e6f-a37e-7bd8e66524df"), new Guid("873cd274-2499-4c51-9140-630bbda12030") },
                    { new Guid("8d5f6f9b-1d6a-4eae-ae66-19001db34817"), new Guid("4d303483-1232-4017-8c8c-8e1ac6cfcbe8"), new Guid("a4b20494-5489-4eef-ac76-eb30120b7fc5") },
                    { new Guid("de4321b3-cdcd-4c58-b6b3-6f58b5108092"), new Guid("4d303483-1232-4017-8c8c-8e1ac6cfcbe8"), new Guid("873cd274-2499-4c51-9140-630bbda12030") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseDbs_TeacherId",
                table: "CourseDbs",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentDb_CourseId",
                table: "EnrollmentDb",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentDb_StudentId",
                table: "EnrollmentDb",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrollmentDb");

            migrationBuilder.DropTable(
                name: "CourseDbs");

            migrationBuilder.DropTable(
                name: "StudentDbs");

            migrationBuilder.DropTable(
                name: "TeacherDbs");
        }
    }
}
