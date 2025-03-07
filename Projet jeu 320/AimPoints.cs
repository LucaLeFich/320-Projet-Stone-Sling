using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_320_Stone_Sling
{
    internal class AimPoints
    {
        public string[] aimPoints { get; set; }

        public int PosX { get; set; }
        public int PosY { get; set; }
        public ConsoleColor Color { get; set; }
        public AimPoints(int posX, int posY)
        {
            aimPoints = new string[]
            {
                @"●",
                @" ●",
                @"  ●",
                @"   ●"
            };
            Color = ConsoleColor.Green;
            PosX = posX;
            PosY = posY;
        }

        /// <summary>
        /// Méthode pour afficher les aimpoints
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="activeIndex"></param>
        public void Afficher(int x, int y, int activeIndex)
        {
            Console.SetCursorPosition(x, y + activeIndex);
            Console.ForegroundColor = Color;
            Console.Write(aimPoints[activeIndex]);
        }

        /// <summary>
        /// Méthode qui efface les aimpoints pour en afficher que un a la fois
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < aimPoints.Length; i++)
            {
                Console.SetCursorPosition(12, 23 + i);
                Console.Write(new string(' ', aimPoints[i].Length));
            }
        }
    }
}