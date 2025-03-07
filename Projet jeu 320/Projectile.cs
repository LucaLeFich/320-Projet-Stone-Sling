using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_320_Stone_Sling
{
    internal class Projectile
    {
        public char projectile {  get; set; }
        public ConsoleColor Color { get; set; }

        public Projectile(ConsoleColor color)
        {
            projectile = '●';

            Color = color;
        }

        
        public void Afficher(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(projectile);
        }
    }
}
