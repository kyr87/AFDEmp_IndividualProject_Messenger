using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Threading;

namespace AFDEmp_IndividualProject_Messenger
{
    class DatabaseAccess : IDataHandling
    {

        public bool CreateMessageData(Message personal)
        {
            using (var Db = new DatabaseEntities())
            {
                Db.Messages.Add(personal);
                return SaveDbChanges(Db);
            }
        }

        public bool CreateUserData(User user)
        {
            using (var Db = new DatabaseEntities())
            {
                Db.Users.Add(user);
                return SaveDbChanges(Db);
            }
        }

        public bool DeleteMessage(Message DeletedMessage, User user)
        {
            using (var Db = new DatabaseEntities())
            {              
                Db.Messages.Remove(Db.Messages.Single(m => m.Id == DeletedMessage.Id));               
                return SaveDbChanges(Db);
            }
        }

        public bool DeleteUser(User DeletedUser)
        {
            using (var Db = new DatabaseEntities())
            {
                var DeletingMessages = Db.Messages.Where(i => i.ReceiverId == DeletedUser.Id || i.SenderId == DeletedUser.Id);

                Db.Messages.RemoveRange(DeletingMessages);
                Db.Users.Remove(Db.Users.Single(u => u.Id == DeletedUser.Id));
                return SaveDbChanges(Db);
            }
        }

        public List<Message> ReadMessages()
        {
            using (var Db = new DatabaseEntities())
            {
                return Db.Messages
                    .Include(Pm => Pm.Sender)
                    .Include(Pm => Pm.Receiver)
                    .ToList();
            }
        }

        public List<User> ReadUserData()
        {
            using (var Db = new DatabaseEntities())
            {
                return Db.Users
                    .Include(u => u.ReceivedMessages)
                    .Include(u => u.SentMessages)
                    .ToList();
            }
        }
      
        public bool UpdateMessageBody(Message OldMessage, string NewMessage)
        {
            using (var Db = new DatabaseEntities())
            {
                if(OldMessage != null)
                {
                    Db.Messages.Single(PM => PM.Body == OldMessage.Body && PM.Id == OldMessage.Id).Body = NewMessage;
                }
                else
                {
                    return false;
                }
                return SaveDbChanges(Db);
            }
        }

        public bool UpdateUserRole(User UpdatedUser)
        {
            using (var Db = new DatabaseEntities())
            {
                Db.Users.Single(u => u.Id == UpdatedUser.Id).UserAccess = UpdatedUser.UserAccess;
                return SaveDbChanges(Db);
            }
        }

        public bool UsernameExists(string Username)
        {
            using (var Db = new DatabaseEntities())
            {
                return Db.Users.Any(u => u.UserName == Username);
            }
        }

        public bool SaveDbChanges(DatabaseEntities DB)
        {
            try
            {
                return (DB.SaveChanges() > 0);
            }
            catch (DbException e)
            {
                Console.WriteLine(e.Message);
                Thread.Sleep(1800);
                return false;
            }
        }

        public bool EmptyStorage()
        {
            using (var Db = new DatabaseEntities())
            {
                return !Db.Users.Any();
            }
        }
    }
}
