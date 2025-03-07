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
        public int PosX { get; set; }
        public int PosY { get; set; }

        public ConsoleColor Color { get; set; }

        /// <summary>
        /// constructeur pour la tour
        /// </summary>
        public Tours(int posX, int posY) 
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
            PosX = posX;
            PosY = posY;
        }

        /// <summary>
        /// Methode pour afficher la tour
        /// </summary>
        /// <param name="x">position X</param>
        /// <param name="y"></param>
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
