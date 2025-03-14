///ETML
///Auteur : Luca Premat
///Date : 17.01.2025
///Description : Programme d'un jeu de combat entre deux joueurs inspiré du jeu "Stone Sling"
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

        /// <summary>
        /// Méthode principale
        /// </summary>
        static void Main()
        {
            // Masquer le curseur de la console
            Console.CursorVisible = false;

            // Pour afficher "●"
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Taille de la console
            Console.WindowHeight = 40;
            Console.WindowWidth = 150;

            // Création des Objets
            player1 = new Player(1, 10, 37) { Color = ConsoleColor.Cyan }; // Joueur 1 en bleu
            player2 = new Player(2, 135, 37) { Color = ConsoleColor.Red }; // Joueur 2 en rouge
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
            tower1.Draw();
            tower2.Draw();
            hudP1.Draw(hudP1.PosX, hudP1.PosY);
            hudP2.Draw(hudP2.PosX, hudP2.PosY);

            bool isRunning = true;

            while (isRunning)
            {
                // Tour du joueur 1
                PlayTurn(player1, projectileJ1, aimPointsJ1, hudP1, hudP2, false);

                // Vérifier si le joueur 2 a perdu
                if (player2.HpValue <= 0)
                {
                    GameOver(player1);
                    break;
                }

                // Tour du joueur 2
                PlayTurn(player2, projectileJ2, aimPointsJ2, hudP1, hudP2, true);

                // Vérifier si le joueur 1 a perdu
                if (player1.HpValue <= 0)
                {
                    GameOver(player2);
                    break;
                }
            }
        }

        /// <summary>
        /// Méthode pour gérer le tour d'un joueur
        /// </summary>
        /// <param name="player"></param>
        /// <param name="projectile"></param>
        /// <param name="aimPoints"></param>
        /// <param name="hudP1"></param>
        /// <param name="hudP2"></param>
        /// <param name="isReversed"></param>
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
                strengthBar.Start(115, 7, player.Color);
            }
            else
            {
                strengthBar.Start(10, 7, player.Color);
            }

            double playerForce = strengthBar.GetChargeLevel();
            var (playerHeadX, playerHeadY) = player.GetHeadPosition();
            projectile.Throw(playerForce, angle, playerHeadX, playerHeadY, isReversed, player1, player2, tower1, tower2, hudP1, hudP2);

            // Mise à jour du score et des points de vie
            hudP1.UpdateScore(player1.Score);
            hudP1.UpdateHP(player1.HpValue);
            hudP2.UpdateScore(player2.Score);
            hudP2.UpdateHP(player2.HpValue);
        }

        /// <summary>
        /// Méthode pour calculer l'angle
        /// </summary>
        /// <param name="elapsedMilliseconds"></param>
        /// <returns></returns>
        static float CalulateAngle(long elapsedMilliseconds)
        {
            Random random = new Random();
            float blurFactor = (float)(random.NextDouble() * 10.0 - 5.0); // Zone floue entre -5 et +5 degrés

            // Convertir le temps écoulé en angle
            float baseAngle = 90 - ((float)(elapsedMilliseconds % 1600) / 1600 * 90); // Ajuster le temps écoulé pour un cycle complet de 2400ms
            float angle = baseAngle + blurFactor;
            return Clamp(angle, 0f, 90f); // Contraindre l'angle entre 0 et 90 degrés
        }

        /// <summary>
        /// Méthode pour contraindre une valeur entre un minimum et un maximum
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        static float Clamp(float value, float min, float max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        /// <summary>
        /// Méthode pour afficher l'écran de fin de jeu
        /// </summary>
        /// <param name="winner"></param>
        static void GameOver(Player winner)
        {
            Console.Clear();
            string gameOverText = "GAME OVER";
            string winnerText = $"Le joueur {winner.Number} a gagné!";
            string scoreTextP1 = $"Score du joueur 1: {player1.Score}";
            string scoreTextP2 = $"Score du joueur 2: {player2.Score}";

            // Positionner le texte "GAME OVER" au centre de l'écran
            int centerX = (Console.WindowWidth - gameOverText.Length) / 2;
            int centerY = Console.WindowHeight / 2;

            Console.SetCursorPosition(centerX, centerY);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(gameOverText);

            // Positionner le texte du gagnant en dessous
            int winnerTextX = (Console.WindowWidth - winnerText.Length) / 2;
            int winnerTextY = centerY + 2;

            Console.SetCursorPosition(winnerTextX, winnerTextY);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(winnerText);

            // Positionner les scores des joueurs en dessous
            int scoreTextP1X = (Console.WindowWidth - scoreTextP1.Length) / 2;
            int scoreTextP1Y = winnerTextY + 2;

            Console.SetCursorPosition(scoreTextP1X, scoreTextP1Y);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(scoreTextP1);

            int scoreTextP2X = (Console.WindowWidth - scoreTextP2.Length) / 2;
            int scoreTextP2Y = scoreTextP1Y + 1;

            Console.SetCursorPosition(scoreTextP2X, scoreTextP2Y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(scoreTextP2);

            Console.ResetColor();
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
        }
    }
}