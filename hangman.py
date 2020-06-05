import random

phrases = ["cat"]
censoredPhrase = ""

#index of the random phrase to be picked
index = random.randrange(0,len(phrases))
#phrase the user has to guess
randomPhrase = phrases[index]

#generates the censored str
for i in range(0, len(randomPhrase)):
    censoredPhrase += "*"
print(censoredPhrase)

userStr = ""
index = 0
correctGuess = False
while(correctGuess == False):
    #until user can guess correct char
    loop = True
    while(loop == True):
        if(len(userStr) != len(randomPhrase)):
            print(userStr)
            userGuess = input("(guess): ") 
            if(userGuess != randomPhrase[index]):
                print("nope\n")
            else:
                print("correct\n")
                index += 1
                userStr += userGuess
        else:
            correctGuess = True
            loop = False


print("nice ni=bba")
exit()



