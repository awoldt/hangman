import random

phrases = ["apple", "blue", "corn", "valley"]
censoredPhrase = "" # word with all characters replaced with * (ex: cat = ***)
matchesTotal = 0
guesses = []
guessIndex = 0

# index of the random phrase to be picked
index = random.randrange(0,len(phrases))
# phrase the user has to guess
randomPhrase = phrases[index]
# generates the censored str
for i in range(0, len(randomPhrase)):
    censoredPhrase += "*"


# function that checks if user guessed char is in randomphrase
def checkGuess():
    global matchesTotal
    anyMatches = False
    for i in randomPhrase:
        if(guesses[guessIndex] == i):
            print("\n>>There is a " + userGuess + "\n")
            anyMatches = True
            for x in randomPhrase:
                if(guesses[guessIndex] == x):
                    matchesTotal += 1
            break
        else:
            anyMatches = False
    
    if(anyMatches == False):
        print("\n>>There are no '" + userGuess + "'s\n")


# function appends user guess to guesses list
def appendGuess():
    global guessIndex
    # first guess
    if(len(guesses) == 0):
        guesses.append(userGuess)
        checkGuess()
        guessIndex += 1
    # every guess after the first
    else:
        charBeenGuessed = False
        for i in guesses:
            # if user has already guessed this char
            if(userGuess == i):
                print("\n>>already guessed '" + userGuess + "'\n")
                charBeenGuessed = True
                break
            else:
                charBeenGuessed = False

        if(charBeenGuessed == False):
            guesses.append(userGuess)
            checkGuess()
            guessIndex += 1
                


print("\n")

solved = False
# until user can guess the word
while(solved == False):
    print("Word - " + censoredPhrase)
    userGuess = input("Guess: ")
    appendGuess()
 
    # inserts correctly guessed chars into censoredPhrase
    index = 0
    x = list(censoredPhrase)
    for i in randomPhrase:
        if(userGuess == i):
            x.pop(index)
            x.insert(index, userGuess)
            censoredPhrase = "".join(x)
            index += 1
        else:
            index += 1

    # if user has guessed all words correctly 
    if(matchesTotal == len(randomPhrase)):
        break

print("Congrats!, the word was '" + randomPhrase + "'")
exit()