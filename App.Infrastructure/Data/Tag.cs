using System.ComponentModel.DataAnnotations;

namespace App.Infrastructure.Data
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        // Navigation
        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

        public virtual ICollection<TagPost> TagPosts { get; set; } = new List<TagPost>();
    }
}