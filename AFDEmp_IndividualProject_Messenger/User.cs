using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFDEmp_IndividualProject_Messenger
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public Role UserAccess { get; set; }

        public virtual ICollection<Message> ReceivedMessages { get; set; }
        public virtual ICollection<Message> SentMessages { get; set; }

        public User()
        {
            ReceivedMessages = new List<Message>();
            SentMessages = new List<Message>();
        }
    }
}
