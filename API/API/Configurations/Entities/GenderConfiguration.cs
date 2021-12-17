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
                    IdGender = 1,
                    Name = "Accion",
                    Image = "Anonymous.png"
                },
                new Gender
                {
                    IdGender = 2,
                    Name = "Terror",
                    Image = "Anonymous.png"
                },
                new Gender
                {
                    IdGender = 3,
                    Name = "Suspenso",
                    Image = "Anonymous.png"
                },
                new Gender
                {
                    IdGender = 4,
                    Name = "Amor",
                    Image = "Anonymous.png"
                }
            );
        }
    }
}
