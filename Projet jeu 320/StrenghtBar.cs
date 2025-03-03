using Microsoft.SqlServer.Server;
using System;
using System.Threading;

public class StrengthBar
{
    private const int barLength = 20;
    private bool charging = true;
    private int chargeLevel = 1;
    private bool isRunning = true;

    public string[] border {  get; set; } 

    public void Start()
    {
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
            Thread.Sleep(40); // Adjust the sleep time to change the speed of charging/discharging
        }

        Console.WriteLine("\nFinal strength level: " + chargeLevel);
    }

    private void DrawBar(int chargeLevel, int barLength)
    {
        border = new string[]
        {
            "╔══════════════════════╗",
            "║                      ║",
            "╚══════════════════════╝" 
        };

        for (int i = 0; i < border.Length; i++)
        {
            Console.SetCursorPosition(10, 7 + i);
            Console.Write(border[i]);
        }

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

    private void CheckInput()
    {
        while (isRunning)
        {
            if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Spacebar)
            {
                isRunning = false;
            }
            Thread.Sleep(10); // Adjust the sleep time to change the speed of checking for key press
        }
    }
}