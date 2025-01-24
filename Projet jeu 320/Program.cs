using Projet_jeu_320;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_320_Stone_Sling
{
    internal class Program
    {
        static void Main()
        {
            //Pour afficher "●"
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            //Taille de la console
            Console.WindowHeight = 40;
            Console.WindowWidth = 150;

            // Création des joueurs
            Joueur joueur1 = new Joueur { Color = ConsoleColor.Cyan }; //Joueur 1 en bleu
            Joueur joueur2 = new Joueur { Color = ConsoleColor.Red }; //Joueur 2 en rouge
            Tours tour1 = new Tours();
            Tours tour2 = new Tours();
            Projectile projectile = new Projectile();
            AimPoints aimPoints = new AimPoints();

            // Coordonnées pour affichage joueurs
            joueur1.Afficher(10, 27);
            joueur2.Afficher(108, 27);
            tour1.Afficher(20, 23);
            tour2.Afficher(98, 23);
            projectile.Afficher(20, 20);
            aimPoints.Afficher(12, 23);
            

            Console.ReadLine();
        }
    }
}
