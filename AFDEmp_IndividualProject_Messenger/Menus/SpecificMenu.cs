using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFDEmp_IndividualProject_Messenger
{
    class SpecificMenu
    {
        IDataHandling DataProvider;
        User ActiveUser;
        ManageUser mu;
        MessageActions PersonalMessage;

        public SpecificMenu(IDataHandling dataprovider, User Current)
        {
            DataProvider = dataprovider;
            ActiveUser = Current;
            PersonalMessage = new MessageActions(DataProvider, ActiveUser);
            mu = new ManageUser(DataProvider, ActiveUser);
        }
        public void MessagesMenu()
        {
            while (true)
            {
                string Choice = MenuStyle.Vertical(new List<string>
                {
                "Create Message",
                "Inbox",
                "View Sent",
                "Show All Messages",
                "Edit Message",
                "Delete Message",
                "Back"
                });

                switch (Choice)
                {
                    case "Create Message":
                        PersonalMessage.CreateMessage();
                        break;
                    case "Inbox":
                        PersonalMessage.ShowReceivedMessages();
                        break;
                    case "View Sent":
                        PersonalMessage.ShowSentMessages();
                        break;
                    case "Show All Messages":
                        PersonalMessage.ShowAllMessages();
                        break;
                    case "Edit Message":
                        PersonalMessage.UpdateMessage();
                        break;
                    case "Delete Message":
                        PersonalMessage.DeleteMessage();
                        break;
                    case "Back":
                        return;
                }
            }          
        }

        public void ManageUserMenu()
        {           
            while (true)
            {
                string AdminSelection = MenuStyle.Vertical(new List<string>
                {
                    "Create User",
                    "Update User Access",
                    "Delete User",
                    "Back"
                });
                switch (AdminSelection)
                {
                    case "Create User":
                        mu.CreateUser();
                        break;
                    case "Update User Access":
                        mu.UpdateUserAccess();
                        break;
                    case "Delete User":
                        mu.DeleteUser();
                        break;
                    default:
                        return;
                }
            }
        }
    }
}
