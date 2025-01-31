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
        public string[] _HUD {  get; set; }

        public int _numeroJoueur { get; set; }

        public int _score { get; set; }

        public int _hpValue {  get; set; } 

        public string _hp {  get; set; }

        public ConsoleColor Color { get; set; }

        public HUD(int numeroJoueur, int score, string hp, int hpValue)
        {
            _numeroJoueur = numeroJoueur;
            _score = score;
            _hp = hp;
            _hpValue = hpValue;

            if (hpValue == 3)
            {
                hp = "♥ ♥ ♥";
            }
            else
            {
                if (hpValue == 2)
                {
                    hp = "♥ ♥";
                }
                else
                {
                    if (hpValue == 1)
                    {
                        hp = "♥";
                    }
                    else
                    {
                        if (hpValue == 0)
                        {
                            hp = "";
                        }
                    }
                }
            }

            string scoreFormate = _score.ToString("D3");

            _HUD = new string[]
            {
                "╔═════════════════════╗",
                $"║ Joueur {numeroJoueur}            ║",
                $"║ HP: {hp}           ║",
                $"║ Score: {scoreFormate}          ║",
                "╚═════════════════════╝",
                "╔═════════════════════╗",
                "║ ███████████████████ ║",
                "╚═════════════════════╝"
            };
            Color = ConsoleColor.White; //couleur par défaut
        }

        public void Afficher(int x, int y)
        {
            Console.ForegroundColor = Color;

            for (int i = 0; i < _HUD.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(_HUD[i]);
            }
        }

    }
}
