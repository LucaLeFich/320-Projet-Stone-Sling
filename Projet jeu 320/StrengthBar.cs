///ETML
///Auteur : Luca Premat
///Date : 17.01.2025
///Description : Programme d'un jeu de combat entre deux joueurs inspiré du jeu "Stone Sling"
using System;
using System.Threading;

namespace Projet_320_Stone_Sling
{
    /// <summary>
    /// Classe pour la barre de force
    /// </summary>
    internal class StrengthBar
    {
        // Longueur de la barre de force
        private const int barLength = 20;

        // Indicateur si la barre de force est en train de charger
        private bool charging = true;

        // Niveau de charge initial de la barre de force
        private int chargeLevel = 1;

        // Indicateur si la barre de force est en cours d'exécution
        private bool isRunning = true;

        // Couleur de la barre de force
        public ConsoleColor Color { get; set; }

        // Bordure de la barre de force
        public string[] Border { get; set; }

        // Position X de la barre de force
        public int PosX { get; set; }

        // Position Y de la barre de force
        public int PosY { get; set; }

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

                // changer la valeur modifie la vitesse de la barre
                Thread.Sleep(40);
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
                // change la vitesse à laquelle le programme vérifie que l'espace soit pressé
                Thread.Sleep(10);
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