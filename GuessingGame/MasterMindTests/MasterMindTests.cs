using System.Text.Json;
using GuessingGameLibrary.GameDirectory;
using GuessingGameLibrary.GameDirectory.MasterMind;

namespace MasterMindTests;

//example unit test suite for one Mastermind method. More tests need to be created.
public class Tests
{
    private GameParameters GameParameters { get; set; }
    private IGuessingGame<string> mastermind;
    [SetUp]
    public void Setup()
    {
        string parameterFile = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "GameParameters/MasterMindGameParameters.json").ReadToEnd();
        GameParameters = JsonSerializer.Deserialize<GameParameters>(parameterFile);
        mastermind = new Mastermind<string>();
    }

    [Test]
    public void GenerateAnswerLengthTest()
    {
        string answer = mastermind.GenerateAnswer();

        if (answer.Length == GameParameters.GuessLength)
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();    
        }
    }
    
    [Test]
    public void GenerateAnswerMinTest()
    {
        string answer = mastermind.GenerateAnswer();

        foreach (char c in answer)
        {
            int value = int.Parse(c.ToString());
            if (value < GameParameters.MinValue)
            {
                Assert.Fail();
            }
        }
        
        Assert.Pass();
    }
    
    [Test]
    public void GenerateAnswerMaxTest()
    {
        string answer = mastermind.GenerateAnswer();

        foreach (char c in answer)
        {
            int value = int.Parse(c.ToString());
            if (value > GameParameters.MaxValue)
            {
                Assert.Fail();
            }
        }
        
        Assert.Pass();
    }
}