using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MarketPlace.Infrastructure.Migrations.PostgreSql
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор роли"),
                    Title = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, comment: "Название роли")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор пользователя"),
                    Role_Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор роли"),
                    Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, comment: "Имя пользователя")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Role_Id",
                        column: x => x.Role_Id,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Advertisements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор объявления"),
                    Creator_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false, comment: "Номер объявления")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, comment: "Заголовок объявления"),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, comment: "Описание объявления"),
                    Image_Url = table.Column<string>(type: "text", nullable: true, comment: "Ссылка на изображение"),
                    Rating_Sum = table.Column<int>(type: "integer", nullable: false, comment: "Сумма оценок"),
                    Rating_Count = table.Column<int>(type: "integer", nullable: false, comment: "Количество оценок"),
                    Rating = table.Column<double>(type: "double precision", precision: 2, nullable: false, comment: "Рейтинг объявления"),
                    Date_Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "Дата создания объявления"),
                    Date_Updated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "Дата последнего обновления объявления"),
                    Date_Expired = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "Дата окончания объявления"),
                    Is_Active = table.Column<bool>(type: "boolean", nullable: false, comment: "Активно ли объявление")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Advertisements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Advertisements_Creator_Id",
                        column: x => x.Creator_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Advertisement_Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор отзыва"),
                    Advertisement_Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор объявления"),
                    Creator_Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор создателя отзыва"),
                    Rating = table.Column<int>(type: "integer", nullable: false, comment: "Оценка"),
                    Comment = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, comment: "Комментарий"),
                    Date_Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "Дата создания отзыва"),
                    Date_Updated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "Дата обновления отзыва")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisement_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Advertisement_Reviews_Advertisement_Id",
                        column: x => x.Advertisement_Id,
                        principalTable: "User_Advertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Advertisement_Reviews_Creator_Id",
                        column: x => x.Creator_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_Reviews_Advertisement_Id",
                table: "Advertisement_Reviews",
                column: "Advertisement_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_Reviews_Creator_Id",
                table: "Advertisement_Reviews",
                column: "Creator_Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Advertisements_Creator_Id",
                table: "User_Advertisements",
                column: "Creator_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Role_Id",
                table: "Users",
                column: "Role_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advertisement_Reviews");

            migrationBuilder.DropTable(
                name: "User_Advertisements");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
