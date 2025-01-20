using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Projet_320_Stone_Sling
{
    internal class Joueur_1
    {
        public string[] Player1 =
        {
            @" o",
            @"/░\",
            @"/ \",
        };

        private int positionX;
        private int positionY;

        public void Draw()
        {
            string[] view = Player1;
            for (int i = 0; i < view.Length; i++)
            {
                Console.SetCursorPosition(positionX, positionY + i);
                Console.WriteLine(view[i]);
            }
        }

    }
}
