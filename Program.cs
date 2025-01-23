using System.Text;

namespace Hangman
{
    class Program
    {
        static List<string> words = new List<string>
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
            "Turnschuhe",
            "Computer",
            "Programmierung",
            "Kopfhörer",
            "Maus",
            "Tastatur",
            "Monitor",
            "Drucker",
            "Scanner",
            "Tablet",
            "Smartphone"
        };

        static Random rnd = new Random();
        static int wordIndex = 0;
        static int lives = 10;

        static void Main(string[] args)
        {
            ShuffleWords();
            MainMenu();
        }

        static void ShuffleWords()
        {
            words = words.OrderBy(x => rnd.Next()).ToList();
        }

        static void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("### Welcome to Hangman ###");
                Console.WriteLine("##########################");
                Console.ResetColor();
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
                            SelectDifficulty();
                            StartGame();
                            break;
                        case 2:
                            end = true;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Ungültige Eingabe! Bitte erneut versuchen.");
                            Console.ResetColor();
                            break;
                    }

                    if (end)
                    {
                        break;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ungültige Eingabe! Bitte gib eine Zahl ein.");
                    Console.ResetColor();
                }

                Console.Clear();
            }
        }

        static void SelectDifficulty()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Bitte wähle eine Schwierigkeit aus:");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("[1] Einfach (15 Versuche)");
                Console.WriteLine("[2] Mittel (10 Versuche)");
                Console.WriteLine("[3] Schwer (5 Versuche)");
                Console.ResetColor();
                Console.Write("Schwierigkeit: ");

                if (int.TryParse(Console.ReadLine(), out int difficulty))
                {
                    switch (difficulty)
                    {
                        case 1:
                            lives = 15;
                            return;
                        case 2:
                            lives = 10;
                            return;
                        case 3:
                            lives = 5;
                            return;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Ungültige Eingabe! Bitte erneut versuchen.");
                            Console.ResetColor();
                            break;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ungültige Eingabe! Bitte gib eine Zahl ein.");
                    Console.ResetColor();
                }
            }
        }

        static void StartGame()
        {
            if (wordIndex >= words.Count)
            {
                ShuffleWords();
                wordIndex = 0;
            }

            string word = words[wordIndex].ToLower();
            wordIndex++;
            GameLoop(word);
        }

        static void GameLoop(string word)
        {
            int live = lives;
            StringBuilder hiddenWord = new StringBuilder(new string('_', word.Length));
            List<char> guessedChars = new List<char>();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("### Hangman ###");
                Console.WriteLine();
                Console.WriteLine($"Gesuchtes Wort: {hiddenWord}");
                Console.Write("Noch übrige Versuche: ");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(new string('X', live));
                Console.ResetColor();
                Console.WriteLine();

                if (guessedChars.Count > 0)
                {
                    Console.WriteLine($"Falsch erkannte Buchstaben: {string.Join(", ", guessedChars)}");
                }

                Console.Write("Buchstabe: ");
                char character;

                try
                {
                    character = Convert.ToChar(Console.ReadLine().ToLower());
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ungültige Eingabe! Bitte gib nur einen Buchstaben ein.");
                    Console.ResetColor();
                    continue;
                }

                if (guessedChars.Contains(character))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Dieser Buchstabe wurde bereits geraten.");
                    Console.ResetColor();
                    continue;
                }

                guessedChars.Add(character);

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
