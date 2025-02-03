using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Projet_320_Stone_Sling
{
    internal class Joueur
    {
        public string[] Representation { get; set; }

        public int Number { get; set; }

        public int Score { get; set; }

        public int HpValue { get; set; }
        public string HP { get; set; }

        public ConsoleColor Color { get; set; }

        // Constructeur par défaut (représentation générique)
        public Joueur(int number)
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
        }

        public void Afficher(int x, int y)
        {
            Console.ForegroundColor = Color;

            for (int i = 0; i < Representation.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(Representation[i]);
            }
            Console.ResetColor(); // Réinitialise la couleur par défaut
        }
    }
}
