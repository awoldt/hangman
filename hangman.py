import random

phrases = ["apple"]
censoredPhrase = "" #word with all characters replaced with * (ex: cat = ***)
guesses = []

#index of the random phrase to be picked
index = random.randrange(0,len(phrases))
#phrase the user has to guess
randomPhrase = phrases[index]
#generates the censored str
for i in range(0, len(randomPhrase)):
    censoredPhrase += "*"

print("\n")

solved = False
#until user can guess the word
while(solved == False):
    print("Word - " + censoredPhrase)
    userGuess = input("Guess: ")

    
    
    


        

