namespace App.Infrastructure.Data
{
    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public string? ContentImgPath { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation
        public virtual User User { get; set; }

        public virtual List<PostReaction> Reactions { get; set; } = new List<PostReaction>();
        public virtual List<PostComment> PostComments { get; set; } = new List<PostComment>();
        public virtual List<Tag> Tags { get; set; } = new List<Tag>();
        public virtual List<TagPost> TagPosts { get; set; }
    }
}