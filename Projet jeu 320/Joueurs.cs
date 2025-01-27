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
        public string[] _representation { get; set; }

        public int _number { get; set; }

        public int _score { get; set; }

        public ConsoleColor Color { get; set; }

        // Constructeur par défaut (représentation générique)
        public Joueur(int number)
        {
            _number = number;
            _score = _score;

            _representation = new string[]
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

            for (int i = 0; i < _representation.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(_representation[i]);
            }
            Console.ResetColor(); // Réinitialise la couleur par défaut
        }
    }
}
