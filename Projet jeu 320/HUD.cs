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
        public string[] _HUD {  get; set; }

        public int NumeroJoueur { get; set; }

        public int Score { get; set; }

        public ConsoleColor Color { get; set; }

        public HUD(int numeroJoueur, int score)
        {
            NumeroJoueur = numeroJoueur;
            Score = score;

            string scoreFormate = Score.ToString("D3");

            _HUD = new string[]
            {
                "╔═════════════════════╗",
                $"║ Joueur {numeroJoueur}            ║",
                "║ HP: ♥ ♥ ♥           ║",
                $"║ Score: {scoreFormate}          ║",
                "╚═════════════════════╝",
                "╔═════════════════════╗",
                "║ ███████████████████ ║",
                "╚═════════════════════╝"
            };
        }

        public void Afficher(int x, int y)
        {
            for (int i = 0; i < _HUD.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(_HUD[i]);
            }
        }

    }
}
