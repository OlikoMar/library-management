using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RefreshToken = Identity.Data.Entities.RefreshToken;

namespace Identity.Data.EntityConfiguration;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<Entities.RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(s => s.Token);
    }
}