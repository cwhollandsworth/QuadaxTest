namespace LoggingLibrary;

public class LogLibrary
{
    public enum LogLevel
    {
        Info,
        Warning,
        Error
    }
    
    //eventually can update to use Microsoft logger, Serilog or other means to output logs to other locations
    public static void LogMessage(LogLevel loglevel, string message)
    {
        Console.WriteLine($"[{loglevel}] {message}");
    }
}