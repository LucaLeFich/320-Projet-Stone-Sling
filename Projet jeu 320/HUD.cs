using Projet_320_Stone_Sling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Projet_jeu_320
{
    internal class HUD
    {
        public string[] Hud { get; set; }

        public int NumeroJoueur { get; set; }

        public int Score { get; set; }

        public int HpValue { get; set; }

        public string HP { get; set; }

        public string StrenghtBar {  get; set; }

        public int StrenghtValue { get; set; }

        public ConsoleColor Color { get; set; }

        public HUD(int numeroJoueur, int score, string hp, int hpValue)
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
                "╚══════════════════════╝",
                "╔══════════════════════╗",
               $"║ {StrenghtBar}        ║",
                "╚══════════════════════╝"
            };
            Color = ConsoleColor.White; //couleur par défaut
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

        public string UpdateBar(int strenghtValue)
        {
            if(strenghtValue == 0)
            {
                StrenghtBar = "█                   ";
            }
            return StrenghtBar;
        }

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