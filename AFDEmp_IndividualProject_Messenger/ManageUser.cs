using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AFDEmp_IndividualProject_Messenger
{
    class ManageUser
    {
        IDataHandling DataProvider;
        SignInRegister Create;
        FileStorage file;
        User ActiveUser;

        public ManageUser(IDataHandling dataprovider, User CurrentUser)
        {
            DataProvider = dataprovider;
            ActiveUser = CurrentUser;
            Create = new SignInRegister(DataProvider);
            file = new FileStorage();
        }

        public void CreateUser()
        {
            User NewUser = new User()
            {              
                UserName = Create.UsernameInput(true),              
                Password = Create.PasswordInput(),
                UserAccess = Role.user
            };
            file.CreateFileUserData(NewUser);
            DataProvider.CreateUserData(NewUser);
            Console.WriteLine($"\nUser {NewUser.UserName} has been created!");
            Thread.Sleep(1700);
        }

        public void DeleteUser()
        {
            User SelectedUser = MainMenu.SelectUser();
            if (SelectedUser != null)
            {
                string Selection = MenuStyle.Horizontal(new List<string>
                {
                    "Delete User",
                    "Back"
                });

                switch (Selection)
                {
                    case "Delete User":
                        DataProvider.DeleteUser(SelectedUser);
                        Console.WriteLine($"{SelectedUser.UserName} just deleted");
                        Thread.Sleep(1600);                      
                        break;
                    case "Back":
                        return;
                }
            }
            else { return; }           
        }

        public void UpdateUserAccess()
        {                  
            User SelectedUser = MainMenu.SelectUser();
            if (SelectedUser != null)
            {
                string UpdateSelection = MenuStyle.Horizontal(new List<string> { $"\n\t{SelectedUser.UserName} is {SelectedUser.UserAccess}, how do you want to change his permissions?" });
                if (SelectedUser.UserAccess == Role.administrator)
                {
                    UpdateSelection = MenuStyle.Horizontal(new List<string> { "Downgrade to Moderator", "Downgrade to User", "Downgrade to Visitor", "Back" });
                }
                else if (SelectedUser.UserAccess == Role.moderator)
                {
                    UpdateSelection = MenuStyle.Horizontal(new List<string> { "Upgrade to Administrator", "Downgrade to User", "Downgrade to Visitor", "Back" });
                }
                else if (SelectedUser.UserAccess == Role.user)
                {
                    UpdateSelection = MenuStyle.Horizontal(new List<string> { "Upgrade to Administrator", "Upgrade to Moderator", "Downgrade to Visitor", "Back" });
                }
                else
                {
                    UpdateSelection = MenuStyle.Horizontal(new List<string> { "Upgrade to Administrator", "Upgrade to Moderator", "Upgrade to User", "Back" });
                }

                if (UpdateSelection.Contains("Administrator"))
                {
                    SelectedUser.UserAccess = Role.administrator;
                }
                else if (UpdateSelection.Contains("Moderator"))
                {
                    SelectedUser.UserAccess = Role.moderator;
                }
                else if (UpdateSelection.Contains("User"))
                {
                    SelectedUser.UserAccess = Role.user;
                }
                else if (UpdateSelection.Contains("Visitor"))
                {
                    SelectedUser.UserAccess = Role.visitor;
                }
                else
                {
                    return;
                }
                Console.WriteLine($"{SelectedUser.UserName} updated to {SelectedUser.UserAccess}");
                Thread.Sleep(1700);
                DataProvider.UpdateUserRole(SelectedUser);
            }
            else { return; }
        }
    }
}
