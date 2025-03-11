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
        double currentTime = 0;
        double currentX = 0;
        double currentY = 0;

        public char projectile { get; set; }
        public ConsoleColor Color { get; set; }

        public Projectile(ConsoleColor color)
        {
            projectile = '●';
            Color = color;
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

        public void Throw(double force, double angle, int startX, int startY, bool isReversed = false)
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

                prevX = x;
                prevY = y;

                Thread.Sleep(100);
            }

            // Effacer la dernière position du projectile
            Clear(prevX, prevY);
        }
    }
}