using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AFDEmp_IndividualProject_Messenger
{
    internal class MainMenu
    {
        public static IDataHandling DataProvider { get; set; }
        public static User ActiveUser { get; set; }

        public MainMenu(IDataHandling dataprovider, User CurrentUser)
        {
            DataProvider = dataprovider;
            ActiveUser = CurrentUser;
        }

        public void ShowMenu()
        {
            if (ActiveUser != null)
            {
                MessageActions View = new MessageActions(DataProvider, ActiveUser);
                SignInRegister InitialMenu = new SignInRegister(DataProvider);
                SpecificMenu MenuActions = new SpecificMenu(DataProvider, ActiveUser);
                while (true)
                {

                    string selecteditem = string.Empty;

                    switch (ActiveUser.UserAccess)
                    {
                        case Role.visitor:
                            selecteditem = MenuStyle.Vertical(new List<string> { "Create Message", "Inbox", "View Sent", "Log Out", "Exit" });
                            break;
                        case Role.user:
                            selecteditem = MenuStyle.Vertical(new List<string> { "Create Message", "Inbox", "View Sent", "Edit Message", "Delete Message", "Log Out", "Exit" });
                            break;
                        case Role.moderator:
                            selecteditem = MenuStyle.Vertical(new List<string> { "Messages", "Log Out", "Exit" });
                            break;
                        case Role.administrator:
                            selecteditem = MenuStyle.Vertical(new List<string> { "Manage Users", "Messages", "Log Out", "Exit" });
                            break;
                    }
                    switch (selecteditem)
                    {
                        case "Manage Users":
                            MenuActions.ManageUserMenu();
                            break;
                        case "Messages":
                            MenuActions.MessagesMenu();
                            break;
                        case "Create Message":
                            View.CreateMessage();
                            break;
                        case "Inbox":
                            View.ShowReceivedMessages();
                            break;
                        case "View Sent":
                            View.ShowSentMessages();
                            break;
                        case "Edit Message":
                            View.UpdateMessage();
                            break;
                        case "Delete Message":
                            View.DeleteMessage();
                            break;
                        case "Log Out":
                            return;
                        case "Exit":
                            Console.WriteLine("\tThank you for watching");
                            Thread.Sleep(1600);
                            Environment.Exit(0);
                            break;

                    }
                }
            }
        }

        public static User SelectUser()
        {
            List<User> UsersList = DataProvider.ReadUserData();
            List<string> UsernameList = UsersList
                .Where(user => user.Id != ActiveUser.Id)
                .Select(UserInList => UserInList.UserName)
                .ToList();
            if (UsernameList.Count > 0)
            {
                string SelectedUsername = MenuStyle.Vertical(UsernameList);
                return UsersList.Single(UserOfList => SelectedUsername == UserOfList.UserName);
            }
            else
            {
                Console.WriteLine("No Users to Display");
                Thread.Sleep(1600);
                return null;
            }           
        }
    }
}
