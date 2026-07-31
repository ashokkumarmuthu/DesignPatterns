using System;

namespace DesignPatterns.Creational.Singleton;

// Problem Solved: Thread-Safe Single-Instance Central Logger
public sealed class AppLogger
{
    private static readonly Lazy<AppLogger> _instance = new(() => new AppLogger());

    private AppLogger()
    {
        Console.WriteLine("  [AppLogger] Instance created (this should only appear ONCE).");
    }

    public static AppLogger Instance => _instance.Value;

    private int _logCount;

    public void Log(string message)
    {
        _logCount++;
        Console.WriteLine($"  [Log #{_logCount}] {message}");
    }
}

public class Program
{
    public static void RunDemo()
    {
        Console.WriteLine("=== SINGLETON PATTERN DEMO ===");
        Console.WriteLine("Problem Solved: Single shared log coordinator across the application\n");

        var logger1 = AppLogger.Instance;
        var logger2 = AppLogger.Instance;

        logger1.Log("Application started.");
        logger2.Log("User logged in.");

        Console.WriteLine($"\n  Same instance reference? {ReferenceEquals(logger1, logger2)}");
        Console.WriteLine("==============================\n");
    }
}
