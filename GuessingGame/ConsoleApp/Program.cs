//***************************************************
//  Guessing Game Console Application
//  
//***************************************************

using static ConsoleApp.GameActivities;
using static LoggingLibrary.LogLibrary;

namespace ConsoleApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                PopulateGameList();

                var selectedGame = SelectGame();

                PlayGame(selectedGame);
            }
            catch (Exception e)
            {
                LogMessage(LogLevel.Error, e.Message);
            }
        }
    }
}