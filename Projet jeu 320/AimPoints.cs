///ETML
///Auteur : Luca Premat
///Date : 17.01.2025
///Description : Programme d'un jeu de combat entre deux joueurs inspiré du jeu "Stone Sling"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_320_Stone_Sling
{
    /// <summary>
    /// Classe pour les points de visée
    /// </summary>
    internal class AimPoints
    {
        // Représentation graphique des points de visée
        public string[] aimPoints { get; set; }

        // Position X des points de visée
        private int posX;
        public int PosX
        {
            get { return posX; }
            set { posX = value; }
        }

        // Position Y des points de visée
        private int posY;
        public int PosY
        {
            get { return posY; }
            set { posY = value; }
        }

        public ConsoleColor Color { get; set; }

        /// <summary>
        /// Constructeur pour les points de visée
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="isMirrored"></param>
        public AimPoints(int posX, int posY, bool isMirrored = false)
        {
            if (isMirrored)
            {
                aimPoints = new string[]
                {
                    @"   ●",
                    @"  ●",
                    @" ●",
                    @"●"
                };
            }
            else
            {
                aimPoints = new string[]
                {
                    @"●",
                    @" ●",
                    @"  ●",
                    @"   ●"
                };
            }
            Color = ConsoleColor.Green;
            PosX = posX;
            PosY = posY;
        }

        /// <summary>
        /// Méthode pour afficher les points de visée
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="activeIndex"></param>
        public void Draw(int x, int y, int activeIndex)
        {
            Console.SetCursorPosition(x, y + activeIndex);
            Console.ForegroundColor = Color;
            Console.Write(aimPoints[activeIndex]);
        }

        /// <summary>
        /// Méthode qui efface les points de visée pour n'en afficher qu'un à la fois
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < aimPoints.Length; i++)
            {
                Console.SetCursorPosition(PosX, PosY + i);
                Console.Write(new string(' ', aimPoints[i].Length));
            }
        }
    }
}