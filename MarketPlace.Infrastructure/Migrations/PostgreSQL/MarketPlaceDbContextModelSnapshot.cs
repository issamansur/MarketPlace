﻿// <auto-generated />
using System;
using MarketPlace.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MarketPlace.Infrastructure.Migrations.PostgreSql
{
    [DbContext(typeof(MarketPlaceDbContext))]
    partial class MarketPlaceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MarketPlace.Domain.Entities.UserAdvertisements.AdvertisementReview", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id")
                        .HasComment("Уникальный идентификатор отзыва");

                    b.Property<Guid>("AdvertisementId")
                        .HasColumnType("uuid")
                        .HasColumnName("Advertisement_Id")
                        .HasComment("Идентификатор объявления");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("Comment")
                        .HasComment("Комментарий");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uuid")
                        .HasColumnName("Creator_Id")
                        .HasComment("Идентификатор создателя отзыва");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("Date_Created")
                        .HasComment("Дата создания отзыва");

                    b.Property<DateTimeOffset>("DateUpdated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("Date_Updated")
                        .HasComment("Дата обновления отзыва");

                    b.Property<int>("Rating")
                        .HasColumnType("integer")
                        .HasColumnName("Rating")
                        .HasComment("Оценка");

                    b.HasKey("Id")
                        .HasName("PK_Advertisement_Reviews");

                    b.HasIndex("AdvertisementId")
                        .HasDatabaseName("IX_Advertisement_Reviews_Advertisement_Id");

                    b.HasIndex("CreatorId")
                        .HasDatabaseName("IX_Advertisement_Reviews_Creator_Id");

                    b.HasIndex("Rating")
                        .HasDatabaseName("IX_Advertisement_Reviews_Rating");

                    b.ToTable("Advertisement_Reviews", (string)null);
                });

            modelBuilder.Entity("MarketPlace.Domain.Entities.UserAdvertisements.UserAdvertisement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id")
                        .HasComment("Уникальный идентификатор объявления");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uuid")
                        .HasColumnName("Creator_Id");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("Date_Created")
                        .HasComment("Дата создания объявления");

                    b.Property<DateTimeOffset>("DateExpired")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("Date_Expired")
                        .HasComment("Дата окончания объявления");

                    b.Property<DateTimeOffset>("DateUpdated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("Date_Updated")
                        .HasComment("Дата последнего обновления объявления");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("Description")
                        .HasComment("Описание объявления");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Image_Url")
                        .HasComment("Ссылка на изображение");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("Is_Active")
                        .HasComment("Активно ли объявление");

                    b.Property<int>("Number")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Number")
                        .HasComment("Номер объявления");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Number"));

                    b.Property<double>("Rating")
                        .HasPrecision(2)
                        .HasColumnType("double precision")
                        .HasColumnName("Rating")
                        .HasComment("Рейтинг объявления");

                    b.Property<int>("RatingCount")
                        .HasColumnType("integer")
                        .HasColumnName("Rating_Count")
                        .HasComment("Количество оценок");

                    b.Property<int>("RatingSum")
                        .HasColumnType("integer")
                        .HasColumnName("Rating_Sum")
                        .HasComment("Сумма оценок");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("Title")
                        .HasComment("Заголовок объявления");

                    b.HasKey("Id")
                        .HasName("PK_User_Advertisements");

                    b.HasIndex("CreatorId")
                        .HasDatabaseName("IX_User_Advertisements_Creator_Id");

                    b.HasIndex("DateUpdated")
                        .HasDatabaseName("IX_User_Advertisements_DateUpdated");

                    b.HasIndex("Description")
                        .HasDatabaseName("IX_User_Advertisements_Description");

                    b.HasIndex("Number")
                        .HasDatabaseName("IX_User_Advertisements_Number");

                    b.HasIndex("Title")
                        .HasDatabaseName("IX_User_Advertisements_Title");

                    b.ToTable("User_Advertisements", (string)null);
                });

            modelBuilder.Entity("MarketPlace.Domain.Entities.Users.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id")
                        .HasComment("Уникальный идентификатор роли");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("Title")
                        .HasComment("Название роли");

                    b.HasKey("Id")
                        .HasName("PK_Roles");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("MarketPlace.Domain.Entities.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id")
                        .HasComment("Уникальный идентификатор пользователя");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("Name")
                        .HasComment("Имя пользователя");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("Role_Id")
                        .HasComment("Идентификатор роли");

                    b.HasKey("Id")
                        .HasName("PK_Users");

                    b.HasIndex("RoleId");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("MarketPlace.Domain.Entities.UserAdvertisements.AdvertisementReview", b =>
                {
                    b.HasOne("MarketPlace.Domain.Entities.UserAdvertisements.UserAdvertisement", null)
                        .WithMany()
                        .HasForeignKey("AdvertisementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Advertisement_Reviews_Advertisement_Id");

                    b.HasOne("MarketPlace.Domain.Entities.Users.User", null)
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Advertisement_Reviews_Creator_Id");
                });

            modelBuilder.Entity("MarketPlace.Domain.Entities.UserAdvertisements.UserAdvertisement", b =>
                {
                    b.HasOne("MarketPlace.Domain.Entities.Users.User", null)
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_Advertisements_Creator_Id");
                });

            modelBuilder.Entity("MarketPlace.Domain.Entities.Users.User", b =>
                {
                    b.HasOne("MarketPlace.Domain.Entities.Users.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_Users_Role_Id");
                });
#pragma warning restore 612, 618
        }
    }
}
