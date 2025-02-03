using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_320_Stone_Sling
{
    internal class Tours
    {
        public string[] Tour {  get; set; }
        public ConsoleColor Color { get; set; }

        public Tours() 
        {
            Tour = new string[]
            {
                @"████",
                @"████",
                @"████",
                @"████",
                @"████",
                @"████",
                @"████",
                @"████"
            };
            Color = ConsoleColor.Yellow; //couleur par défaut
        }

        public void Afficher(int x, int y)
        {
            Console.ForegroundColor = Color;

            for (int i = 0; i < Tour.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(Tour[i]);
            }
            Console.ResetColor(); // Réinitialise la couleur par défaut
        }
    }
}
