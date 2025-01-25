HttpClient client = new HttpClient();
while (true)
{
    HangmanGame game = new HangmanGame(client);
    Difficulties SelectedDifficulty = game.ChooseDifficulty();
    string? WordToSpell = await game.GetWordToSpell(SelectedDifficulty);

    bool CorrectGuess = false;
    int Guesses = 0;

    if (WordToSpell != null)
    {
        while (Guesses < 5)
        {
            Console.WriteLine(game.HiddenGuess);
            Console.Write($"\n\n({5 - Guesses} tries remaining)\nGuess word: ");

            var UserAttempt = Console.ReadLine();
            if (UserAttempt == "")
            {
                Console.Clear();
                continue;
            }
            Guesses++;

            // CORRECT GUESS!
            if (UserAttempt != null && UserAttempt.ToLower() == WordToSpell.ToLower())
            {
                CorrectGuess = true;
                break;
            }

            game.NewUserAttempt(WordToSpell, UserAttempt);
        }
    }

    if (CorrectGuess)
    {
        Console.WriteLine($"\n\n🎉 Congrats, you gussed the word \"{WordToSpell}\" in {Guesses} {(Guesses == 1 ? "try" : "tries")}!");
    }
    else
    {
        Console.WriteLine($"The word was \"{WordToSpell}\"");
    }

    bool playAgain = game.PlayAgain();
    if (!playAgain) break;
}



