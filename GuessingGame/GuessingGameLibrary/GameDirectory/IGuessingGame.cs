namespace GuessingGameLibrary.GameDirectory;

//generic to allow different types of objects to be used in game (string, int, list, array, etc)
public interface IGuessingGame<T>
{
    public T? GenerateAnswer();
    
    public bool EvaluateGuess(T guess);

    public string GenerateResponse(out bool success);

    public string GetIntroduction();
}