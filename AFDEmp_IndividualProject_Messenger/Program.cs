using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AFDEmp_IndividualProject_Messenger
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                IDataHandling DataReader = new DatabaseAccess();
                SignInRegister Screen = new SignInRegister(DataReader);
                User OnlineUser = Screen.SignInOrRegister(DataReader);
                MainMenu MainMenuSelection = new MainMenu(DataReader, OnlineUser);
                MainMenuSelection.ShowMenu();
            }
        }
    }
}
