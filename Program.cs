using System.Text.Json;

Game newGame = new Game();
await newGame.Play();

public class Game
{
    public List<char> Guesses { get; set; } = new List<char>();

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

            Guesses.Clear();
            for (int i = 0; i < words[0].Length; i++)
            {
                Guesses.Add('_');
            }

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

            while (attempts != 0)
            {
                Console.WriteLine(string.Join("", Guesses));
                Console.Write("\nGuess word: ");

                var guess = Console.ReadLine().ToLower();
                Console.Write("\n\n");

                if (guess == "" || guess == null) continue;
                if (guess.Length != word.Length)
                {
                    Console.WriteLine("error: guess must be " + word.Length + " chars\n\n");
                    continue;
                }

                attempts--;

                // CORRECT GUESS
                if (guess == word)
                {
                    Console.WriteLine("\nCORRECT! The word was '" + word + "'");
                    Console.WriteLine("-----------------------------------------------");
                    Console.WriteLine("Attempts: " + (5 - attempts));

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
                    }
                    break;
                }
                else
                {
                    for (int i = 0; i < word.Length; i++)
                    {
                        if (guess[i] == word[i] && Guesses[i] != guess[i])
                        {
                            Guesses[i] = guess[i];
                        }
                    }
                }
            }

            // game over
            if (attempts == 0)
            {
                Console.WriteLine("\n-----------------------------------------------");
                Console.WriteLine("\nThe word was '" + word + "'");

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
                }
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
