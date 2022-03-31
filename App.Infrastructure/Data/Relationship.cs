namespace App.Infrastructure.Data
{
    // TODO: Sonra yapılacak
    public class Relationship
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FollowerId { get; set; }
        public int FollowedId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation
        public User Follwer { get; set; }

        public User Followed { get; set; }
    }
}