using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Projet_320_Stone_Sling
{
    internal class Projectile
    {
        // Constantes
        const double gravity = 9.81; // Accélération due à la gravité en m/s²
        const double timeInterval = 0.1; // Intervalle de temps en secondes
        const int consoleWidth = 150; // Largeur de la console en caractères
        const int consoleHeight = 40; // Hauteur de la console en caractères

        // Variables
        double initialVelocity;
        double launchAngle;
        double currentTime = 0;
        double currentX = 0;
        double currentY = 0;

        /// <summary>
        /// 
        /// </summary>
        public char projectile {  get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ConsoleColor Color { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Color"></param>
        public Projectile(ConsoleColor Color)
        {
            projectile = '●';

            Color = this.Color;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Draw(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(projectile);
        }

        public void Throw()
        {
            // Conversion de l'angle en radians
            double launchAngleRad = launchAngle * (Math.PI / 180);

            // Composantes de la vitesse initiale
            double initialVelocityX = initialVelocity * Math.Cos(launchAngleRad);
            double initialVelocityY = initialVelocity * Math.Sin(launchAngleRad);

            // Simulation
            while (currentY >= 0)
            {
                // Effacement de la console
                Console.Clear();

                // Affichage de l'état actuel
                Console.WriteLine($"Temps: {currentTime:F2} s, Position: ({currentX:F2} m, {currentY:F2} m)");

                // Calcul de la position de la balle
                int x = (int)(currentX * 2) % consoleWidth; // Conversion de la position x (échelle 1m = 2 caractères)
                int y = consoleHeight - (int)(currentY * 2); // Conversion de la hauteur (échelle 1m = 2 caractères)

                // S'assurer que x et y sont dans les limites de la console
                x = Math.Max(0, Math.Min(consoleWidth - 1, x));
                y = Math.Max(0, Math.Min(consoleHeight - 1, y));

                // Affichage de la balle
                for (int i = 0; i < consoleHeight; i++)
                {
                    if (i == y)
                    {
                        try
                        {
                            Console.SetCursorPosition(x, i);
                            Console.Write('O');
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            Console.SetCursorPosition(0, consoleHeight - 1);
                            Console.WriteLine($"Erreur: {e.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }

                // Calcul de la nouvelle position
                currentTime += timeInterval;
                currentX = initialVelocityX * currentTime;
                currentY = initialVelocityY * currentTime - 0.5 * gravity * Math.Pow(currentTime, 2);

                // Pause pour visualisation
                Thread.Sleep(100);
            }
        }
    }
}
