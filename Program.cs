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
                int action = Convert.ToInt32(Console.ReadLine());

                bool end = false;

                switch (action)
                {
                    case 1:
                        StartGame();
                        break;
                    case 2:
                        end = true;
                        break;
                }

                if (end)
                {
                    break;
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
            string hiddenWord = "";

            for (int i = 0; i < word.Length; i++)
            {
                hiddenWord += "_";
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Gesuchtes Wort: {hiddenWord}");
                Console.Write("Noch übrige Versuche: ");

                for (int i = 0; i < live; i++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("X");
                    Console.ResetColor();
                }

                Console.WriteLine();
                Console.Write("Buchstabe: ");
                char character = Convert.ToChar(Console.ReadLine().ToLower());

                bool foundCharInWord = false;

                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == character)
                    {
                        foundCharInWord = true;
                        break;
                    }
                }

                string tempHiddenWord = hiddenWord;
                hiddenWord = "";

                if (foundCharInWord)
                {
                    for (int i = 0; i < word.Length; i++)
                    {
                        if (word[i] == character)
                        {
                            hiddenWord += character;
                        }
                        else if (tempHiddenWord[i] != '_')
                        {
                            hiddenWord += tempHiddenWord[i];
                        }
                        else
                        {
                            hiddenWord += '_';
                        }
                    }

                    if (hiddenWord == word)
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
                    hiddenWord = tempHiddenWord;

                    if (live > 0)
                    {
                        live -= 1;
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("GAMEOVER!!");
                        Console.ReadKey();
                        Console.ResetColor();
                        break;
                    }
                }
            }
        }
    }
}
