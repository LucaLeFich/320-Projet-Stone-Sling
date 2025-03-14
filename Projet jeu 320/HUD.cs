using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Projet_320_Stone_Sling
{
    internal class HUD
    {
        public string[] Hud { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }

        public int PlayerID { get; set; }
        public int Score { get; set; }
        public int HpValue { get; set; }
        public string HP { get; set; }
        public ConsoleColor Color { get; set; }

        private const int barLength = 20;
        private bool charging = true;
        private int chargeLevel = 0;

        public HUD(int playerID, int posX, int posY, int score, string hp, int hpValue, ConsoleColor color)
        {
            PlayerID = playerID;
            Score = score;
            HP = hp;
            HpValue = hpValue;

            string formattedScore = Score.ToString("D3");

            Hud = new string[]
            {
                "╔══════════════════════╗",
                $"║ Joueur {playerID}             ║",
                $"║ HP: {hp}            ║",
                $"║ Score: {formattedScore}           ║",
                "╚══════════════════════╝"
            };
            Color = color;
            PosX = posX;
            PosY = posY;
        }

        public string UpdateHP(int hpValue)
        {
            HpValue = hpValue;
            if (hpValue == 2)
            {
                HP = "♥ ♥ x";
            }
            else if (hpValue == 1)
            {
                HP = "♥ x x";
            }
            else if (hpValue == 0)
            {
                HP = "x x x";
            }
            Hud[2] = $"║ HP: {HP}            ║";
            Draw(PosX, PosY);
            return HP;
        }

        public void UpdateScore(int score)
        {
            Score = score;
            string formattedScore = Score.ToString("D3");
            Hud[3] = $"║ Score: {formattedScore}           ║";
            Draw(PosX, PosY);
        }

        public void Draw(int x, int y)
        {
            Console.ForegroundColor = Color;

            for (int i = 0; i < Hud.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(Hud[i]);
            }
            Console.ResetColor(); // Réinitialise la couleur par défaut
        }
    }
}