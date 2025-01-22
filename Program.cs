using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenu();
        }

        static void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("### Welcome to Hangman ###");
                Console.WriteLine("##########################");
                Console.WriteLine();

                Console.WriteLine("Bitte wähle eine Aktion aus:");

                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("[1] Spielen");
                Console.WriteLine("[2] Beenden");
                Console.ResetColor();
                Console.WriteLine();

                Console.Write("Aktion: ");

                if (int.TryParse(Console.ReadLine(), out int action))
                {
                    bool end = false;

                    switch (action)
                    {
                        case 1:
                            StartGame();
                            break;
                        case 2:
                            end = true;
                            break;
                        default:
                            Console.WriteLine("Ungültige Eingabe! Bitte erneut versuchen.");
                            break;
                    }

                    if (end)
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Ungültige Eingabe! Bitte gib eine Zahl ein.");
                }

                Console.Clear();
            }
        }

        static void StartGame()
        {
            string[] words = new string[]
            {
                "Lotus",
                "Personenkraftwagen",
                "Lastkraftwagen",
                "Flugzeug",
                "Flammkuchen",
                "Donut",
                "Schüssel",
                "Pistole",
                "Bundeswehr",
                "Kinderriegel",
                "Uhr",
                "Tasse",
                "Geld",
                "Kofferraum",
                "Handy",
                "Schlüssel",
                "Wasser",
                "Turnschuhe"
            };

            Random rnd = new Random();
            int index = rnd.Next(0, words.Length);
            string word = words[index].ToLower();
            GameLoop(word);
        }

        static void GameLoop(string word)
        {
            int live = 10;
            StringBuilder hiddenWord = new StringBuilder(new string('_', word.Length));

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Gesuchtes Wort: {hiddenWord}");
                Console.Write("Noch übrige Versuche: ");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(new string('X', live));
                Console.ResetColor();

                Console.Write("Buchstabe: ");
                char character;

                try
                {
                    character = Convert.ToChar(Console.ReadLine().ToLower());
                }
                catch
                {
                    Console.WriteLine("Ungültige Eingabe! Bitte gib nur einen Buchstaben ein.");
                    continue;
                }

                bool foundCharInWord = false;

                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == character)
                    {
                        hiddenWord[i] = character;
                        foundCharInWord = true;
                    }
                }

                if (foundCharInWord)
                {
                    if (hiddenWord.ToString() == word)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("GEWONNEN!!");
                        Console.WriteLine($"Das Word war: {word}");
                        Console.ReadKey();
                        Console.ResetColor();
                        break;
                    }
                }
                else
                {
                    if (live > 0)
                    {
                        live -= 1;
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("GAMEOVER!!");
                        Console.WriteLine($"Das Word war: {word}");
                        Console.ReadKey();
                        Console.ResetColor();
                        break;
                    }
                }
            }
        }
    }
}
