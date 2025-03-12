using System;
using System.Threading;

namespace Projet_320_Stone_Sling
{
    internal class Projectile
    {
        const double gravity = 9.81;
        const double timeInterval = 0.1;
        const int consoleWidth = 150;
        const int consoleHeight = 40;
        const double forceMultiplier = 3.0; // Facteur de multiplication pour augmenter la force du tir

        double initialVelocity;
        double launchAngle;
        double currentTime;
        double currentX;
        double currentY;

        public char projectile { get; set; }
        public ConsoleColor Color { get; set; }

        public Projectile(ConsoleColor color)
        {
            projectile = '●';
            Color = color;
            Reset();
        }

        public void Draw(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = Color;
            Console.Write(projectile);
            Console.ResetColor();
        }

        public void Clear(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(' ');
        }

        public void Throw(double force, double angle, int startX, int startY, bool isReversed = false,
            Player player1 = null, Player player2 = null, Towers tower1 = null, Towers tower2 = null)
        {
            initialVelocity = force * forceMultiplier; // Augmenter la force du tir
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
                currentX = startX + initialVelocityX * currentTime;
                currentY = startY - (initialVelocityY * currentTime - 0.5 * gravity * Math.Pow(currentTime, 2));

                int x = (int)currentX;
                int y = (int)currentY;

                x = Math.Max(0, Math.Min(consoleWidth - 1, x));
                y = Math.Max(0, Math.Min(consoleHeight - 1, y));

                // Effacer la position précédente du projectile
                Clear(prevX, prevY);

                // Dessiner le projectile à la nouvelle position
                Draw(x, y);

                // Vérifier les collisions
                if (CheckCollisions(x, y, player1, player2, tower1, tower2, isReversed))
                {
                    Clear(x, y);
                    break;
                }

                prevX = x;
                prevY = y;

                Thread.Sleep(100);
            }

            // Effacer la dernière position du projectile
            Clear(prevX, prevY);
        }

        // Méthode pour vérifier les collisions
        private bool CheckCollisions(int projX, int projY, Player player1, Player player2, Towers tower1, Towers tower2, bool isReversed)
        {
            if (isReversed)
            {
                if (player2.CheckCollision(projX, projY))
                {
                    player2.HpValue--;
                    player2.Score -= 1;
                    return true;
                }
                if (player1.CheckCollision(projX, projY))
                {
                    player1.HpValue--;
                    player2.Score += 50;
                    return true;
                }
                if (tower2.CheckCollision(projX, projY))
                {
                    player2.Score -= 10;
                    return true;
                }
                if (tower1.CheckCollision(projX, projY))
                {
                    player2.Score += 10;
                    return true;
                }
            }
            else
            {
                if (player1.CheckCollision(projX, projY))
                {
                    player1.HpValue--;
                    player1.Score -= 1;
                    return true;
                }
                if (player2.CheckCollision(projX, projY))
                {
                    player2.HpValue--;
                    player1.Score += 50;
                    return true;
                }
                if (tower1.CheckCollision(projX, projY))
                {
                    player1.Score -= 10;
                    return true;
                }
                if (tower2.CheckCollision(projX, projY))
                {
                    player1.Score += 10;
                    return true;
                }
            }
            return false;
        }

        // Méthode pour réinitialiser les paramètres du projectile
        public void Reset()
        {
            initialVelocity = 0;
            launchAngle = 0;
            currentTime = 0;
            currentX = 0;
            currentY = 0;
        }
    }
}