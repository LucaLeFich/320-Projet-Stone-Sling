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
            Player player1 = new Player(1, 10, 37) { Color = ConsoleColor.Cyan }; //Joueur 1 en bleu
            Player player2 = new Player(2, 135, 37) { Color = ConsoleColor.Red }; //Joueur 2 en rouge
            Towers tower1 = new Towers(20, 32);
            Towers tower2 = new Towers(125, 32);
            Projectile projectileJ1 = new Projectile(player1.Color);
            Projectile projectileJ2 = new Projectile(player2.Color);
            AimPoints aimPointsJ1 = new AimPoints(12, 33);
            AimPoints aimPointsJ2 = new AimPoints(133, 33, true); // Ajout pour joueur 2 avec inversion
            HUD hudP1 = new HUD(player1.Number, 10, 2, player1.Score = 0, player1.HP = "♥ ♥ ♥", player1.HpValue = 3, player1.Color);
            HUD hudP2 = new HUD(player2.Number, 115, 2, player2.Score = 0, player2.HP = "♥ ♥ ♥", player2.HpValue = 3, player2.Color);

            // Coordonnées pour affichage Objets
            player1.Draw(player1.PosX, player1.PosY);
            player2.Draw(player2.PosX, player2.PosY);
            tower1.Draw(tower1.PosX, tower1.PosY);
            tower2.Draw(tower2.PosX, tower2.PosY);
            hudP1.Draw(hudP1.PosX, hudP1.PosY);
            hudP2.Draw(hudP2.PosX, hudP2.PosY);

            // Joueur 1 - Sélection de l'angle
            bool isAiming = true;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            float angleJ1 = 0;

            while (isAiming)
            {
                int currentIndexJ1 = (int)(stopwatch.ElapsedMilliseconds / 400) % aimPointsJ1.aimPoints.Length;
                angleJ1 = CalulateAngle(stopwatch.ElapsedMilliseconds);
                aimPointsJ1.Draw(aimPointsJ1.PosX, aimPointsJ1.PosY, currentIndexJ1);
                DebugAngle(angleJ1, 0);
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
                        Console.WriteLine($"Angle joueur 1 enregistré: {angleJ1} degrés");
                    }
                }
                else
                {
                    aimPointsJ1.Clear();
                }
            }

            StrengthBar strengthBarP1 = new StrengthBar();
            strengthBarP1.Start(10, 7, player1.Color);
            double player1Force = strengthBarP1.GetChargeLevel();
            var (player1HeadX, player1HeadY) = player1.GetHeadPosition();
            projectileJ1.Throw(player1Force, angleJ1, player1HeadX, player1HeadY);

            // Joueur 2 - Sélection de l'angle
            isAiming = true;
            stopwatch.Restart();
            float angleJ2 = 0;

            while (isAiming)
            {
                int currentIndexJ2 = (int)(stopwatch.ElapsedMilliseconds / 400) % aimPointsJ2.aimPoints.Length;
                angleJ2 = CalulateAngle(stopwatch.ElapsedMilliseconds);
                aimPointsJ2.Draw(aimPointsJ2.PosX, aimPointsJ2.PosY, currentIndexJ2);
                DebugAngle(angleJ2, 1);
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
                        Console.WriteLine($"Angle joueur 2 enregistré: {angleJ2} degrés");
                    }
                }
                else
                {
                    aimPointsJ2.Clear();
                }
            }

            StrengthBar strengthBarP2 = new StrengthBar();
            strengthBarP2.Start(10, 7, player2.Color);
            double player2Force = strengthBarP2.GetChargeLevel();
            var (player2HeadX, player2HeadY) = player2.GetHeadPosition();
            projectileJ2.Throw(player2Force, angleJ2, player2HeadX, player2HeadY);

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
                        player1.Score++;
                        hudP1 = new HUD(player1.Number, hudP1.PosX, hudP1.PosY, player1.Score, player1.HP, player1.HpValue, player1.Color);
                        hudP1.Draw(hudP1.PosX, hudP1.PosY);
                    }

                    else if (key.Key == ConsoleKey.RightArrow)
                    {
                        player2.Score++;
                        hudP2 = new HUD(player2.Number, hudP2.PosX, hudP2.PosY, player2.Score, player2.HP, player2.HpValue, player2.Color);
                        hudP2.Draw(hudP2.PosX, hudP2.PosY);
                    }

                    else if (key.Key == ConsoleKey.DownArrow && player1.HpValue > 0)
                    {
                        player1.HpValue--;
                        hudP1 = new HUD(player1.Number, hudP1.PosX, hudP1.PosY, player1.Score, player1.HP = hudP1.UpdateHP(player1.HpValue), player1.HpValue, player1.Color);
                        hudP1.Draw(hudP1.PosX, hudP1.PosY);
                    }

                    else if (key.Key == ConsoleKey.UpArrow && player2.HpValue > 0)
                    {
                        player2.HpValue--;
                        hudP2 = new HUD(player2.Number, hudP2.PosX, hudP2.PosY, player2.Score, player2.HP = hudP2.UpdateHP(player2.HpValue), player2.HpValue, player2.Color);
                        hudP2.Draw(hudP2.PosX, hudP2.PosY);
                    }
                }
            }
        }

        static float CalulateAngle(long elapsedMilliseconds)
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
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        static void DebugAngle(float angle, int player)
        {
            Console.SetCursorPosition(0, player);
            Console.Write($"Angle joueur {player + 1}: {angle:F2} degrés");
        }
    }
}