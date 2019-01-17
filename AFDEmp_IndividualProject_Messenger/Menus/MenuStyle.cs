using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFDEmp_IndividualProject_Messenger
{
    public class MenuStyle
    {
        public static string Vertical(List<string> MenutoshowVertical)
        {
            int Selectedoption = 0;
            ConsoleKeyInfo UserChoice;
            Console.CursorVisible = false;
            do
            {
                Console.Clear();
                for (int option = 0; option < MenutoshowVertical.Count; option++)
                {
                    if (option == Selectedoption) { Console.ForegroundColor = ConsoleColor.Green; }

                    else { Console.ForegroundColor = ConsoleColor.White; }
                    Console.WriteLine("\t" + MenutoshowVertical[option] + " \n");
                }
                UserChoice = Console.ReadKey();

                if (UserChoice.Key == ConsoleKey.UpArrow)
                {
                    if (Selectedoption == 0) { Selectedoption = MenutoshowVertical.Count - 1; }
                    else { Selectedoption--; }
                }
                if (UserChoice.Key == ConsoleKey.DownArrow)
                {
                    if (Selectedoption == MenutoshowVertical.Count - 1) { Selectedoption = 0; }
                    else { Selectedoption++; }
                }

            }
            while (UserChoice.Key != ConsoleKey.Enter);
            Console.Clear();
            return MenutoshowVertical[Selectedoption];
        }

        public static string Horizontal(List<string> MenuToShowHorizontal)
        {
            int CurrentOption = 0;
            ConsoleKeyInfo UserKeyPressed;
            Console.CursorVisible = false;
            do
            {
                Console.Clear();
                for (int option = 0; option < MenuToShowHorizontal.Count; option++)
                {
                    if (option == CurrentOption) { Console.ForegroundColor = ConsoleColor.Yellow; }
                    else { Console.ForegroundColor = ConsoleColor.White; }
                    Console.Write("\t" + MenuToShowHorizontal[option] + " \t");
                }
                UserKeyPressed = Console.ReadKey();
                if (UserKeyPressed.Key == ConsoleKey.LeftArrow)
                {
                    if (CurrentOption == 0) { CurrentOption = MenuToShowHorizontal.Count - 1; }
                    else { CurrentOption--; }
                }
                else if (UserKeyPressed.Key == ConsoleKey.RightArrow)
                {
                    if (CurrentOption == MenuToShowHorizontal.Count - 1) { CurrentOption = 0; }
                    else { CurrentOption++; }
                }
            }
            while (UserKeyPressed.Key != ConsoleKey.Enter);
            Console.Clear();
            return MenuToShowHorizontal[CurrentOption];
        }
    }
}
