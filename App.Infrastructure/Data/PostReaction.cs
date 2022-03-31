namespace App.Infrastructure.Data
{
    public class PostReaction
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string ReactionType { get; set; }

        // Navigation
        public virtual User User { get; set; }

        public virtual Post Post { get; set; }
    }
}