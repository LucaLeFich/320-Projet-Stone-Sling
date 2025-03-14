///ETML
///Auteur : Luca Premat
///Date : 17.01.2025
///Description : Programme d'un jeu de combat entre deux joueurs inspiré du jeu "Stone Sling"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Projet_320_Stone_Sling
{
    /// <summary>
    /// Classe pour le HUD
    /// </summary>
    internal class HUD
    {
        // Représentation graphique du HUD
        public string[] Hud { get; set; }

        // Position X du HUD
        private int posX;
        public int PosX
        {
            get { return posX; }
            set { posX = value; }
        }

        // Position Y du HUD
        private int posY;
        public int PosY
        {
            get { return posY; }
            set { posY = value; }
        }

        // Informations du joueur
        public int PlayerID { get; set; }
        public int Score { get; set; }
        public int HpValue { get; set; }
        public string HP { get; set; }
        public ConsoleColor Color { get; set; }

        private const int barLength = 20;
        private bool charging = true;
        private int chargeLevel = 0;

        /// <summary>
        /// Constructeur pour le HUD
        /// </summary>
        /// <param name="playerID"></param>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="score"></param>
        /// <param name="hp"></param>
        /// <param name="hpValue"></param>
        /// <param name="color"></param>
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

        /// <summary>
        /// Méthode pour mettre à jour les points de vie
        /// </summary>
        /// <param name="hpValue"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Méthode pour mettre à jour le score
        /// </summary>
        /// <param name="score"></param>
        public void UpdateScore(int score)
        {
            Score = score;
            string formattedScore = Score.ToString("D3");
            Hud[3] = $"║ Score: {formattedScore}           ║";
            Draw(PosX, PosY);
        }

        /// <summary>
        /// Méthode pour afficher le HUD
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
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