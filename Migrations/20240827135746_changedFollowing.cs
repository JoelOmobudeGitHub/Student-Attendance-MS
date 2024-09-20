using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentAttendanceManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class changedFollowing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Follower",
                table: "Followings",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Follow",
                table: "Followings",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Follow",
                table: "Followings");

            migrationBuilder.AlterColumn<string>(
                name: "Follower",
                table: "Followings",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
