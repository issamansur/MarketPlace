namespace MarketPlace.Infrastructure.Data.Configurations;

public class RoleConfiguration: IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(x => x.Id)
            .HasName("PK_Roles");
        
        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasComment("Уникальный идентификатор роли")
            .IsRequired();

        builder.Property(x => x.Title)
            .HasColumnName("Title")
            .HasComment("Название роли")
            .HasMaxLength(Constraints.ROLE_MAX_TITLE_LENGTH)
            .IsRequired();
    }
}