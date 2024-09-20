using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentAttendanceManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class ThirdCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Remarks_RemarksId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RemarksId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RemarksId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "StudentIdId",
                table: "Remarks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Remarks_StudentIdId",
                table: "Remarks",
                column: "StudentIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Remarks_AspNetUsers_StudentIdId",
                table: "Remarks",
                column: "StudentIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Remarks_AspNetUsers_StudentIdId",
                table: "Remarks");

            migrationBuilder.DropIndex(
                name: "IX_Remarks_StudentIdId",
                table: "Remarks");

            migrationBuilder.DropColumn(
                name: "StudentIdId",
                table: "Remarks");

            migrationBuilder.AddColumn<string>(
                name: "RemarksId",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RemarksId",
                table: "AspNetUsers",
                column: "RemarksId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Remarks_RemarksId",
                table: "AspNetUsers",
                column: "RemarksId",
                principalTable: "Remarks",
                principalColumn: "Id");
        }
    }
}
