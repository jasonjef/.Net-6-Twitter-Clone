using App.Infrastructure.Data.Seeder;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<PostCommentReaction> PostCommentReactions { get; set; }
        public DbSet<PostReaction> PostReactions { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagPost> TagPosts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostComment>()
                .HasOne(s => s.User)
                .WithMany(g => g.PostComments)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PostReaction>()
                .HasOne(s => s.User)
                .WithMany(g => g.PostReactions)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PostCommentReaction>()
                .HasOne(s => s.User)
                .WithMany(g => g.PostCommentReactions)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Message>()
                .HasOne(s => s.SenderUser)
                .WithMany(s => s.WriterSender)
                .HasForeignKey(z => z.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Message>()
                .HasOne(s => s.ReciverUser)
                .WithMany(s => s.WriterReceiver)
                .HasForeignKey(z => z.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Relationship>()
                .HasOne(s => s.Follwer)
                .WithMany(s => s.UserFollower)
                .HasForeignKey(z => z.FollowerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Relationship>()
                .HasOne(s => s.Followed)
                .WithMany(s => s.UserFollowed)
                .HasForeignKey(z => z.FollowedId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            UserSeeder.SeedData(modelBuilder);
            PostSeeder.SeedData(modelBuilder);

            // EF Core 6 ile
            //modelBuilder.Entity<Tag>()
            //    .HasMany(e => e.Posts)
            //    .WithMany(e => e.Tags)

            //    .UsingEntity<TagPost>(
            //        e => e.HasOne<Post>().WithMany().HasForeignKey(e => e.PostId),
            //        e => e.HasOne<Tag>().WithMany().HasForeignKey(e => e.TagId),
            //        e => e.HasKey(e => new { e.PostId, e.TagId })
            //        );
        }
    }
}