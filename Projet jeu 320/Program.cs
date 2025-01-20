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

            // Création des joueurs
            Joueur j1 = new Joueur { Color = ConsoleColor.Blue }; //Joueur 1 en bleu
            Joueur j2 = new Joueur { Color = ConsoleColor.Red }; //Joueur 2 en rouge

            // Coordonnées pour affichage joueurs
            j1.Afficher(10, 5);
            j2.Afficher(30, 5);

            Console.ResetColor();
            Console.ReadLine();
        }

    }
}
