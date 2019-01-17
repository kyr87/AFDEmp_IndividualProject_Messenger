using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFDEmp_IndividualProject_Messenger
{
    internal interface IDataHandling
    {
        // Create       
        bool CreateUserData(User user);
        bool CreateMessageData(Message personal);

        // Read
        List<User> ReadUserData();
        List<Message> ReadMessages();

        // Update
        bool UpdateUserRole(User UpdatedUser);
        bool UpdateMessageBody(Message OldMessage, string NewMessage);

        // Delete
        bool DeleteUser(User DeletedUser);
        bool DeleteMessage(Message DeletedMessage, User user);

        // Check
        bool UsernameExists(string Username);
        bool EmptyStorage();
    }
}
