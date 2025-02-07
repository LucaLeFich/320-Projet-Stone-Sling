using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_jeu_320
{
    internal class AimPoints
    {
        public string[] aimPoints { get; set; }

        public ConsoleColor Color { get; set; }
        public AimPoints()
        {
            aimPoints = new string[]
            {
                @"●",
                @" ●",
                @"  ●",
                @"   ●"
            };
            Color = ConsoleColor.Green;
        }

        public void Afficher(int x, int y, int activeIndex)
        {
            Console.SetCursorPosition(x, y + activeIndex);
            Console.ForegroundColor = Color;
            Console.Write(aimPoints[activeIndex]);
        }

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