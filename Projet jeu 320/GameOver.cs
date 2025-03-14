using System;

namespace Projet_320_Stone_Sling
{
    /// <summary>
    /// Classe pour gérer la fin du jeu
    /// </summary>
    internal class GameOver
    {
        /// <summary>
        /// Méthode pour afficher l'écran de fin de jeu
        /// </summary>
        /// <param name="winner"></param>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        public static void Show(Player winner, Player player1, Player player2)
        {
            Console.Clear();
            string gameOverText = "GAME OVER";
            string winnerText = $"Le joueur {winner.Number} a gagné!";
            string scoreTextP1 = $"Score du joueur 1: {player1.Score}";
            string scoreTextP2 = $"Score du joueur 2: {player2.Score}";

            // Positionner le texte "GAME OVER" au centre de l'écran
            int centerX = (Console.WindowWidth - gameOverText.Length) / 2;
            int centerY = Console.WindowHeight / 2;

            Console.SetCursorPosition(centerX, centerY);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(gameOverText);

            // Positionner le texte du gagnant en dessous
            int winnerTextX = (Console.WindowWidth - winnerText.Length) / 2;
            int winnerTextY = centerY + 2;

            Console.SetCursorPosition(winnerTextX, winnerTextY);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(winnerText);

            // Positionner les scores des joueurs en dessous
            int scoreTextP1X = (Console.WindowWidth - scoreTextP1.Length) / 2;
            int scoreTextP1Y = winnerTextY + 2;

            Console.SetCursorPosition(scoreTextP1X, scoreTextP1Y);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(scoreTextP1);

            int scoreTextP2X = (Console.WindowWidth - scoreTextP2.Length) / 2;
            int scoreTextP2Y = scoreTextP1Y + 1;

            Console.SetCursorPosition(scoreTextP2X, scoreTextP2Y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(scoreTextP2);

            Console.ResetColor();
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
        }
    }
}