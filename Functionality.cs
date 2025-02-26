using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_c_
{
    public static class Functionality
    {
        enum literals
        {
            CONTINUE = 1,
            EXIT = 2
        };
        public static bool Looping()
        {
            int response;
            while (true)
            {
                AdditionalInfo.LoopMenu();
                response = InputHandler.GetInput<int>(" - ");
                if (response == (int)literals.CONTINUE) { return true; }
                else if (response == (int)literals.EXIT) { return false; }
                Console.WriteLine(" Ошибка ввода! ");
            }
        }
    }
}
