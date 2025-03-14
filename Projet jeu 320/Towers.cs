///ETML
///Auteur : Luca Premat
///Date : 17.01.2025
///Description : Programme d'un jeu de combat entre deux joueurs inspiré du jeu "Stone Sling"
using System;

namespace Projet_320_Stone_Sling
{
    /// <summary>
    /// Classe pour les tours
    /// </summary>
    internal class Towers
    {
        // Propriétés pour la position de la tour
        public int PosX { get; set; }
        public int PosY { get; set; }

        // Phases de destruction de la tour, phase actuelle et état de destruction
        public string[][] DestructionPhases { get; set; }
        public int CurrentPhase { get; private set; }
        public bool IsDestroyed { get; private set; }

        /// <summary>
        /// Constructeur des étapes des tours
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        public Towers(int posX, int posY)
        {
            // Initialisation des positions X et Y
            PosX = posX;
            PosY = posY;
            DestructionPhases = new string[][]
            {
                new string[] {
                    "████",
                    "████",
                    "████",
                    "████",
                    "████",
                    "████",
                    "████",
                    "████"
                },
                new string[] {
                    "██",
                    "███",
                    "████",
                    "████",
                    "████",
                    "████",
                    "████"
                },
                new string[] {
                    "   █",
                    "  ██",
                    "████",
                    "████",
                    "████",
                    "████"
                },
                new string[] {
                    "  █ ",
                    " ███",
                    "████",
                    "████",
                    "████"
                },
                new string[] {
                    " ██",
                    "████",
                    "████",
                    "████"
                },
                new string[] {
                    "  ██",
                    " ███",
                    "████"
                },
                new string[] {
                    "   █",
                    " ███"
                },
                new string[] {
                    "████"
                }
            };
            // Initialisation de la phase actuelle à 0
            CurrentPhase = 0;
            IsDestroyed = false;
        }

        /// <summary>
        /// Méthode pour dessiner la tour sur la console
        /// </summary>
        public void Draw()
        {
            // Ne rien faire si la tour est déjà détruite
            if (IsDestroyed) return;

            Console.ForegroundColor = ConsoleColor.Yellow;

            for (int i = 0; i < DestructionPhases[CurrentPhase].Length; i++)
            {
                Console.SetCursorPosition(PosX, PosY + i);
                Console.Write(DestructionPhases[CurrentPhase][i]);
            }
            Console.ResetColor();
        }

        /// <summary>
        /// Méthode pour effacer la tour de la console
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < DestructionPhases[CurrentPhase].Length; i++)
            {
                Console.SetCursorPosition(PosX, PosY + i);
                Console.Write(new string(' ', DestructionPhases[CurrentPhase][i].Length));
            }
        }

        /// <summary>
        /// Méthode pour détruire une étape de la tour
        /// </summary>
        public void DestroyStep()
        {
            // Ne rien faire si la tour est déjà détruite
            if (IsDestroyed) return;

            Clear();
            CurrentPhase++;
            PosY++;
            if (CurrentPhase >= DestructionPhases.Length)
            {
                IsDestroyed = true;
                return;
            }
            Draw();
        }

        /// <summary>
        /// Méthode pour vérifier la collision entre un projectile et la tour
        /// </summary>
        /// <param name="projX"></param>
        /// <param name="projY"></param>
        /// <returns></returns>
        public bool CheckCollision(int projX, int projY)
        {
            // Retourner false si la tour est complètement détruite
            if (CurrentPhase >= DestructionPhases.Length) return false;

            for (int i = 0; i < DestructionPhases[CurrentPhase].Length; i++)
            {
                if (projX >= PosX && projX < PosX + DestructionPhases[CurrentPhase][i].Length && projY == PosY + i)
                {
                    // Retourner true si le projectile entre en collision avec la tour
                    return true;
                }
            }
            // Retourner false si aucune collision n'est détectée
            return false;
        }
    }
}