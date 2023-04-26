using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace net_il_mio_fotoalbum.Models
{
    public class AlbumContext : IdentityDbContext<IdentityUser>
    {
        public AlbumContext(DbContextOptions<AlbumContext> options) : base(options) { }

        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Message> Messages { get; set; }

        public void Seed()
        {
            var imageSeed = new Image[]
            {
                new()
                {
                    Title = "Colosseo",
                    Description = "Una splendida foto del colosseo al tramonto",
                    Url = "https://media.istockphoto.com/id/539115110/it/foto/colosseo-di-roma-e-sole-mattutino-italia.webp?s=612x612&w=is&k=20&c=LwkfIALOB9p_n58jDQ31QM5hZyVEFbvckuwdthiHT0c=",
                    Visible = true,
                },
                new()
                {
                    Title = "Coppia innamorata",
                    Description = "Coppia su uno scooter",
                    Url = "https://media.istockphoto.com/id/967326474/it/foto/giovane-coppia-che-si-diverte-a-guidare-scooter-nel-centro-storico-europeo.jpg?s=612x612&w=0&k=20&c=9SoeLBvQjvwB4jvuxFaEvGfCRWiC56p0CPUhNlbk4gw=",
                    Visible = true,
                },
                new()
                {
                    Title = "Paese sul mare",
                    Description = "Scorcio di un paese molto colorato sulla scogliera",
                    Url = "https://media.istockphoto.com/id/479824818/it/foto/bellissimo-colorato-paesaggio-urbano.jpg?s=612x612&w=0&k=20&c=-HcRUmWKw4Skh5R8gQe17sGLcZNpk2DuYdGdbpBloyM=",
                    Visible = true,
                },
                new()
                {
                    Title = "Venezia",
                    Description = "Veduta dei canali di Venezia",
                    Url = "https://media.istockphoto.com/id/911570904/it/foto/veduta-del-canal-grande-di-venezia.jpg?s=612x612&w=0&k=20&c=zX0LOZ7H2MYFoq-juUuFO7It8wI5wZ0DBHPE8SsJEn0=",
                    Visible = true,
                },
            };
            if (!Images.Any())
            {
                Images.AddRange(imageSeed);
            }

            var categoriesSeed = new Category[]
            {
                new()
                {
                    Name = "Paesaggi naturali",
                    Images = imageSeed
                },
                new()
                {
                    Name = "Ritratti"
                },
                new()
                {
                    Name = "Paesaggi urbani"
                },
                new()
                {
                    Name = "Italia"
                },
            };
            if (!Categories.Any())
            {
                Categories.AddRange(categoriesSeed);
            };

            if (!Roles.Any())
            {
                var RolesSeed = new IdentityRole[]
                {
                    new("Admin"),
                };

                Roles.AddRange(RolesSeed);
            }

            if (!Users.Any(u => u.Email == "admin@dev.com") && !UserRoles.Any())
            {
                var admin = Users.First(u => u.Email == "admin@dev.com");

                var adminRole = Roles.First(r => r.Name == "Admin");

                var seed = new IdentityUserRole<string>[]
                {
                    new()
                    {
                        UserId = admin.Id,
                        RoleId = adminRole.Id
                    },
                };

                UserRoles.AddRange(seed);
            }

            SaveChanges();
        }
    }
}
