using GuessingGameLibrary.GameDirectory;
using GuessingGameLibrary.GameDirectory.MasterMind;

namespace GuessingGameLibrary.GameFactory;

public class GuessingGameFactory<T> :IGuessingGameFactory<T>
{
    public IGuessingGame<T>? CreateGuessingGame(GuessingGameType gameType)
    {
        return gameType switch
        {
            GuessingGameType.MasterMind => new Mastermind<T>(),
            _ => null
        };
    }
}