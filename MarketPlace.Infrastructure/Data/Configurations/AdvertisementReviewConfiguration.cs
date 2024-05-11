namespace MarketPlace.Infrastructure.Data.Configurations;

public class AdvertisementReviewConfiguration: IEntityTypeConfiguration<AdvertisementReview>
{
    public void Configure(EntityTypeBuilder<AdvertisementReview> builder)
    {
        builder.ToTable("Advertisement_Reviews");

        builder.HasKey(x => x.Id)
            .HasName("PK_Advertisement_Reviews");

        builder.Property(x => x.Id)
            .HasColumnName("Id");

        builder.Property(x => x.AdvertisementId)
            .HasColumnName("Advertisement_Id")
            .IsRequired();
        
        builder.Property(x => x.CreatorId)
            .HasColumnName("Creator_Id")
            .IsRequired();
        
        builder.Property(x => x.Rating)
            .HasColumnName("Rating")
            .IsRequired();

        builder.Property(x => x.Comment)
            .HasColumnName("Comment")
            .HasMaxLength(Constraints.REVIEW_MAX_COMMENT_LENGTH);
        // .IsRequired();
        // We can't make this, because we want to allow empty comments
        // In some BD empty string is the same as NULL
        
        builder.Property(x => x.DateCreated)
            .HasColumnName("Date_Created")
            .IsRequired();
        
        builder.Property(x => x.DateUpdated)
            .HasColumnName("Date_Updated")
            .IsRequired();
        
        builder.HasOne<UserAdvertisement>()
            .WithMany()
            .HasForeignKey(x => x.AdvertisementId)
            .HasConstraintName("FK_Advertisement_Reviews_Advertisement_Id")
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.CreatorId)
            .HasConstraintName("FK_Advertisement_Reviews_Creator_Id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}