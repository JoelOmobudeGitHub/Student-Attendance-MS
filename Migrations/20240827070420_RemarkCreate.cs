using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentAttendanceManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class RemarkCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Remarks_StudentRemarkId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "StudentRemarkId",
                table: "AspNetUsers",
                newName: "RemarksId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_StudentRemarkId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_RemarksId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Remarks_RemarksId",
                table: "AspNetUsers",
                column: "RemarksId",
                principalTable: "Remarks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Remarks_RemarksId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "RemarksId",
                table: "AspNetUsers",
                newName: "StudentRemarkId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_RemarksId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_StudentRemarkId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Remarks_StudentRemarkId",
                table: "AspNetUsers",
                column: "StudentRemarkId",
                principalTable: "Remarks",
                principalColumn: "Id");
        }
    }
}
