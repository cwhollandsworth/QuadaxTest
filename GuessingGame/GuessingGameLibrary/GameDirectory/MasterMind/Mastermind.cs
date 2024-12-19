using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static LoggingLibrary.LogLibrary;

namespace GuessingGameLibrary.GameDirectory.MasterMind;

public class Mastermind<T> : IGuessingGame<T>
{
    private GameParameters GameParameters { get; set; }
    private T? Answer { get; set; } 
    private int CorrectDigitWrongPosition { get; set; } = 0;
    private int CorrectDigitCorrectPosition { get; set; } = 0;

    
    public Mastermind()
    {
        try
        {
            string parameterFile = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "GameParameters/MasterMindGameParameters.json").ReadToEnd();
            GameParameters = JsonSerializer.Deserialize<GameParameters>(parameterFile);
        }
        catch (JsonException e)
        {
            LogMessage(LogLevel.Error, e.Message);
        }
        catch (Exception e)
        {
            LogMessage(LogLevel.Error, e.Message);
        }
    }

    public string GetIntroduction()
    {
        StringBuilder builder = new StringBuilder();
        
        builder.AppendLine("Welcome to the Mastermind guessing game!\n\r");
        builder.AppendLine($"Guess which {GameParameters.GuessLength} numbers we chose. ");
        builder.AppendLine($"Enter {GameParameters.GuessLength} numbers in a row with each digit between {GameParameters.MinValue} and {GameParameters.MaxValue} (inclusive)");
        builder.AppendLine("Correct answers in the correct location will be indicated by a '+'");
        builder.AppendLine("Correct answers in the wrong location will be indicated by a '-'");
        builder.AppendLine("Though we won't tell you which locations are correct, only which values - good luck!");
        
        return builder.ToString();
    }
    
    public T? GenerateAnswer()
    {
        try
        {
            if (typeof(T) == typeof(string))
            {
                var random = new Random();
                var answer = string.Empty;

                for (var count = 0; count < GameParameters?.GuessLength; count++)
                {
                    var number = random.Next(GameParameters.MinValue, GameParameters.MaxValue);
                    answer += number.ToString();
                }

                Answer = (T)Convert.ChangeType(answer, typeof(T));
                Console.WriteLine($"Since this is a test I will give you a hint - {Answer.ToString()}");
            }

            return Answer;
        }
        catch (Exception e)
        {
            LogMessage(LogLevel.Error, e.Message);
            return (T) Convert.ChangeType(string.Empty, typeof(T));;
        }    
    }

    public bool EvaluateGuess(T userGuess)
    {
        try
        {
            if (typeof(T) == typeof(string) && userGuess != null && Answer != null)
            {
                try
                {
                    int position = 0;
                    CorrectDigitCorrectPosition = 0;
                    CorrectDigitWrongPosition = 0;
                    string? answer = Answer.ToString();
                    string? guess = userGuess.ToString();
                    
                    foreach (char c in guess)
                    {
                        if(answer.Contains(c))
                        {
                            if (answer[position].Equals(c))
                            {
                                CorrectDigitCorrectPosition++;
                            }
                            else
                            {
                                CorrectDigitWrongPosition++;
                            }
                        }
                        position++;
                    }
                }
                catch (Exception e)
                {
                    LogMessage(LogLevel.Error, e.Message);
                    return false;
                }

                return true;
            }
        }
        catch(Exception e)
        {
            LogMessage(LogLevel.Error, e.Message);
        }
        return false;
    }

    public string GenerateResponse(out bool success)
    {
        success = false;
        StringBuilder response = new StringBuilder();

        response.Append('+', CorrectDigitCorrectPosition);
        response.Append('-', CorrectDigitWrongPosition);
        
        success = (CorrectDigitCorrectPosition == Answer.ToString().Length);
        
        return response.ToString();
    }
}

public class GameParameters()
{
    [JsonInclude]
    public int GuessLength { get; set; }
    [JsonInclude]
    public int MinValue { get; set; }
    [JsonInclude]
    public int MaxValue { get; set; }
}