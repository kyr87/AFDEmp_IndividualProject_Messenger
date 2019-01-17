using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace AFDEmp_IndividualProject_Messenger
{
    class MessageTableConfiguration : EntityTypeConfiguration<Message>
    {
        public MessageTableConfiguration()
        {
            Property(message => message.TimeSent)                
                .IsRequired();
        
            Property(message => message.Body)
                .HasMaxLength(250)
                .IsRequired();

            Property(message => message.SenderId)
                .IsRequired();

            Property(message => message.ReceiverId)
                .IsRequired();

            HasRequired<User>(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .WillCascadeOnDelete(false);

            HasRequired<User>(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .WillCascadeOnDelete(false);
        }     
    }
}
