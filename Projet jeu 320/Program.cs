﻿using Projet_jeu_320;
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
            HUD hudj1 = new HUD(joueur1.Number, joueur1.Score = 0, joueur1.HP = "♥ ♥ ♥", joueur1.HpValue = 3) { Color = ConsoleColor.Cyan };
            HUD hudj2 = new HUD(joueur2.Number, joueur2.Score = 0, joueur2.HP = "♥ ♥ ♥", joueur2.HpValue = 3) { Color = ConsoleColor.Red };

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
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.LeftArrow)
                    {
                        joueur1.Score++;
                        hudj1 = new HUD(joueur1.Number, joueur1.Score, joueur1.HP, joueur1.HpValue) { Color = ConsoleColor.Cyan };
                        hudj1.Afficher(10, 2);
                    }
                   
                    else if (key.Key == ConsoleKey.RightArrow)
                    {
                        joueur2.Score++;
                        hudj2 = new HUD(joueur2.Number, joueur2.Score, joueur2.HP, joueur2.HpValue) { Color = ConsoleColor.Red };
                        hudj2.Afficher(115, 2);
                    }

                    else if (key.Key == ConsoleKey.UpArrow)
                    {
                        joueur2.HpValue--;
                        hudj2.UpdateHP();
                        hudj2 = new HUD(joueur2.Number, joueur2.Score, joueur2.HP, joueur2.HpValue) { Color = ConsoleColor.Red };
                        hudj2.UpdateHP();
                        hudj2.Afficher(115, 2);
                        
                    }
                }
            }           
            Console.ReadLine();
        }
    }
}
