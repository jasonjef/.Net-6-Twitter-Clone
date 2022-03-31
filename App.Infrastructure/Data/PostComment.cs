using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Infrastructure.Data
{
    public class PostComment
    {
        public int Id { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        public int UserId { get; set; }

        public string? Comment { get; set; }
        public string? CommentImgPath { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation

        [ForeignKey("PostId")]
        public Post Post { get; set; }

        public User User { get; set; }

        public virtual List<PostCommentReaction> PostCommentReactions { get; set; } = new List<PostCommentReaction>();
    }
}