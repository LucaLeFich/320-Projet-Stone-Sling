using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Projet_320_Stone_Sling
{
    internal class Player
    {
        // Représentation graphique du joueur
        public string[] Representation { get; set; }

        // Position X du joueur
        private int posX;
        public int PosX
        {
            get { return posX; }
            set { posX = value; }
        }

        // Position Y du joueur
        private int posY;
        public int PosY
        {
            get { return posY; }
            set { posY = value; }
        }

        // Numéro du joueur
        public int Number { get; set; }

        // Score du joueur
        public int Score { get; set; }

        // Points de vie du joueur
        public int HpValue { get; set; }
        public string HP { get; set; }
        public ConsoleColor Color { get; set; }

        /// <summary>
        /// Constructeur pour le joueur
        /// </summary>
        /// <param name="number"></param>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        public Player(int number, int posX, int posY)
        {
            Number = number;
            Score = Score;
            HpValue = HpValue;
            HP = HP;

            Representation = new string[]
            {
                @" o",
                @"/░\",
                @"/ \"
            };
            Color = ConsoleColor.White; // Couleur par défaut
            number = 0; // Nom par défaut
            PosX = posX;
            PosY = posY;
        }

        /// <summary>
        /// Méthode pour afficher le joueur
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Draw(int x, int y)
        {
            Console.ForegroundColor = Color;

            for (int i = 0; i < Representation.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(Representation[i]);
            }
            Console.ResetColor(); // Réinitialise la couleur par défaut
        }

        /// <summary>
        /// Méthode pour obtenir la position de la tête du joueur
        /// </summary>
        /// <returns></returns>
        public (int, int) GetHeadPosition()
        {
            return (PosX, PosY);
        }

        /// <summary>
        /// Méthode pour vérifier la collision avec un projectile
        /// </summary>
        /// <param name="projX"></param>
        /// <param name="projY"></param>
        /// <returns></returns>
        public bool CheckCollision(int projX, int projY)
        {
            for (int i = 0; i < Representation.Length; i++)
            {
                if (projX == PosX && projY == PosY + i)
                {
                    return true;
                }
            }
            return false;
        }
    }
}