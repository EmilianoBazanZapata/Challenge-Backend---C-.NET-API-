using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Configurations.Entities
{
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.HasData(
                new Gender
                {
                    Id = 1,
                    Name = "Accion",
                    Image = "Anonymous.png"
                },
                new Gender
                {
                    Id = 2,
                    Name = "Terror",
                    Image = "Anonymous.png"
                },
                new Gender
                {
                    Id = 3,
                    Name = "Suspenso",
                    Image = "Anonymous.png"
                },
                new Gender
                {
                    Id = 4,
                    Name = "Amor",
                    Image = "Anonymous.png"
                }
            );
        }
    }
}
