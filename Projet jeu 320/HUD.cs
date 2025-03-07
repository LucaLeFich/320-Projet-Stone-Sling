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

        

        public int NumeroJoueur { get; set; }

        public int Score { get; set; }

        public int HpValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string HP { get; set; }

        public ConsoleColor Color { get; set; }

        private const int barLength = 20;
        private bool charging = true;
        private int chargeLevel = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numeroJoueur"></param>
        /// <param name="score"></param>
        /// <param name="hp"></param>
        /// <param name="hpValue"></param>
        /// <param name="color"></param>
        public HUD(int numeroJoueur, int score, string hp, int hpValue, ConsoleColor color)
        {
            NumeroJoueur = numeroJoueur;
            Score = score;
            HP = hp;
            HpValue = hpValue;

            string scoreFormate = Score.ToString("D3");

            Hud = new string[]
            {
                "╔══════════════════════╗",
                $"║ Joueur {numeroJoueur}             ║",
                $"║ HP: {hp}            ║",
                $"║ Score: {scoreFormate}           ║",
                "╚══════════════════════╝"
            };
            Color = color;
        }
        public string UpdateHP(int hpValue)
        {
            if (hpValue == 2)
            {
                HP = "♥ ♥ x";
            }
            if (hpValue == 1)
            {
                HP = "♥ x x";
            }
            if (hpValue == 0)
            {
                HP = "x x x";
            }
            return HP;
        }

        /// <summary>
        /// Methode pour afficher le HUD
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Afficher(int x, int y)
        {
            Console.ForegroundColor = Color;

            for (int i = 0; i < Hud.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(Hud[i]);
            }
        }

    }
}