using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;

namespace SimpleChat.Data;

public class ChatContext : DbContext
{
  public ChatContext(DbContextOptions<ChatContext> options)
    : base(options)
  {
  }

  public DbSet<ChatUser> Users => Set<ChatUser>();
  public DbSet<Message> Messages => Set<Message>();

  protected override void OnModelCreating(ModelBuilder bldr)
  {
    new Action<EntityTypeBuilder<ChatUser>>(users =>
    {
      users.HasData(
        new { Id = 1, Name = "Shawn Wildermuth" },
        new { Id = 2, Name = "Jake Smith" }
      );
    })(bldr.Entity<ChatUser>());

    new Action<EntityTypeBuilder<Message>>(msgType =>
    {
      msgType.HasOne(m => m.Sender).WithOne().OnDelete(DeleteBehavior.NoAction);
      msgType.HasOne(m => m.Receiver).WithOne().OnDelete(DeleteBehavior.NoAction);

      msgType.HasData(
        new { Id = 1, Text = "Hello World", SenderId = 1, ReceiverId = 2, MessageDate = DateTime.UtcNow },
        new { Id = 2, Text = "Replied", SenderId = 2, ReceiverId = 1, MessageDate = DateTime.UtcNow }
      );
    })(bldr.Entity<Message>());
  }
}
