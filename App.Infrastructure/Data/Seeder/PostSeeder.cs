using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Data.Seeder
{
    public class PostSeeder
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().HasData(new List<Post>
            {
                new Post {
                    Id = 1,
                    Content = "Selam bu bir deneme tweete",
                    UserId = 1,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                },
                new Post {
                    Id = 2,
                    Content = "Selam bu bir deneme tweete",
                    UserId = 2,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                },
                new Post {
                    Id = 3,
                    Content = "API İşlemi gerçekleşti",
                    UserId = 2,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                },
                new Post {
                    Id = 4,
                    Content = "Olley",
                    UserId = 2,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                },            
            });
        }
    }
}