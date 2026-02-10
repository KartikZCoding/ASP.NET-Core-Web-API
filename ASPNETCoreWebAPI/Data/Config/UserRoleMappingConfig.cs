using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASPNETCoreWebAPI.Data.Config
{
    public class UserRoleMappingConfig : IEntityTypeConfiguration<UserRoleMapping>
    {
        public void Configure(EntityTypeBuilder<UserRoleMapping> builder)
        {
            builder.ToTable("UserRoleMappings");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasIndex(n => new { n.UserId, n.RoleId }, "UK_UserRoleMapping");
        }
    }
}
