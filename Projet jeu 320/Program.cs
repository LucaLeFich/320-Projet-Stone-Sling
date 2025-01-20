using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_320_Stone_Sling
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WindowHeight = 40;
            Console.WindowWidth = 150;

            Joueur_1 j1 = new Joueur_1();
            j1.Afficher();  // Affiche le personnage

            Joueur_2 j2 = new Joueur_2();
            j2.Afficher();  // Affiche le personnage

            Console.ReadLine();
        }
    }
}
