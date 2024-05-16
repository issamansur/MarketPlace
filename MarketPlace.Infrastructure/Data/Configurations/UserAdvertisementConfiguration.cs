namespace MarketPlace.Infrastructure.Data.Configurations;

public class UserAdvertisementConfiguration: IEntityTypeConfiguration<UserAdvertisement>
{
    public void Configure(EntityTypeBuilder<UserAdvertisement> builder)
    {
        builder.ToTable("User_Advertisements");

        builder.HasKey(x => x.Id)
            .HasName("PK_User_Advertisements");

        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasComment("Уникальный идентификатор объявления")
            .IsRequired();

        builder.Property(x => x.CreatorId)
            .HasColumnName("Creator_Id")
            .IsRequired();
        
        // TODO: CHECK ON CORRECTNESS
        builder.Property(x => x.Number)
            .HasColumnName("Number")
            .HasComment("Номер объявления")
            .ValueGeneratedOnAdd()
            .IsRequired();
        
        builder.Property(x => x.Title)
            .HasColumnName("Title")
            .HasComment("Заголовок объявления")
            .HasMaxLength(Constraints.USER_AD_MAX_DESC_LENGTH)
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasColumnName("Description")
            .HasComment("Описание объявления")
            .HasMaxLength(Constraints.USER_AD_MAX_DESC_LENGTH)
            .IsRequired();

        builder.Property(x => x.ImageUrl)
            .HasColumnName("Image_Url")
            .HasComment("Ссылка на изображение")
            .IsRequired();
        
        
        builder.Property(x => x.RatingCount)
            .HasColumnName("Rating_Count")
            .HasComment("Количество оценок")
            .IsRequired();
        
        builder.Property(x => x.RatingSum)
            .HasColumnName("Rating_Sum")
            .HasComment("Сумма оценок")
            .IsRequired();
        
        // TODO: CHECK ON CORRECTNESS
        builder.Property(x => x.Rating)
            .HasColumnName("Rating")
            .HasComment("Рейтинг объявления")
            .HasPrecision(2)
            .IsRequired();
        
        
        builder.Property(x => x.DateCreated)
            .HasColumnName("Date_Created")
            .HasComment("Дата создания объявления")
            .IsRequired();
        
        builder.Property(x => x.DateUpdated)
            .HasColumnName("Date_Updated")
            .HasComment("Дата последнего обновления объявления")
            .IsRequired();
        
        builder.Property(x => x.DateExpired)
            .HasColumnName("Date_Expired")
            .HasComment("Дата окончания объявления")
            .IsRequired();
        
        
        builder.Property(x => x.IsActive)
            .HasColumnName("Is_Active")
            .HasComment("Активно ли объявление")
            .IsRequired();
        
        
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.CreatorId)
            .HasConstraintName("FK_User_Advertisements_Creator_Id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}