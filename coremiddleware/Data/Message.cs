namespace SimpleChat.Data;

public class Message
{
  public int Id { get; set; }
  public string? Text { get; set; }
  public DateTime MessageDate { get; set; } = DateTime.UtcNow;
  public ChatUser? Receiver { get; set; }
  public int ReceiverId { get; set; }
  public ChatUser? Sender { get; set; }
  public int SenderId { get; set; }

}