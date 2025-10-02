namespace App.Domain.Entities
{
    public class Subscription
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Plan { get; set; } = string.Empty;
        public int MaxItems { get; set; }
        public byte[] RowVersion { get; set; } = default!;
    }
}
