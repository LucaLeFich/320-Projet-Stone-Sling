using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_jeu_320
{
    internal class Projectile
    {
        public char projectile {  get; set; }

        public Projectile()
        {
            projectile = '●';
        }

        public void Afficher(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(projectile);
        }
    }
}
