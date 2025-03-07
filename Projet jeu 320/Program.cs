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
            Joueur joueur1 = new Joueur(1, 10, 27) { Color = ConsoleColor.Cyan }; //Joueur 1 en bleu
            Joueur joueur2 = new Joueur(2, 135, 27) { Color = ConsoleColor.Red }; //Joueur 2 en rouge
            Tours tour1 = new Tours(20, 22);
            Tours tour2 = new Tours(125, 22);
            Projectile projectileJ1 = new Projectile(joueur1.Color);
            Projectile projectileJ2 = new Projectile(joueur2.Color);
            AimPoints aimPoints = new AimPoints(12, 23);
            HUD hudj1 = new HUD(joueur1.Number,10, 2, joueur1.Score = 0, joueur1.HP = "♥ ♥ ♥", joueur1.HpValue = 3, joueur1.Color);
            HUD hudj2 = new HUD(joueur2.Number,115, 2, joueur2.Score = 0, joueur2.HP = "♥ ♥ ♥", joueur2.HpValue = 3, joueur2.Color);

            // Coordonnées pour affichage Objets
            joueur1.Afficher(joueur1.PosX, joueur1.PosY);
            joueur2.Afficher(joueur2.PosX, joueur2.PosY);
            tour1.Afficher(tour1.PosX, tour1.PosY);
            tour2.Afficher(tour2.PosX, tour2.PosY);
            hudj1.Afficher(hudj1.PosX, hudj1.PosY);
            hudj2.Afficher(hudj2.PosX, hudj2.PosY);

            bool isAiming = true;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (isAiming)
            {
                int currentIndex = (int)(stopwatch.ElapsedMilliseconds / 400) % aimPoints.aimPoints.Length;
                float angle = CalculerAngle(stopwatch.ElapsedMilliseconds);
                aimPoints.Afficher(aimPoints.PosX, aimPoints.PosY, currentIndex);
                DebugAngle(angle);
                Thread.Sleep(100);

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Escape)
                    {
                        Environment.Exit(0);
                    }

                    if (key.Key == ConsoleKey.Spacebar)
                    {
                        isAiming = false;
                        stopwatch.Stop();

                        // Enregistrer l'angle relatif basé sur le temps écoulé avec une zone floue
                        Console.WriteLine($"Angle enregistré: {angle} degrés");
                    }
                }
                else
                {
                    aimPoints.Clear();
                }
            }

            StrengthBar strengthBar = new StrengthBar();
            strengthBar.Start(joueur1.Color);
            projectileJ1.Afficher(20, 20);


            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Escape)
                    {
                        Environment.Exit(0);
                    }

                    if (key.Key == ConsoleKey.LeftArrow)
                    {
                        joueur1.Score++;
                        hudj1 = new HUD(joueur1.Number,hudj1.PosX, hudj1.PosY, joueur1.Score, joueur1.HP, joueur1.HpValue, joueur1.Color);
                        hudj1.Afficher(hudj1.PosX, hudj1.PosY);
                    }

                    else if (key.Key == ConsoleKey.RightArrow)
                    {
                        joueur2.Score++;
                        hudj2 = new HUD(joueur2.Number,hudj2.PosX, hudj2.PosY, joueur2.Score, joueur2.HP, joueur2.HpValue, joueur2.Color);
                        hudj2.Afficher(hudj2.PosX, hudj2.PosY);
                    }

                    else if (key.Key == ConsoleKey.DownArrow && joueur1.HpValue > 0)
                    {
                        joueur1.HpValue--;
                        hudj1 = new HUD(joueur1.Number, hudj1.PosX, hudj1.PosY, joueur1.Score, joueur1.HP = hudj1.UpdateHP(joueur1.HpValue), joueur1.HpValue, joueur1.Color);
                        hudj1.Afficher(hudj1.PosX, hudj1.PosY);

                    }

                    else if (key.Key == ConsoleKey.UpArrow && joueur2.HpValue > 0)
                    {
                        joueur2.HpValue--;
                        hudj2 = new HUD(joueur2.Number, hudj2.PosX, hudj2.PosY, joueur2.Score, joueur2.HP = hudj2.UpdateHP(joueur2.HpValue), joueur2.HpValue, joueur2.Color);
                        hudj2.Afficher(hudj2.PosX, hudj2.PosY);

                    }
                }
            }
        }
        static float CalculerAngle(long elapsedMilliseconds)
        {
            Random random = new Random();
            float blurFactor = (float)(random.NextDouble() * 10.0 - 5.0); // Zone floue entre -5 et +5 degrés

            // Convertir le temps écoulé en angle
            float baseAngle = 90 - ((float)(elapsedMilliseconds % 1600) / 1600 * 90); // Ajuster le temps écoulé pour un cycle complet de 2400ms
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