using GuessingGameLibrary.GameDirectory;

namespace GuessingGameLibrary.GameFactory;

public enum GuessingGameType
{
    MasterMind
}

public interface IGuessingGameFactory<T>
{
    public IGuessingGame<T>? CreateGuessingGame(GuessingGameType gameType);
}