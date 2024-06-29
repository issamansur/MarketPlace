namespace MarketPlace.Infrastructure.Data.Configurations;

public class AdvertisementReviewConfiguration: IEntityTypeConfiguration<AdvertisementReview>
{
    public void Configure(EntityTypeBuilder<AdvertisementReview> builder)
    {
        builder.ToTable("Advertisement_Reviews");

        builder.HasKey(x => x.Id)
            .HasName("PK_Advertisement_Reviews");

        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasComment("Уникальный идентификатор отзыва")
            .IsRequired();

        builder.Property(x => x.AdvertisementId)
            .HasColumnName("Advertisement_Id")
            .HasComment("Идентификатор объявления")
            .IsRequired();
        
        builder.Property(x => x.CreatorId)
            .HasColumnName("Creator_Id")
            .HasComment("Идентификатор создателя отзыва")
            .IsRequired();
        
        builder.Property(x => x.Rating)
            .HasColumnName("Rating")
            .HasComment("Оценка")
            .IsRequired();

        builder.Property(x => x.Comment)
            .HasColumnName("Comment")
            .HasComment("Комментарий")
            .HasMaxLength(Constraints.REVIEW_MAX_COMMENT_LENGTH);
        
        builder.Property(x => x.DateCreated)
            .HasColumnName("Date_Created")
            .HasComment("Дата создания отзыва")
            .IsRequired();
        
        builder.Property(x => x.DateUpdated)
            .HasColumnName("Date_Updated")
            .HasComment("Дата обновления отзыва")
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