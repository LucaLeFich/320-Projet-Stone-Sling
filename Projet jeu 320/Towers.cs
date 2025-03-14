using System;

namespace Projet_320_Stone_Sling
{
    internal class Towers
    {
        // Propriétés pour la position de la tour
        public int PosX { get; set; } // Position X de la tour
        public int PosY { get; set; } // Position Y de la tour

        // Phases de destruction de la tour, phase actuelle et état de destruction
        public string[][] DestructionPhases { get; set; } // Tableaux des phases de destruction de la tour
        public int CurrentPhase { get; private set; } // Phase actuelle de la destruction
        public bool IsDestroyed { get; private set; } // Indicateur si la tour est détruite

        /// <summary>
        /// Constructeur des étapes des tours
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        public Towers(int posX, int posY)
        {
            PosX = posX; // Initialisation de la position X
            PosY = posY; // Initialisation de la position Y
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
            CurrentPhase = 0; // Initialisation de la phase actuelle à 0
            IsDestroyed = false; // Initialisation de l'état de destruction à false
        }

        /// <summary>
        /// Méthode pour dessiner la tour sur la console
        /// </summary>
        public void Draw()
        {
            if (IsDestroyed) return; // Ne rien faire si la tour est déjà détruite

            Console.ForegroundColor = ConsoleColor.Yellow; // Définir la couleur de la tour

            for (int i = 0; i < DestructionPhases[CurrentPhase].Length; i++)
            {
                Console.SetCursorPosition(PosX, PosY + i); // Définir la position du curseur pour dessiner la tour
                Console.Write(DestructionPhases[CurrentPhase][i]); // Dessiner la ligne actuelle de la phase de destruction
            }
            Console.ResetColor(); // Réinitialiser la couleur de la console
        }

        /// <summary>
        /// Méthode pour effacer la tour de la console
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < DestructionPhases[CurrentPhase].Length; i++)
            {
                Console.SetCursorPosition(PosX, PosY + i); // Définir la position du curseur pour effacer la tour
                Console.Write(new string(' ', DestructionPhases[CurrentPhase][i].Length)); // Effacer la ligne actuelle
            }
        }

        /// <summary>
        /// Méthode pour détruire une étape de la tour
        /// </summary>
        public void DestroyStep()
        {
            if (IsDestroyed) return; // Ne rien faire si la tour est déjà détruite

            Clear(); // Effacer la tour actuelle
            CurrentPhase++; // Passer à la phase suivante
            PosY++; // Déplacer la tour vers le bas après chaque étape
            if (CurrentPhase >= DestructionPhases.Length)
            {
                IsDestroyed = true; // Marquer la tour comme détruite si toutes les phases sont complétées
                return;
            }
            Draw(); // Dessiner la tour dans sa nouvelle phase de destruction
        }

        /// <summary>
        /// Méthode pour vérifier la collision entre un projectile et la tour
        /// </summary>
        /// <param name="projX"></param>
        /// <param name="projY"></param>
        /// <returns></returns>
        public bool CheckCollision(int projX, int projY)
        {
            if (CurrentPhase >= DestructionPhases.Length) return false; // Retourner false si la tour est complètement détruite

            for (int i = 0; i < DestructionPhases[CurrentPhase].Length; i++)
            {
                if (projX >= PosX && projX < PosX + DestructionPhases[CurrentPhase][i].Length && projY == PosY + i)
                {
                    return true; // Retourner true si le projectile entre en collision avec la tour
                }
            }
            return false; // Retourner false si aucune collision n'est détectée
        }
    }
}