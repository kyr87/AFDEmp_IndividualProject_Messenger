using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AFDEmp_IndividualProject_Messenger
{
    class SignInRegister
    {
        IDataHandling DataProvider;
        int tries;

        public SignInRegister(IDataHandling DataSelected)
        {
            DataProvider = DataSelected;
        }

        public User SignInOrRegister(IDataHandling DataProvider)
        {
            string UsersChoice = MenuStyle.Horizontal(new List<string> { "Sign In", "Register", "Exit" });

            if (UsersChoice == "Exit")
            {
                Console.WriteLine("\tThank you for watching");
                Thread.Sleep(1600);
                Environment.Exit(0);
            }
            else if (UsersChoice == "Register")
            {
                FileStorage file = new FileStorage();
                UserData Data = GetInputUserData(true);
                User NewUser = new User()
                {
                    UserName = Data.InputName,
                    Password = Data.InputPassword,
                    UserAccess = DataProvider.EmptyStorage() ? Role.administrator : Role.visitor
                };
                DataProvider.CreateUserData(NewUser);
                file.CreateFileUserData(NewUser);
                Console.WriteLine($"\nNew User {NewUser.UserName} was created successfully");
                Thread.Sleep(1600);            
                return NewUser;
            }
            else if (UsersChoice == "Sign In")
            {
                tries = 3;
                do
                {
                    User ActiveUser = SigninUser();
                    if (ActiveUser == null)
                    {
                        Console.WriteLine("\nInvalid Username or Password, Please try again");                       
                        tries--;
                        Console.WriteLine($"Remaining Tries {tries}/3");
                    }
                    else
                    {
                        Console.WriteLine($"\nWelcome { ActiveUser.UserName } !!!");
                        Thread.Sleep(1600);
                        return ActiveUser;
                    }
                    if (tries == 0)
                    {
                        Console.WriteLine("\nToo many false attempts\n\nExit from application");
                        Thread.Sleep(1800);
                        Environment.Exit(0);
                    }
                }
                while (true);
            }
            return null;
        }
        //Class is a reference type. Structs are mainly useful to hold small data values and includes a value type entity.
        //Structs are custom value types that store the values in each field together. They do not store referenced data, such as the character array in a string.
        internal struct UserData
        {
            internal string InputName;
            internal string InputPassword;
        }

        internal UserData GetInputUserData(bool condition)
        {
            UserData InputData = new UserData();
            InputData.InputName = UsernameInput(condition);
            InputData.InputPassword = PasswordInput();
            return InputData;
        }

        public User SigninUser()
        {
            UserData Data = GetInputUserData(false);

            return DataProvider
                .ReadUserData()
                .SingleOrDefault(Found => Found.UserName == Data.InputName && Found.Password == Data.InputPassword);
        }

        public string UsernameInput(bool condition)
        {
            Console.Write("Username:\n");
            Console.CursorVisible = true;
            string username;
            while (true)
            {
                username = Console.ReadLine();
                if (!(username.Length >= 5 && username.Length <= 20))
                {
                    Console.Write("Wrong. Input 5-20 letters. Try again!\nUsername:\n");

                }
                else if (!(username.All(char.IsLetter)))
                {
                    Console.Write("Wrong. Input only letters. Try again!\nUsername:\n");
                }
                else if (condition && DataProvider.UsernameExists(username))
                {
                    Console.WriteLine("Username exists, try another\n\nUsername:");
                }
                else
                {
                    return username;
                }
            }
        }

        public string PasswordInput()
        {
            Console.Write("Password:\n");
            Console.CursorVisible = true;
            
            while (true)
            {
                //ConsoleKeyInfo Struct describes the console key that was pressed
                ConsoleKeyInfo keypress;
                string password = string.Empty;
                do
                {
                    keypress = Console.ReadKey(true);
                    if (keypress.Key != ConsoleKey.Backspace && keypress.Key != ConsoleKey.Enter)
                    {
                        password += keypress.KeyChar;
                        Console.Write("*");
                    }
                    else if (keypress.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Remove(password.Length - 1);
                        Console.Write("\b \b");
                    }
                } while (keypress.Key != ConsoleKey.Enter);
                if (!(password.Length >= 5 && password.Length <= 20))
                {
                    Console.Write("\nWrong. Length must be 5-20. Try again!\nPassword:\n");

                }
                if (password.All(char.IsLetter) || password.All(char.IsNumber) || password.All(char.IsPunctuation))
                {
                    Console.Write("\nPassword must contain both letters and numbers. Try again!\nPassword:\n");
                }
                else if(password.Contains(' '))
                {
                    Console.WriteLine("\nWrong. Password must not contains Space. Try again!\nPassword:");
                }
                else
                {
                    return HashCode.sha256_hash(password);
                }
            }
        }
    }
}
