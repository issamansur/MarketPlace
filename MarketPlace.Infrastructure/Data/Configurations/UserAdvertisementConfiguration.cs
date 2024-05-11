namespace MarketPlace.Infrastructure.Data.Configurations;

public class UserAdvertisementConfiguration: IEntityTypeConfiguration<UserAdvertisement>
{
    public void Configure(EntityTypeBuilder<UserAdvertisement> builder)
    {
        builder.ToTable("User_Advertisements");

        builder.HasKey(x => x.Id)
            .HasName("PK_User_Advertisements");

        builder.Property(x => x.Id)
            .HasColumnName("Id");

        builder.Property(x => x.CreatorId)
            .HasColumnName("Creator_Id")
            .IsRequired();
        
        // TODO: CHECK ON CORRECTNESS
        builder.Property(x => x.Number)
            .HasColumnName("Number")
            .ValueGeneratedOnAdd()
            .IsRequired();
        
        builder.Property(x => x.Title)
            .HasColumnName("Title")
            .HasMaxLength(Constraints.USER_AD_MAX_DESC_LENGTH)
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasColumnName("Description")
            .HasMaxLength(Constraints.USER_AD_MAX_DESC_LENGTH)
            .IsRequired();

        builder.Property(x => x.ImageUrl)
            .HasColumnName("Image_Url");
        
        
        builder.Property(x => x.CountRating)
            .HasColumnName("Count_Rating")
            .IsRequired();
        
        builder.Property(x => x.SumRating)
            .HasColumnName("Sum_Rating")
            .IsRequired();
        
        // TODO: CHECK ON CORRECTNESS
        builder.Property(x => x.Rating)
            .HasColumnName("Rating")
            .IsRequired();
        
        
        builder.Property(x => x.DateCreated)
            .HasColumnName("Date_Created")
            .IsRequired();
        
        builder.Property(x => x.DateUpdated)
            .HasColumnName("Date_Updated")
            .IsRequired();
        
        builder.Property(x => x.DateExpired)
            .HasColumnName("Date_Expired")
            .IsRequired();
        
        
        builder.Property(x => x.IsActive)
            .HasColumnName("Is_Active")
            .IsRequired();
        
        
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.CreatorId)
            .HasConstraintName("FK_User_Advertisements_Creator_Id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}