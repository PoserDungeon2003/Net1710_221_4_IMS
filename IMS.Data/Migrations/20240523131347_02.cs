using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class _02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    company_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.company_id);
                });

            migrationBuilder.CreateTable(
                name: "Mentor",
                columns: table => new
                {
                    mentor_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    full_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    job_position = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Mentor__E5D27EF313602FE1", x => x.mentor_id);
                    table.ForeignKey(
                        name: "FK_Mentor_Company",
                        column: x => x.company_id,
                        principalTable: "Company",
                        principalColumn: "company_id");
                });

            migrationBuilder.CreateTable(
                name: "Intern",
                columns: table => new
                {
                    intern_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    university = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    major = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    job_position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    education_background = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    experiences = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    working_tasks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mentor_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Intern__CE265C53DCACDD51", x => x.intern_id);
                    table.ForeignKey(
                        name: "FK_Intern_Company",
                        column: x => x.company_id,
                        principalTable: "Company",
                        principalColumn: "company_id");
                    table.ForeignKey(
                        name: "FK_Intern_Mentor1",
                        column: x => x.mentor_id,
                        principalTable: "Mentor",
                        principalColumn: "mentor_id");
                });

            migrationBuilder.CreateTable(
                name: "interviews_info",
                columns: table => new
                {
                    interviewinfo_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    time = table.Column<DateTime>(type: "datetime", nullable: false),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    result = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    intern_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__intervie__E4A9940E7D71A669", x => x.interviewinfo_id);
                    table.ForeignKey(
                        name: "FK_interviews_info_Intern2",
                        column: x => x.intern_id,
                        principalTable: "Intern",
                        principalColumn: "intern_id");
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    task_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    intern_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    mentor_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.task_id);
                    table.ForeignKey(
                        name: "FK_Task_Intern",
                        column: x => x.intern_id,
                        principalTable: "Intern",
                        principalColumn: "intern_id");
                    table.ForeignKey(
                        name: "FK_Task_Mentor",
                        column: x => x.mentor_id,
                        principalTable: "Mentor",
                        principalColumn: "mentor_id");
                });

            migrationBuilder.CreateTable(
                name: "WorkingResult",
                columns: table => new
                {
                    result_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_by = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    rating = table.Column<double>(type: "float", nullable: true),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mentor_id = table.Column<int>(type: "int", nullable: false),
                    task_id = table.Column<int>(type: "int", nullable: false),
                    intern_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__feedback__7A6B2B8C2ACE3141", x => x.result_id);
                    table.ForeignKey(
                        name: "FK_WorkingResult_Intern",
                        column: x => x.intern_id,
                        principalTable: "Intern",
                        principalColumn: "intern_id");
                    table.ForeignKey(
                        name: "FK_WorkingResult_Task",
                        column: x => x.task_id,
                        principalTable: "Task",
                        principalColumn: "task_id");
                    table.ForeignKey(
                        name: "FK_feedback_Mentor",
                        column: x => x.mentor_id,
                        principalTable: "Mentor",
                        principalColumn: "mentor_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Intern_company_id",
                table: "Intern",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_Intern_mentor_id",
                table: "Intern",
                column: "mentor_id");

            migrationBuilder.CreateIndex(
                name: "IX_interviews_info_intern_id",
                table: "interviews_info",
                column: "intern_id");

            migrationBuilder.CreateIndex(
                name: "IX_Mentor_company_id",
                table: "Mentor",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_Task_intern_id",
                table: "Task",
                column: "intern_id");

            migrationBuilder.CreateIndex(
                name: "IX_Task_mentor_id",
                table: "Task",
                column: "mentor_id");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingResult_intern_id",
                table: "WorkingResult",
                column: "intern_id");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingResult_mentor_id",
                table: "WorkingResult",
                column: "mentor_id");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingResult_task_id",
                table: "WorkingResult",
                column: "task_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "interviews_info");

            migrationBuilder.DropTable(
                name: "WorkingResult");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "Intern");

            migrationBuilder.DropTable(
                name: "Mentor");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
