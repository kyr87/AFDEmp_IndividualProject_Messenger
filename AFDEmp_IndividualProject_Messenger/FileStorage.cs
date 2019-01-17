using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AFDEmp_IndividualProject_Messenger
{
    class FileStorage
    {
        private string Path = Properties.Settings.Default.FilePath; 
        const string MessageTxt = "Messages.txt", UserTxt = "Users.txt";
     

        public bool CreateFileMessageData(Message personal)
        {
            return SaveData(Path + MessageTxt, personal.SenderId + " sent at" + ", " + personal.TimeSent + ", " + personal.Body + ", " + "to " + personal.ReceiverId + Environment.NewLine);
        }

        public bool CreateFileUserData(User user)
        {
            return SaveData(Path + UserTxt, user.Id + ", " + user.UserName + ", " + user.Password + ", " + (int)user.UserAccess + Environment.NewLine);
        }

        public bool SaveData(string path, string Data)
        {
            try
            {
                File.AppendAllText(path, Data);
                return true;
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                Thread.Sleep(1800);
                return false;
            }
        }
    }
}
