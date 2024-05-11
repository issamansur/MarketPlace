namespace MarketPlace.Infrastructure.Data.Configurations;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id)
            .HasName("PK_Users_Id");
        
        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .IsRequired();
        
        builder.Property(x => x.RoleId)
            .HasColumnName("Role_Id")
            .IsRequired();
        
        builder.Property(x => x.Name)
            .HasColumnName("First_Name")
            .HasMaxLength(Constraints.USER_MAX_NAME_LENGTH)
            .IsRequired();
        
        builder.HasOne<Role>()
            .WithMany()
            .HasForeignKey(x => x.RoleId)
            .HasConstraintName("FK_Users_Role_Id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}