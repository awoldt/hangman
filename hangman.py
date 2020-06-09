import random

print("\n-You have 6 attempts to guess the word\n-Incorrect values will deduct attempts\n-Correct values will not decrease attempts")

phrases = ["apple"]
censoredPhrase = "" # word with all characters replaced with * (ex: cat = ***)
matchesTotal = 0
guesses = []
guessIndex = 0
guessesRemaining = 6
integer = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"]

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
    global guessesRemaining
    anyMatches = False
    matches = 0
    for i in randomPhrase:
        if(guesses[guessIndex] == i):
            anyMatches = True
            for x in randomPhrase:
                if(guesses[guessIndex] == x):
                    matchesTotal += 1
                    matches += 1
            if(matches == 1):
                print("\n>>There is 1 '" + userGuess + "'\n(" + str(guessesRemaining) + " guesses left)\n")
            else:
                print("\n>>There are " + str(matches) + " '" + userGuess + "'s\n(" + str(guessesRemaining) + " guesses left)\n")
            break
        else:
            anyMatches = False
    
    if(anyMatches == False):
        guessesRemaining -= 1
        print("\n>>There are no '" + userGuess + "'s\n(" + str(guessesRemaining) + " guesses left)\n")
        
# function appends user guess to guesses list
def appendGuess():
    global guessIndex
    global guessesRemaining
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
                guessesRemaining -= 1
                print("\n>>Already guessed '" + userGuess + "'\n(" + str(guessesRemaining) + " guesses left)\n")
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
    # game ends when user is out of guesses
    if(guessesRemaining == 0):
        print("\nno attempts remaining, the word was '" + randomPhrase + "'\n")
        exit()
    print("Word - " + censoredPhrase)
    userGuess = input("Guess: ")

    # if user enters an integer, resests loop
    if(userGuess in integer):
        print("\n>>>error: cannot use integers\n")
        continue
    elif(userGuess == " "):
        print("\n>>>error: cannot use spaces\n")
        continue
  
    if(len(userGuess) == 0):
        print("\n>>>>error: must enter value\n")
    elif(len(userGuess) > 1):
        print("\n>>>error: enter only 1 character\n")
    else:
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

print("Congrats!, the word was '" + randomPhrase + "'\n")
exit()