using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_320_Stone_Sling
{
    internal class Joueur_2
    {
        public string[] Player2 =
        {
            @" o",
            @"/░\",
            @"/ \",
        };

        public void Afficher()
        {
            // Affiche chaque ligne du tableau Player1
            foreach (string ligne in Player2)
            {
                Console.WriteLine(ligne);
            }
        }
    }
}
