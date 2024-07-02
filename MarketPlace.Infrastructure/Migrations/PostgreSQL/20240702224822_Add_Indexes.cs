using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPlace.Infrastructure.Migrations.PostgreSql
{
    /// <inheritdoc />
    public partial class Add_Indexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Role_Id",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_User_Advertisements_DateUpdated",
                table: "User_Advertisements",
                column: "Date_Updated");

            migrationBuilder.CreateIndex(
                name: "IX_User_Advertisements_Description",
                table: "User_Advertisements",
                column: "Description");

            migrationBuilder.CreateIndex(
                name: "IX_User_Advertisements_Number",
                table: "User_Advertisements",
                column: "Number");

            migrationBuilder.CreateIndex(
                name: "IX_User_Advertisements_Title",
                table: "User_Advertisements",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_Reviews_Rating",
                table: "Advertisement_Reviews",
                column: "Rating");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Role_Id",
                table: "Users",
                column: "Role_Id",
                principalTable: "Roles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Role_Id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_User_Advertisements_DateUpdated",
                table: "User_Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_User_Advertisements_Description",
                table: "User_Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_User_Advertisements_Number",
                table: "User_Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_User_Advertisements_Title",
                table: "User_Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisement_Reviews_Rating",
                table: "Advertisement_Reviews");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Role_Id",
                table: "Users",
                column: "Role_Id",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
