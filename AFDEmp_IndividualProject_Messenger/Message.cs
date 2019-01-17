using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFDEmp_IndividualProject_Messenger
{
    public class Message
    {
        public int Id { get; set; }

        public DateTime TimeSent { get; set; }
        public string Body { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }

        public Message()
        {
            TimeSent = DateTime.Now;
        }
    }
}
