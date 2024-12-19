Guessing Game Description

Programming Exercise
Create a C# console application that is a simple version of Mastermind.  The randomly generated answer should be 
four (4) digits in length, with each digit between the numbers 1 and 6.  After the player enters a combination, 
a minus (-) sign should be printed for every digit that is correct but in the wrong position, 
and a plus (+) sign should be printed for every digit that is both correct and in the correct position. 
Print all plus signs first, all minus signs second, and nothing for incorrect digits. 
The player has ten (10) attempts to guess the number correctly before receiving a message that they have lost.

Example:
If the secret answer were: 1234
And the user guessed: 4233
The hint should be: ++--

//*******************
Design

Jetbrains Rider with .Net 8 was used to build project.

- ConsoleApp
    The console app entry point Main method contains the high level activities. 
    Dependency Injection was used with GameFactory to allow selection of game at runtime.
    
- GuessingGameLibrary
    -Folder GameDirectory contains list of games and IGuessingGame interface
        IGuessing game interface was used to allow creation of different types of games
        
    -Folder GameFactory contains factory for generating games
        GameFactory was created to allow creation and selection of different types of games implementing IGuessingGame interface.
        
    -Folder GameParameters
        Contains game parameter JSON files used to allow user to change parameters of MasterMind game and read when constructor is called.
        
- LoggingLibrary
    - Logging project added to capture errors.  Expansion is needed to allow sync to other sources (DB, txt file, etc)
    
- MasterMindTests
    - Example unit tests included but bot exhaustive




















