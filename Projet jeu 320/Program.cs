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

            // Création des Objets
            Joueur joueur1 = new Joueur(1) { Color = ConsoleColor.Cyan }; //Joueur 1 en bleu
            Joueur joueur2 = new Joueur(2) { Color = ConsoleColor.Red }; //Joueur 2 en rouge
            Tours tour1 = new Tours();
            Tours tour2 = new Tours();
            Projectile projectile = new Projectile();
            AimPoints aimPoints = new AimPoints();
            HUD hudj1 = new HUD(joueur1._number, joueur1._score = 0, joueur1._hp = "♥ ♥ ♥", joueur1._hpValue = 3) { Color = ConsoleColor.Cyan };
            HUD hudj2 = new HUD(joueur2._number, joueur2._score = 0, joueur2._hp = "♥ ♥ ♥", joueur2._hpValue = 3) { Color = ConsoleColor.Red };

            // Coordonnées pour affichage Objets
            joueur1.Afficher(10, 27);
            joueur2.Afficher(135, 27);
            tour1.Afficher(20, 22);
            tour2.Afficher(125, 22);
            projectile.Afficher(20, 20);
            aimPoints.Afficher(12, 23);
            hudj1.Afficher(10, 2);
            hudj2.Afficher(115, 2);

            while (true)
            {
               var key = Console.ReadKey();
                if (key.Key == ConsoleKey.LeftArrow)
                {
                    joueur1._score++;
                    hudj1 = new HUD(joueur1._number, joueur1._score, joueur1._hp = "♥ ♥ ♥", joueur1._hpValue = 3) { Color = ConsoleColor.Cyan };
                    hudj1.Afficher(10, 2);
                }

                var key2 = Console.ReadKey();
                if (key2.Key == ConsoleKey.RightArrow)
                {
                    joueur2._score++;
                    hudj2 = new HUD(joueur2._number, joueur2._score, joueur2._hp = "♥ ♥ ♥", joueur2._hpValue = 3) { Color = ConsoleColor.Red };
                    hudj2.Afficher(115, 2);
                }

                var key3 = Console.ReadKey();
                if (key3.Key == ConsoleKey.UpArrow)
                {
                    joueur2._hpValue--;
                    hudj2 = new HUD(joueur2._number, joueur2._score, joueur2._hp = "♥ ♥ ♥", joueur2._hpValue = 3) { Color = ConsoleColor.Red };
                    hudj2.Afficher(115, 2);
                }
            }           

            Console.ReadLine();
        }
    }
}
