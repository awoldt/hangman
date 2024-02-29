using System.Text.Json;

Game newGame = new Game();
await newGame.Play();

public class Game
{
    public async Task<string?> GenerateWord(DifficultyLevel diff)
    {
        /* 
            Generates the word for user to spell

            random-word-api.herokuapp.com 🤘
         */

        HttpClient client = new HttpClient();
        string url = "";
        switch (diff)
        {
            case DifficultyLevel.Easy:
                {
                    url = "https://random-word-api.herokuapp.com/word?length=4";
                    break;
                }

            case DifficultyLevel.Medium:
                {
                    url = "https://random-word-api.herokuapp.com/word?length=7";
                    break;
                }

            case DifficultyLevel.Hard:
                {
                    url = "https://random-word-api.herokuapp.com/word?length=10";
                    break;
                }
        }

        try
        {
            var req = await client.GetAsync(url);
            req.EnsureSuccessStatusCode();
            var res = await req.Content.ReadAsStringAsync();
            var words = JsonSerializer.Deserialize<string[]>(res);
            var RETURN_WORD = words[0];

            return RETURN_WORD;
        }
        catch (Exception err)
        {
            Console.WriteLine(err);
            return null;
        }
    }

    public DifficultyLevel SelectDifficulty()
    {
        /* 
            Gives prompt to choose difficulty before each game
         */

        int diffIndex = 0; // defaults to easy
        string[] diff = ["Easy", "Medium", "Hard"];

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Choose difficulty:\n");
            for (int i = 0; i < 3; i++)
            {
                if (diffIndex == i)
                {
                    Console.WriteLine($"-> {diff[i]}");
                    continue;
                }
                Console.WriteLine($"{diff[i]}");
            }

            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.UpArrow)
            {
                // cant move up if easy
                if (diffIndex != 0)
                {
                    diffIndex--;
                }
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                // cant move down if hard
                if (diffIndex != 2)
                {
                    diffIndex++;
                }
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                return diffIndex == 0 ? DifficultyLevel.Easy : diffIndex == 1 ? DifficultyLevel.Medium : DifficultyLevel.Hard;
            }
        }
    }

    public async Task Play()
    {
        /* 
            Will start hangman game after difficulty is selected

            This will loop until a player chooses not to play again
         */

        bool GAMEOVER = false;

        while (!GAMEOVER)
        {
            var attempts = 5;
            var diff = SelectDifficulty();
            var word = await GenerateWord(diff);
            var guessesDisplay = new char[word.Length];
            List<char> guesses = new List<char>();
            // populates the default placeholder chars '_'
            // as user spells word, placeholders will be replaced with letters
            for (int i = 0; i < word.Length; i++)
            {
                guessesDisplay[i] = '_';
            }

            while (attempts != 0)
            {
                // GAME WON
                if (!guessesDisplay.Contains('_'))
                {
                    Console.WriteLine("\n\nCONGRATS, YOU GUESSED THE WORD!\nThe word was " + word);
                    // play again?
                    Console.Write("\nPlay again? (y/n) ");
                    var p = Console.ReadLine().ToLower();
                    while (p != "y" && p != "n")
                    {
                        Console.Write("Play again? (y/n) ");
                        p = Console.ReadLine().ToLower();
                    }
                    if (p == "n")
                    {
                        Console.Clear();
                        GAMEOVER = true;
                        break;
                    }
                }

                Console.WriteLine(string.Join("", guessesDisplay));
                Console.Write("\nGuess character: ");
                var guess = Console.ReadKey();
                Console.WriteLine("\n");

                if (!guesses.Contains(guess.KeyChar))
                {
                    guesses.Add(guess.KeyChar);
                    // character guessed is within word to guess
                    if (word.ToCharArray().Contains(guess.KeyChar))
                    {
                        for (int i = 0; i < word.Length; i++)
                        {
                            if (word[i] == guess.KeyChar)
                            {
                                guessesDisplay[i] = guess.KeyChar;
                            }
                        }
                    }
                    else
                    {
                        attempts--;
                    }
                }
                else
                {
                    Console.WriteLine("- already guessed the character '" + guess.KeyChar + "'\n");
                }
            }

            // game over, could not guess word
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine("\nThe word was '" + word + "'");

            // play again?
            Console.Write("\nPlay again? (y/n) ");
            var playAgain = Console.ReadLine().ToLower();
            while (playAgain != "y" && playAgain != "n")
            {
                Console.Write("Play again? (y/n) ");
                playAgain = Console.ReadLine().ToLower();
            }
            if (playAgain == "n")
            {
                Console.Clear();
                break;
            }
        }
    }
}

public enum DifficultyLevel
{
    Easy, // word has length of 4 characters
    Medium, // word has length of 7 characters
    Hard // word has length of 10 characters
}
