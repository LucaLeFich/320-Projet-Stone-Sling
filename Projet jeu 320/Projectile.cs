using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_jeu_320
{
    internal class Projectile
    {
        public char _projectile {  get; set; }

        public Projectile()
        {
            _projectile = '●';
        }

        public void Afficher(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(_projectile);
        }
    }
}
