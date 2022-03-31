namespace App.Infrastructure.Data
{
    public class PostCommentReaction
    {
        public int Id { get; set; }
        public int PostCommentId { get; set; }
        public int UserId { get; set; }
        public string ReactionType { get; set; }

        // Navigation
        public virtual PostComment PostComment { get; set; }

        public virtual User User { get; set; }
    }
}