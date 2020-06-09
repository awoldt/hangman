"""
This program will open up file in directory called "words.txt"
Each word in file will be appened to phrases list
Phrases list is imported into hangman.py
"""

phrases = []
buildWord = ""

file = open("words.txt")
words = file.read()
for i in words:
    if(i == "\n"):
        phrases.append(buildWord)
        buildWord = ""
    else:
        buildWord += i
file.close()


