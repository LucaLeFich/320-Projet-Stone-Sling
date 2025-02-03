using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_jeu_320
{
    internal class AimPoints
    {
        public string[] aimPoints {  get; set; }

        public AimPoints()
        {
            aimPoints = new string[]
            {
                @"·",
                @" ·",
                @"  ·",
                @"   ·"
            };
        }

        public void Afficher(int x, int y)
        {
            for (int i = 0; i < aimPoints.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(aimPoints[i]);
            }
        }
    }
}
