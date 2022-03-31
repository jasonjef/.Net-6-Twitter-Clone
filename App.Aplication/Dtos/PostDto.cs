namespace App.Application.Dtos
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string? ContentImgPath { get; set; }
        public string UserName { get; set; }
        public string ProfileImagePath { get; set; }
        public string Name { get; set; }
        public string ReactionTypeL { get; set; }
        public string ReactionTypeR { get; set; }
    }
}