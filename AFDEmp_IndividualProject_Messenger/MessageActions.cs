using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using System.Threading.Tasks;

namespace AFDEmp_IndividualProject_Messenger
{
    internal class MessageActions
    {
        private IDataHandling DataHandler;
        private User ActiveUser;
        FileStorage file;

        public MessageActions(IDataHandling DataProvider, User CurrentUser)
        {
            DataHandler = DataProvider;
            ActiveUser = CurrentUser;
            file = new FileStorage();
        }

        public bool CreateMessage()
        {
            
            User ReceivedUser = MainMenu.SelectUser();
            if (ReceivedUser != null)
            {
                Console.WriteLine("Write a message:\n");
                Console.CursorVisible = true;
                Message NewPersonalMessage = new Message()
                {
                    SenderId = ActiveUser.Id,
                    ReceiverId = ReceivedUser.Id,
                    Body = Console.ReadLine()
                };
                while (NewPersonalMessage.Body.Length > 250)
                {
                    Console.WriteLine("\nThe message must be under 250 characters\nWrite a message: ");
                    NewPersonalMessage.Body = Console.ReadLine();
                }
                Console.WriteLine("\nMessage sent...");
                Thread.Sleep(1600);
                file.CreateFileMessageData(NewPersonalMessage);
                return DataHandler.CreateMessageData(NewPersonalMessage);
            }
            else { return false; }
        }

        public bool ShowReceivedMessages()
        {
            var ReceivedMessages = DataHandler.ReadMessages()
                .Where(Received => Received.ReceiverId == ActiveUser.Id)
                .ToList();
            if (ReceivedMessages.Count > 0)
            {
                ReceivedMessages.ForEach(x => Console.WriteLine($"{x.Sender.UserName} has sent you at {x.TimeSent}: {x.Body}"));
                Console.ReadKey();
                return true;
            }
            else
            {
                Console.WriteLine("No Messages to Display");
                Thread.Sleep(1600);
                return false;
            }
        }

        public bool ShowSentMessages()
        {
            var SentMessages = DataHandler.ReadMessages()
                .Where(Sent => Sent.SenderId == ActiveUser.Id)
                .ToList();
            if (SentMessages.Count > 0)
            {
                SentMessages.ForEach(x => Console.WriteLine($"You sent to {x.Receiver.UserName} at {x.TimeSent}: {x.Body}"));
                Console.ReadKey();
                return true;
            }
            else
            {
                Console.WriteLine("No Messages to Display");
                Thread.Sleep(1600);
                return false;
            }            
        }

        public List<Message> GetSentMessages()
        {
            return DataHandler.ReadMessages()
                 .Where(SendMessages => SendMessages.SenderId == ActiveUser.Id)
                 .ToList();
        }

        public List<Message> GetRecievedMessages()
        {
            return DataHandler.ReadMessages()
                .Where(ReceivedMessages => ReceivedMessages.ReceiverId == ActiveUser.Id)
                .ToList();
        }

        public Message GetWantedMessage(User ActiveUser)
        {
            string ReceivedOrSentMessage = MenuStyle.Horizontal(new List<string> { "Received", "Sent", "Back" });
            List<Message> Messages = new List<Message>();
            switch (ReceivedOrSentMessage)
            {
                case "Received":
                    Messages = GetRecievedMessages();
                    break;
                case "Sent":
                    Messages = GetSentMessages();
                    break;
                case "Back":
                    return null;
            }
            if (Messages.Count > 0)
            {
                string SelectedMessage = MenuStyle.Vertical(Messages.Select(m => "Message ID: " + m.Id.ToString() + ", at: " + $"{m.TimeSent.ToShortDateString()}" + $", {m.Sender.UserName} " + "sent: " + m.Body + ", to " + $"{m.Receiver.UserName}").ToList());
                return Messages.Single(pm => "Message ID: " + pm.Id.ToString() + ", at: " + $"{pm.TimeSent.ToShortDateString()}" + $", {pm.Sender.UserName} " + "sent: " + pm.Body + ", to " + $"{pm.Receiver.UserName}" == SelectedMessage);
            }
            else
            {
                Console.WriteLine("No Messages to Display");
                Thread.Sleep(1600);
                return null;
            }
        }

        public Message GetSentMessage(User ActiveUser)
        {
            string ReceivedOrSentMessage = MenuStyle.Horizontal(new List<string> { "Sent", "Back" });
            List<Message> Messages = new List<Message>();
            switch (ReceivedOrSentMessage)
            {
                case "Sent":
                    Messages = GetSentMessages();
                    break;
                case "Back":
                    return null;
            }
            if (Messages.Count > 0)
            {
                string SelectedMessage = MenuStyle.Vertical(Messages.Select(m => "Message ID: " + m.Id.ToString() + ", at: " + $"{m.TimeSent.ToShortDateString()}" + $", You " + "sent: " + m.Body + ", to " + $"{m.Receiver.UserName}").ToList());
                return Messages.Single(pm => "Message ID: " + pm.Id.ToString() + ", at: " + $"{pm.TimeSent.ToShortDateString()}" + $", You " + "sent: " + pm.Body + ", to " + $"{pm.Receiver.UserName}" == SelectedMessage);
            }
            else
            {
                Console.WriteLine("No Messages to Display");
                Thread.Sleep(1600);
                return null;
            }
        }

        public Message GetAnyWantedMessage()
        {
            string AnyMessage = MenuStyle.Horizontal(new List<string> { "All Messages", "Back"});
            List<Message> Messages = new List<Message>();
            switch (AnyMessage)
            {
                case "All Messages":
                    Messages = GetAllMessages();
                    break;
                default:
                    return null;
            }
            if (Messages.Count > 0)
            {
                string SelectedMessage = MenuStyle.Vertical(Messages.Select(m => "Message ID: " + m.Id.ToString() + ", at: " + $"{m.TimeSent.ToShortDateString()}"  + $", {m.Sender.UserName} " + "sent: " + m.Body + ", to " + $"{m.Receiver.UserName}").ToList());
                return Messages.Single(pm => "Message ID: " + pm.Id.ToString() + ", at: " + $"{pm.TimeSent.ToShortDateString()}" + $", {pm.Sender.UserName} " + "sent: " + pm.Body + ", to " + $"{pm.Receiver.UserName}" == SelectedMessage);
            }
            else
            {
                Console.WriteLine("No Messages to Display");
                Thread.Sleep(1600);
                return null;
            }
        }

        public List<Message> GetAllMessages()
        {
            return DataHandler.ReadMessages().ToList();
        }

        public bool ShowAllMessages()
        {
            var Messages = GetAllMessages().ToList();
            if (Messages.Count > 0)
            {
                Messages.ForEach(all => Console.WriteLine($"{all.TimeSent.ToShortDateString()}\t" + $"{all.Sender.UserName } sent " + $"to {all.Receiver.UserName}: " + $"{all.Body} "));
                Console.ReadKey();
                return true;
            }
            else
            {
                Console.WriteLine("No Messages to Display");
                Thread.Sleep(1600);
                return false;
            }
        }
      
        public bool UpdateMessage()
        {
            Message WantedMessage;
            if (ActiveUser.UserAccess == Role.administrator || ActiveUser.UserAccess == Role.moderator)
            {
                WantedMessage = GetAnyWantedMessage();               
            }
            else if ( ActiveUser.UserAccess == Role.user)
            {
                WantedMessage = GetSentMessage(ActiveUser);
            }
            else { return false; }

            Message UpdatedMessage = new Message()
            {
                SenderId = ActiveUser.Id
            };

            if (WantedMessage != null)
            {              
                Console.WriteLine($"Old Message: {WantedMessage.Body}");
                Console.WriteLine($"New Message: {UpdatedMessage.Body}");
                Console.CursorVisible = true;
                UpdatedMessage.Body = Console.ReadLine();
                while (UpdatedMessage.Body.Length > 250)
                {
                    Console.WriteLine("\nThe message must be under 250 characters\nWrite a message: ");
                    UpdatedMessage.Body = Console.ReadLine();
                }

                Console.WriteLine("Message just Edited");
                Thread.Sleep(1600);
                file.CreateFileMessageData(UpdatedMessage);
                return DataHandler.UpdateMessageBody(WantedMessage, UpdatedMessage.Body + " (edited)");
            }
            else { return false; }
        }
     
        public bool DeleteMessage()
        {
            Message WantedMessage;
            if (ActiveUser.UserAccess == Role.administrator || ActiveUser.UserAccess == Role.moderator)
            {
                WantedMessage = GetAnyWantedMessage();
            }
            else if (ActiveUser.UserAccess == Role.user)
            {
                WantedMessage = GetWantedMessage(ActiveUser);
            }
            else { return false; }

            if (WantedMessage != null)
            {
                Console.WriteLine("Message deleted");
                Thread.Sleep(1600);
                return DataHandler.DeleteMessage(WantedMessage, ActiveUser);
            }
            return false;
        }
    }
}
