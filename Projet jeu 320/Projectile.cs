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

        public void Reset()
        {
            initialVelocity = 0;
            launchAngle = 0;
            currentTime = 0;
            currentX = 0;
            currentY = 0;
        }

        public void Throw(double force, double angle, int startX, int startY, bool isReversed, Player player1, Player player2, Towers tower1, Towers tower2, HUD hudP1, HUD hudP2)
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
                            player2.Score += 10; // Joueur 2 touche la tour adverse (gagne des points)
                        }
                        else
                        {
                            player1.Score -= 10; // Joueur 1 touche sa propre tour (perd des points)
                        }
                        tower1.DestroyStep();
                        return;
                    }
                    if (tower2.CheckCollision(x, y))
                    {
                        if (isReversed)
                        {
                            player2.Score -= 10; // Joueur 2 touche sa propre tour (perd des points)
                        }
                        else
                        {
                            player1.Score += 10; // Joueur 1 touche la tour adverse (gagne des points)
                        }
                        tower2.DestroyStep();
                        return;
                    }

                    // Vérifier les collisions avec les joueurs (ajouter une zone légèrement plus grande)
                    bool hitPlayer1 = (x >= player1.PosX - 1 && x <= player1.PosX + 1 && y >= player1.PosY && y <= player1.PosY + 2);
                    bool hitPlayer2 = (x >= player2.PosX - 1 && x <= player2.PosX + 1 && y >= player2.PosY && y <= player2.PosY + 2);

                    if (hitPlayer1)
                    {
                        if (isReversed)
                        {
                            player2.Score += 50; // Joueur 2 touche le joueur adverse (gagne des points)
                            player1.HpValue--;
                            hudP1.UpdateHP(player1.HpValue);
                            hudP1.Draw(hudP1.PosX, hudP1.PosY);
                        }
                        else
                        {
                            player1.Score -= 50; // Joueur 1 se touche lui-même (perd des points)
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
                            player2.Score -= 50; // Joueur 2 se touche lui-même (perd des points)
                            player2.HpValue--;
                            hudP2.UpdateHP(player2.HpValue);
                            hudP2.Draw(hudP2.PosX, hudP2.PosY);
                        }
                        else
                        {
                            player1.Score += 50; // Joueur 1 touche le joueur adverse (gagne des points)
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