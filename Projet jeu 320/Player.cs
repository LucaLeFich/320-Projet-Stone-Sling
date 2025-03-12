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
        public string[] Representation { get; set; }

        public int PosX { get; set; }
        public int PosY { get; set; }

        public int Number { get; set; }

        public int Score { get; set; }

        public int HpValue { get; set; }
        public string HP { get; set; }
        public ConsoleColor Color { get; set; }

        // Constructeur pour le joueur
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
            Color = ConsoleColor.White; //couleur par défaut
            number = 0; //Nom par défaut
            PosX = posX;
            PosY = posY;
        }

        /// <summary>
        /// Methode pour afficher le joueur
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
        public (int, int) GetHeadPosition()
        {
            return (PosX, PosY);
        }

        /// <summary>
        /// Méthode pour vérifier la collision avec un projectile
        /// </summary>
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