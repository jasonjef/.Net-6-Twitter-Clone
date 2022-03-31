namespace App.Infrastructure.Data
{
    public class TagPost
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public int TagId { get; set; }

        public virtual List<Post> Posts { get; set; } = new List<Post>();

        public virtual Tag Tags { get; set; }
    }
}