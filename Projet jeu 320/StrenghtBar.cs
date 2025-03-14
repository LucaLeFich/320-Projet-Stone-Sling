using System;
using System.Threading;

namespace Projet_320_Stone_Sling
{
    internal class StrengthBar
    {
        private const int barLength = 20; // Longueur de la barre de force
        private bool charging = true; // Indicateur si la barre de force est en train de charger
        private int chargeLevel = 1; // Niveau de charge initial de la barre de force
        private bool isRunning = true; // Indicateur si la barre de force est en cours d'exécution

        public ConsoleColor Color { get; set; } // Couleur de la barre de force
        public string[] Border { get; set; } // Bordure de la barre de force

        public int PosX { get; set; } // Position X de la barre de force
        public int PosY { get; set; } // Position Y de la barre de force

        /// <summary>
        /// Méthode pour démarrer la barre de force
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="color"></param>
        public void Start(int posX, int posY, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Border = new string[]
            {
                "╔══════════════════════╗",
                "║                      ║",
                "╚══════════════════════╝"
            };
            PosX = posX;
            PosY = posY;

            for (int i = 0; i < Border.Length; i++)
            {
                Console.SetCursorPosition(PosX, PosY + i);
                Console.Write(Border[i]);
            }

            Thread inputThread = new Thread(CheckInput);
            inputThread.Start();

            while (isRunning)
            {
                if (charging)
                {
                    chargeLevel++;
                    if (chargeLevel >= barLength)
                    {
                        charging = false;
                    }
                }
                else
                {
                    chargeLevel--;
                    if (chargeLevel <= 1)
                    {
                        charging = true;
                    }
                }

                DrawBar(chargeLevel, barLength);
                Thread.Sleep(40); // changer la valeur modifie la vitesse de la barre
            }
        }

        /// <summary>
        /// Méthode pour dessiner la barre de force
        /// </summary>
        /// <param name="chargeLevel"></param>
        /// <param name="barLength"></param>
        private void DrawBar(int chargeLevel, int barLength)
        {
            Console.SetCursorPosition(PosX + 2, PosY + 1);
            for (int i = 0; i < barLength; i++)
            {
                if (i < chargeLevel)
                {
                    Console.Write("█");
                }
                else
                {
                    Console.Write(" ");
                }
            }
        }

        /// <summary>
        /// Méthode pour vérifier l'entrée de l'utilisateur
        /// </summary>
        private void CheckInput()
        {
            while (isRunning)
            {
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Spacebar)
                {
                    isRunning = false;
                }
                Thread.Sleep(10); // change la vitesse à laquelle le programme vérifie que l'espace soit pressé
            }
        }

        /// <summary>
        /// Méthode pour obtenir le niveau de charge actuel
        /// </summary>
        /// <returns></returns>
        public int GetChargeLevel()
        {
            return chargeLevel;
        }
    }
}