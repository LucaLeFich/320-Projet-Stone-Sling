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

        private int posX;
        public int PosX
        {
            get { return posX; }
            set { posX = value; }
        }

        private int posY;
        public int PosY
        {
            get { return posY; }
            set { posY = value; }
        }

        public ConsoleColor Color { get; set; }

        public AimPoints(int posX, int posY, bool isMirrored = false)
        {
            if (isMirrored)
            {
                aimPoints = new string[]
                {
                    @"   ●",
                    @"  ●",
                    @" ●",
                    @"●"
                };
            }
            else
            {
                aimPoints = new string[]
                {
                    @"●",
                    @" ●",
                    @"  ●",
                    @"   ●"
                };
            }
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
        public void Draw(int x, int y, int activeIndex)
        {
            Console.SetCursorPosition(x, y + activeIndex);
            Console.ForegroundColor = Color;
            Console.Write(aimPoints[activeIndex]);
        }

        /// <summary>
        /// Méthode qui efface les aimpoints pour n'en afficher qu'un à la fois
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < aimPoints.Length; i++)
            {
                Console.SetCursorPosition(PosX, PosY + i);
                Console.Write(new string(' ', aimPoints[i].Length));
            }
        }
    }
}