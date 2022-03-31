using System.ComponentModel.DataAnnotations;

namespace App.Infrastructure.Data
{
    public class Setting
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Value { get; set; }

        // Navigation
        public virtual User User { get; set; }
    }
}