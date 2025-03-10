using Microsoft.SqlServer.Server;
using Projet_320_Stone_Sling;
using System;
using System.Media;
using System.Threading;

public class StrengthBar
{
    private const int barLength = 20;
    private bool charging = true;
    private int chargeLevel = 1;
    private bool isRunning = true;

    public ConsoleColor Color { get; set; }
    public string[] border { get; set; }

    public int PosX { get; set; }
    public int PosY { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="color"></param>
    public void Start(int posX, int posY,ConsoleColor color)
    {
        Console.ForegroundColor = color;
        border = new string[]
        {
            "╔══════════════════════╗",
            "║                      ║",
            "╚══════════════════════╝"
        };
        PosX = posX;
        PosY = posY;

        for (int i = 0; i < border.Length; i++)
        {
            Console.SetCursorPosition(10, 7 + i);
            Console.Write(border[i]);
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

        Console.WriteLine("\n\nFinal strength level: " + chargeLevel); //strength debug
    }

    /// <summary>
    /// Methode qui affiche la bar
    /// </summary>
    /// <param name="chargeLevel"></param>
    /// <param name="barLength"></param>
    private void DrawBar(int chargeLevel, int barLength)
    {


        Console.SetCursorPosition(12, 8);
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
    /// Methode qui vérifie en boucle si la touche espace soit pressée
    /// </summary>
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
}