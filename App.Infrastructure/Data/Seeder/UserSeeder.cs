using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Data.Seeder
{
    public class UserSeeder
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new List<User>
            {
                new User {
                    Id = 1,
                    UserName = "admin",
                    Name = "admin",
                    Password = "admin",
                    Email = "admin@site.com",
                    Role = "User",
                    ProfileImagePath = "https://cdn.discordapp.com/attachments/916029512884563999/949102276654538792/user.png",
                    ProfileImageThumb = "https://pbs.twimg.com/media/EHlc5JOXkAAc7Oy?format=jpg&name=large",
                    IsActive = true,
                    
                },
                new User {
                    Id = 2,
                    UserName = "alpysl",
                    Name = "Alperen Yeşil",
                    Password = "alpysl",
                    Email = "alperensyesil@gmail.com",
                    Role = "User",
                    ProfileImagePath = "https://pbs.twimg.com/profile_images/1068539238751379456/f8987Hr5_400x400.jpg",
                    ProfileImageThumb = "https://pbs.twimg.com/profile_banners/4612473879/1543594583/1500x500",
                    IsActive = true,
                },
            });
        }
    }
}
