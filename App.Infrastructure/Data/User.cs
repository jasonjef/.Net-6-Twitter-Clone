using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Infrastructure.Data
{
    public class User
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        public string Password { get; set; }

        public string ProfileImagePath { get; set; }

        public string ProfileImageThumb { get; set; }

        public string? Phone { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public string? Role { get; set; }

        public bool IsActive { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string? ActivationKey { get; set; }

        public string? FgPwActivationKey { get; set; }

        #region Navigation

        public virtual List<Post> Posts { get; set; } = new List<Post>();
        public virtual List<PostReaction> PostReactions { get; set; } = new List<PostReaction>();
        public virtual List<Setting> Settings { get; set; }
        public virtual List<PostComment> PostComments { get; set; } = new List<PostComment>();
        public virtual List<PostCommentReaction> PostCommentReactions { get; set; } = new List<PostCommentReaction>();

        public virtual ICollection<Message> WriterSender { get; set; }
        public virtual ICollection<Message> WriterReceiver { get; set; }

        public virtual ICollection<Relationship> UserFollower { get; set; }
        public virtual ICollection<Relationship> UserFollowed { get; set; }

        //public virtual List<Relationship> Relationships{ get; set; } = new List<Relationship>();

        #endregion Navigation
    }
}