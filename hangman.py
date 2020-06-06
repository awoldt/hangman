import random

phrases = ["apple", "cat", "blue", "red", "dolphin", "bacon", "steak", "sky", "farm", "pencil", "law"]
censoredPhrase = "" # word with all characters replaced with * (ex: cat = ***)
matches = 0 # number of matches users guessed character has with ramdomPhrase string

# index of the random phrase to be picked
index = random.randrange(0,len(phrases))
# phrase the user has to guess
randomPhrase = phrases[index]
# generates the censored str
for i in range(0, len(randomPhrase)):
    censoredPhrase += "*"

print("\n")

solved = False
# until user can guess the word
while(solved == False):
    print("Word - " + censoredPhrase)
    userGuess = input("Guess: ")

    #tallys the number of matches user guessed char has in phrase
    for i in randomPhrase:
        if(userGuess == i):
            matches += 1
    if(matches == 0):
        print("\n>>There are no " + "'" + userGuess + "'" + "s\n")
    elif(matches == 1):
        print("\n>>There is 1 " + "'" + userGuess + "'" + "\n")
    else:
        print("\n>>There are " + str(matches) + " " + "'" + userGuess + "'" + "s\n")

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
    if(matches == len(randomPhrase)):
        break

print("Congrats!, the word was '" + randomPhrase + "'")
exit()
    

