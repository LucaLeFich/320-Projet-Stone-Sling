using System;
using System.Threading;

namespace Projet_320_Stone_Sling
{
    internal class StrengthBar
    {
        private const int barLength = 20;
        private bool charging = true;
        private int chargeLevel = 1;
        private bool isRunning = true;

        public ConsoleColor Color { get; set; }
        public string[] Border { get; set; }

        public int PosX { get; set; }
        public int PosY { get; set; }

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
                Thread.Sleep(40); // changer la valeure modifie la vitesse de la barre
            }
        }

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

        private void CheckInput()
        {
            while (isRunning)
            {
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Spacebar)
                {
                    isRunning = false;
                }
                Thread.Sleep(10); // change la vitesse a laquelle le program verifie que espace soit pressé
            }
        }

        public int GetChargeLevel()
        {
            return chargeLevel;
        }
    }
}