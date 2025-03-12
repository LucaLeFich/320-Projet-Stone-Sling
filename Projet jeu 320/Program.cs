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
        // Déclaration des joueurs et HUDs en tant que variables de classe
        static Player player1;
        static Player player2;
        static HUD hudP1;
        static HUD hudP2;
        static Towers tower1;
        static Towers tower2;

        static void Main()
        {
            Console.CursorVisible = false;

            //Pour afficher "●"
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            //Taille de la console
            Console.WindowHeight = 40;
            Console.WindowWidth = 150;

            // Création des Objets
            player1 = new Player(1, 10, 37) { Color = ConsoleColor.Cyan }; //Joueur 1 en bleu
            player2 = new Player(2, 135, 37) { Color = ConsoleColor.Red }; //Joueur 2 en rouge
            tower1 = new Towers(20, 32);
            tower2 = new Towers(125, 32);
            Projectile projectileJ1 = new Projectile(player1.Color);
            Projectile projectileJ2 = new Projectile(player2.Color);
            AimPoints aimPointsJ1 = new AimPoints(12, 33);
            AimPoints aimPointsJ2 = new AimPoints(133, 33, true); // Ajout pour joueur 2 avec inversion
            hudP1 = new HUD(player1.Number, 10, 2, player1.Score = 0, player1.HP = "♥ ♥ ♥", player1.HpValue = 3, player1.Color);
            hudP2 = new HUD(player2.Number, 115, 2, player2.Score = 0, player2.HP = "♥ ♥ ♥", player2.HpValue = 3, player2.Color);

            // Coordonnées pour affichage Objets
            player1.Draw(player1.PosX, player1.PosY);
            player2.Draw(player2.PosX, player2.PosY);
            tower1.Draw(tower1.PosX, tower1.PosY);
            tower2.Draw(tower2.PosX, tower2.PosY);
            hudP1.Draw(hudP1.PosX, hudP1.PosY);
            hudP2.Draw(hudP2.PosX, hudP2.PosY);

            bool isRunning = true;

            while (isRunning)
            {
                // Tour du joueur 1
                PlayTurn(player1, projectileJ1, aimPointsJ1, hudP1, hudP2, false);

                // Tour du joueur 2
                PlayTurn(player2, projectileJ2, aimPointsJ2, hudP1, hudP2, true);
            }
        }

        // Méthode pour gérer le tour d'un joueur
        static void PlayTurn(Player player, Projectile projectile, AimPoints aimPoints, HUD hudP1, HUD hudP2, bool isReversed)
        {
            bool isAiming = true;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            float angle = 0;

            // Réinitialiser la position et le temps du projectile
            projectile.Reset();

            // Sélection de l'angle
            while (isAiming)
            {
                int currentIndex = (int)(stopwatch.ElapsedMilliseconds / 400) % aimPoints.aimPoints.Length;
                angle = CalulateAngle(stopwatch.ElapsedMilliseconds);
                aimPoints.Draw(aimPoints.PosX, aimPoints.PosY, currentIndex);
                DebugAngle(angle, player.Number - 1);
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
                        Console.WriteLine($"Angle joueur {player.Number} enregistré: {angle} degrés");
                    }
                }
                else
                {
                    aimPoints.Clear();
                }
            }

            // Sélection de la force
            StrengthBar strengthBar = new StrengthBar();
            if (isReversed)
            {
                // Ajuster la position de la barre de charge du joueur 2
                strengthBar.Start(120, 7, player.Color);
            }
            else
            {
                strengthBar.Start(10, 7, player.Color);
            }

            double playerForce = strengthBar.GetChargeLevel();
            var (playerHeadX, playerHeadY) = player.GetHeadPosition();
            projectile.Throw(playerForce, angle, playerHeadX, playerHeadY, isReversed, player1, player2, tower1, tower2);

            // Mise à jour du score et des points de vie
            hudP1.UpdateScore(player1.Score);
            hudP1.UpdateHP(player1.HpValue);
            hudP2.UpdateScore(player2.Score);
            hudP2.UpdateHP(player2.HpValue);
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