using GuessingGameLibrary.GameDirectory;
using GuessingGameLibrary.GameDirectory.MasterMind;
using GuessingGameLibrary.GameFactory;
using Microsoft.Extensions.DependencyInjection;
using static LoggingLibrary.LogLibrary;

namespace ConsoleApp;

public static class GameActivities
{
    private static bool _continue = true;
    private static IGuessingGameFactory<string> GuessingGameFactory { get; set; }
    
    internal static void PopulateGameList()
    {
        //dependency injection
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IGuessingGameFactory<string>>(new GuessingGameFactory<string>())
            .AddSingleton<IGuessingGame<string>>(new Mastermind<string>())
            .BuildServiceProvider();

        GuessingGameFactory = serviceProvider.GetService<IGuessingGameFactory<string>>();
    }
    
    internal static IGuessingGame<string> SelectGame()
    {
        Console.WriteLine("Choose your game:");
        Console.WriteLine($"1. MasterMind");
        Console.WriteLine($"2. Color  (coming soon)");
        Console.WriteLine($"3. Car List (coming soon)");
        
        if (int.TryParse(Console.ReadLine(), out var gameSelection))
        {
            GuessingGameType guessingGameType =
                (GuessingGameType)Enum.ToObject(typeof(GuessingGameType), gameSelection - 1);

            return GuessingGameFactory.CreateGuessingGame(guessingGameType);
        }
        
        return null;
    }

    internal static void PlayGame(IGuessingGame<string> game)
    {
        try
        {
            Console.WriteLine(game.GetIntroduction());
            Console.WriteLine("Enter 'q' to exit.\n\r");
        
            game.GenerateAnswer();
        
            while (_continue)
            {
                Console.WriteLine("Enter your guess: ");
                var userGuess = Console.ReadLine();

                if (userGuess == "q")
                {
                    _continue = false;
                }
                else
                {
                    if (game.EvaluateGuess(userGuess))
                    {
                        var response = game.GenerateResponse(out var success);

                        if (success)
                        {
                            Console.WriteLine($"Correct Answer: {userGuess}");
                            Console.WriteLine($"Enter 'Y' to play again.");
                            if (Console.ReadLine() == "Y")
                            {
                                game.GenerateAnswer();
                            }
                            else
                            {
                                _continue = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Wrong Answer: {response}");
                        }
                    }    
                }
            }
        }
        catch (Exception e)
        {
            LogMessage(LogLevel.Error, e.Message);
        }
        
        Console.WriteLine("press any key to exit...");
        Console.ReadLine();
    }
}