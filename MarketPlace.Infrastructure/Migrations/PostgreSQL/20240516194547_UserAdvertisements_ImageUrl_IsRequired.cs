using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPlace.Infrastructure.Migrations.PostgreSql
{
    /// <inheritdoc />
    public partial class UserAdvertisements_ImageUrl_IsRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image_Url",
                table: "User_Advertisements",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "Ссылка на изображение",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldComment: "Ссылка на изображение");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image_Url",
                table: "User_Advertisements",
                type: "text",
                nullable: true,
                comment: "Ссылка на изображение",
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "Ссылка на изображение");
        }
    }
}
