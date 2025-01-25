using System.Net.Http.Json;

public enum Difficulties
{
    Easy,
    Medium,
    Hard,
    Expert
}

public class HangmanGame(HttpClient http)
{
    private readonly HttpClient Http = http;
    private static readonly string RandomWordApiURL = "https://random-word-api.herokuapp.com/word";
    public string HiddenGuess { get; set; }

    public Difficulties ChooseDifficulty()
    {
        int PointerPosition = 0;
        var DifficultyList = Enum.GetValues<Difficulties>();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("\nChoose your difficulty:\n\n");

            for (int i = 0; i < DifficultyList.Length; i++)
            {
                Console.WriteLine(PointerPosition == i ? $"> {DifficultyList[i]}" : DifficultyList[i]);
            }

            ConsoleKeyInfo KeyPressed = Console.ReadKey();

            switch (KeyPressed.Key)
            {
                case ConsoleKey.DownArrow:
                    {
                        if (PointerPosition != DifficultyList.Length - 1) PointerPosition++;
                        break;
                    }

                case ConsoleKey.UpArrow:
                    {
                        if (PointerPosition != 0) PointerPosition--;
                        break;
                    }

                case ConsoleKey.Enter:
                    {
                        return DifficultyList[PointerPosition];
                    }
            }
        }
    }

    public async Task<string?> GetWordToSpell(Difficulties Difficulty)
    {
        try
        {
            int WordLength = Difficulty == Difficulties.Easy ? 3 : Difficulty == Difficulties.Medium ? 5 : Difficulty == Difficulties.Hard ? 7 : Difficulty == Difficulties.Expert ? 9 : 0;
            if (WordLength == 0)
            {
                Console.Clear();
                Console.WriteLine("Error --> could not determine word length based on difficulty selected");
                return null;
            }

            string[]? res = await Http.GetFromJsonAsync<string[]>($"{RandomWordApiURL}?length={WordLength}");
            if (res == null)
            {
                Console.Clear();
                Console.WriteLine("Error --> there was an error while fetching word to spell");
                return null;
            }
            Console.Clear();

            // generate the hidden guess to display to the user
            // cat -> "___"
            string str = "";
            for (int i = 0; i < res[0].Length; i++)
            {
                str += "_";
            }
            HiddenGuess = str;

            return res[0];
        }
        catch (Exception error)
        {
            Console.WriteLine(error);
            return null;
        }
    }

    public void NewUserAttempt(string WordToSpell, string? Attempt)
    {
        if (Attempt != null)
        {
            WordToSpell = WordToSpell.ToLower();
            Attempt = Attempt.ToLower();

            // this function will simply update the HiddenGuess of this game
            char[] CorrectChars = WordToSpell.ToCharArray();
            char[] AttemptedChars = Attempt.ToCharArray();

            string str = "";

            // only need to go length of CorrectChars
            // CorrectChars and HiddenGuess should be the same length
            for (int i = 0; i < CorrectChars.Length; i++)
            {
                if (HiddenGuess[i] != '_')
                {
                    str += HiddenGuess[i]; // this user has already guessed this char correctly
                    continue;
                }

                try
                {
                    if (CorrectChars[i] == AttemptedChars[i])
                    {
                        str += CorrectChars[i];
                    }
                    else
                    {
                        str += "_";
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    if (HiddenGuess[i] != '_')
                    {
                        str += HiddenGuess[i]; // this user has already guessed this char correctly
                        continue;
                    }

                    str += "_";
                    continue;
                }

            }

            HiddenGuess = str;
            Console.Clear();
        }

    }

    public bool PlayAgain()
    {
        char[] ValidKeys = ['y', 'Y', 'n', 'N'];

        while (true)
        {
            Console.Write("\n\nPlay again? (y/n): ");
            ConsoleKeyInfo KeyPressed = Console.ReadKey();

            if (!ValidKeys.Contains(KeyPressed.KeyChar))
            {
                Console.Clear();
                continue;
            }
            if (KeyPressed.KeyChar == 'n' || KeyPressed.KeyChar == 'N')
            {
                Console.Clear();
                return false;

            }
            return true;
        }
    }
}