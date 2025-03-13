using System;

namespace Projet_320_Stone_Sling
{
    internal class Towers
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public string[][] DestructionPhases { get; set; }
        public int CurrentPhase { get; private set; }
        public bool IsDestroyed { get; private set; }

        public Towers(int posX, int posY)
        {
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
            CurrentPhase = 0;
            IsDestroyed = false;
        }

        public void Draw()
        {
            if (IsDestroyed) return;

            Console.ForegroundColor = ConsoleColor.Gray;

            for (int i = 0; i < DestructionPhases[CurrentPhase].Length; i++)
            {
                Console.SetCursorPosition(PosX, PosY + i);
                Console.Write(DestructionPhases[CurrentPhase][i]);
            }
            Console.ResetColor();
        }

        public void Clear()
        {
            for (int i = 0; i < DestructionPhases[CurrentPhase].Length; i++)
            {
                Console.SetCursorPosition(PosX, PosY + i);
                Console.Write(new string(' ', DestructionPhases[CurrentPhase][i].Length));
            }
        }

        public void DestroyStep()
        {
            if (IsDestroyed) return;

            Clear();
            CurrentPhase++;
            PosY++; // Déplacer la tour vers le bas après chaque étape
            if (CurrentPhase >= DestructionPhases.Length)
            {
                IsDestroyed = true;
                return;
            }
            Draw();
        }

        public bool CheckCollision(int projX, int projY)
        {
            for (int i = 0; i < DestructionPhases[CurrentPhase].Length; i++)
            {
                if (projX >= PosX && projX < PosX + DestructionPhases[CurrentPhase][i].Length && projY == PosY + i)
                {
                    return true;
                }
            }
            return false;
        }
    }
}