namespace App.Infrastructure.Data
{
    public class Message
    {
        public int Id { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public string MessageContent { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool WriterStatus { get; set; }

        public User SenderUser { get; set; }
        public User ReciverUser { get; set; }
    }
}