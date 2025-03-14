///ETML
///Auteur : Luca Premat
///Date : 17.01.2025
///Description : Programme d'un jeu de combat entre deux joueurs inspiré du jeu "Stone Sling"
using System;
using System.Threading;

namespace Projet_320_Stone_Sling
{
    /// <summary>
    /// Classe pour les projectiles
    /// </summary>
    internal class Projectile
    {
        // Accélération due à la gravité
        const double gravity = 9.81;

        // Intervalle de temps pour la simulation
        const double timeInterval = 0.1;

        // Largeur et hauteur de la console
        const int consoleWidth = 150;
        const int consoleHeight = 40;

        // Multiplicateur de la force du tir
        const double forceMultiplier = 3.0;

        // Vitesse initiale du projectile et angle de lancement
        double initialVelocity;
        double launchAngle;

        // Temps actuel de la simulation et position actuelle du projectile
        double currentTime = 0;
        double currentX = 0;
        double currentY = 0;

        // Caractère représentant le projectile
        public char projectile { get; set; }

        // Couleur du projectile
        public ConsoleColor Color { get; set; }

        /// <summary>
        /// Constructeur pour le projectile
        /// </summary>
        /// <param name="color"></param>
        public Projectile(ConsoleColor color)
        {
            projectile = '●';
            Color = color;
        }

        /// <summary>
        /// Méthode pour dessiner le projectile à une position donnée
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Draw(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = Color;
            Console.Write(projectile);
            Console.ResetColor();
        }

        /// <summary>
        /// Méthode pour effacer le projectile à une position donnée
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Clear(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(' ');
        }

        /// <summary>
        /// Méthode pour réinitialiser les paramètres du projectile
        /// </summary>
        public void Reset()
        {
            initialVelocity = 0;
            launchAngle = 0;
            currentTime = 0;
            currentX = 0;
            currentY = 0;
        }

        /// <summary>
        /// Méthode pour lancer le projectile
        /// </summary>
        /// <param name="force"></param>
        /// <param name="angle"></param>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="isReversed"></param>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        /// <param name="tower1"></param>
        /// <param name="tower2"></param>
        /// <param name="hudP1"></param>
        /// <param name="hudP2"></param>
        public void Throw(double force, double angle, int startX, int startY, bool isReversed, Player player1, Player player2, Towers tower1, Towers tower2, HUD hudP1, HUD hudP2)
        {
            // Augmenter la force du tir
            initialVelocity = force * forceMultiplier;
            launchAngle = angle;

            double launchAngleRad = launchAngle * (Math.PI / 180);
            double initialVelocityX = initialVelocity * Math.Cos(launchAngleRad) * (isReversed ? -1 : 1);
            double initialVelocityY = initialVelocity * Math.Sin(launchAngleRad);

            int prevX = startX, prevY = startY;
            currentX = startX;
            currentY = startY;

            while (currentY < consoleHeight && currentX >= 0 && currentX < consoleWidth)
            {
                // Calcul de la nouvelle position
                currentTime += timeInterval;
                double nextX = startX + initialVelocityX * currentTime;
                double nextY = startY - (initialVelocityY * currentTime - 0.5 * gravity * Math.Pow(currentTime, 2));

                // Interpoler les positions entre prevX, prevY et nextX, nextY
                int steps = Math.Max(Math.Abs((int)(nextX - currentX)), Math.Abs((int)(nextY - currentY)));
                for (int step = 1; step <= steps; step++)
                {
                    double t = (double)step / steps;
                    int x = (int)(currentX + t * (nextX - currentX));
                    int y = (int)(currentY + t * (nextY - currentY));

                    x = Math.Max(0, Math.Min(consoleWidth - 1, x));
                    y = Math.Max(0, Math.Min(consoleHeight - 1, y));

                    // Vérifier les collisions avec les tours
                    if (tower1.CheckCollision(x, y))
                    {
                        if (isReversed)
                        {
                            // Joueur 2 touche la tour adverse (gagne des points)
                            player2.Score += 10;
                        }
                        else
                        {
                            // Joueur 1 touche sa propre tour (perd des points)
                            player1.Score -= 10;
                        }
                        tower1.DestroyStep();
                        return;
                    }
                    if (tower2.CheckCollision(x, y))
                    {
                        if (isReversed)
                        {
                            // Joueur 2 touche sa propre tour (perd des points)
                            player2.Score -= 10;
                        }
                        else
                        {
                            // Joueur 1 touche la tour adverse (gagne des points)
                            player1.Score += 10;
                        }
                        tower2.DestroyStep();
                        return;
                    }

                    // Vérifier les collisions avec les joueurs
                    bool hitPlayer1 = (x >= player1.PosX - 1 && x <= player1.PosX && y >= player1.PosY - 1 && y <= player1.PosY + 2);
                    bool hitPlayer2 = (x >= player2.PosX - 1 && x <= player2.PosX + 1 && y >= player2.PosY - 1 && y <= player2.PosY + 2);

                    if (hitPlayer1)
                    {
                        if (isReversed)
                        {
                            // Joueur 2 touche le joueur adverse (gagne des points)
                            player2.Score += 50;
                            player1.HpValue--;
                            hudP1.UpdateHP(player1.HpValue);
                            hudP1.Draw(hudP1.PosX, hudP1.PosY);
                        }
                        else
                        {
                            // Joueur 1 se touche lui-même (perd des points)
                            player1.Score -= 50;
                            player1.HpValue--;
                            hudP1.UpdateHP(player1.HpValue);
                            hudP1.Draw(hudP1.PosX, hudP1.PosY);
                        }
                        return;
                    }
                    if (hitPlayer2)
                    {
                        if (isReversed)
                        {
                            // Joueur 2 se touche lui-même (perd des points)
                            player2.Score -= 50;
                            player2.HpValue--;
                            hudP2.UpdateHP(player2.HpValue);
                            hudP2.Draw(hudP2.PosX, hudP2.PosY);
                        }
                        else
                        {
                            // Joueur 1 touche le joueur adverse (gagne des points)
                            player1.Score += 50;
                            player2.HpValue--;
                            hudP2.UpdateHP(player2.HpValue);
                            hudP2.Draw(hudP2.PosX, hudP2.PosY);
                        }
                        return;
                    }

                    // Effacer la position précédente du projectile
                    Clear(prevX, prevY);

                    // Dessiner le projectile à la nouvelle position
                    Draw(x, y);

                    prevX = x;
                    prevY = y;
                }

                currentX = nextX;
                currentY = nextY;

                Thread.Sleep(100);
            }

            // Effacer la dernière position du projectile
            Clear(prevX, prevY);
        }
    }
}