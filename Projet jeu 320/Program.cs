using Projet_jeu_320;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Projet_320_Stone_Sling
{
    internal class Program
    {
        static void Main()
        {
            Console.CursorVisible = false;

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
            hudj1.UpdateBar(0);
            hudj1.Afficher(10, 2);
            hudj2.Afficher(115, 2);

            bool isAiming = true;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (isAiming)
            {
                int currentIndex = (int)(stopwatch.ElapsedMilliseconds / 400) % aimPoints.aimPoints.Length;
                float angle = CalculerAngle(stopwatch.ElapsedMilliseconds);
                aimPoints.Afficher(12, 23, currentIndex);
                DebugAngle(angle);
                Thread.Sleep(100);

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Spacebar)
                    {
                        isAiming = false;
                        stopwatch.Stop();

                        // Enregistrer l'angle relatif basé sur le temps écoulé avec une zone floue
                        //float angle = CalculerAngle(stopwatch.ElapsedMilliseconds);
                        Console.WriteLine($"Angle enregistré: {angle} degrés");
                    }
                }
                else
                {
                    aimPoints.Clear();
                }
            }

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

                    else if (key.Key == ConsoleKey.DownArrow && joueur1.HpValue > 0)
                    {
                        joueur1.HpValue--;
                        hudj1 = new HUD(joueur1.Number, joueur1.Score, joueur1.HP = hudj1.UpdateHP(joueur1.HpValue), joueur1.HpValue) { Color = ConsoleColor.Cyan };
                        hudj1.Afficher(10, 2);

                    }

                    else if (key.Key == ConsoleKey.UpArrow && joueur2.HpValue > 0)
                    {
                        joueur2.HpValue--;
                        hudj2 = new HUD(joueur2.Number, joueur2.Score, joueur2.HP = hudj2.UpdateHP(joueur2.HpValue), joueur2.HpValue) { Color = ConsoleColor.Red };
                        hudj2.Afficher(115, 2);

                    }
                }
            }
        }
        static float CalculerAngle(long elapsedMilliseconds)
        {
            Random random = new Random();
            float blurFactor = (float)(random.NextDouble() * 10.0 - 5.0); // Zone floue entre -5 et +5 degrés

            // Convertir le temps écoulé en angle
            float baseAngle = 90 - ((float)(elapsedMilliseconds % 2000) / 2000 * 90); // Ajuster le temps écoulé pour un cycle complet de 2400ms
            float angle = baseAngle + blurFactor;
            return Clamp(angle, 0f, 90f); // Contraindre l'angle entre 0 et 90 degrés
        }


        static float Clamp(float value, float min, float max)
        {
            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        static void DebugAngle(float angle)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write($"Angle actuel: {angle:F2} degrés");
        }
    }
}