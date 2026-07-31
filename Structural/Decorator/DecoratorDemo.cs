using System;
using System.Diagnostics;

namespace DesignPatterns.Structural.Decorator;

// Problem Solved: Stackable Cross-Cutting Concerns (Encryption, Logging, Timing) Without Subclass Explosion
public interface IDataReader
{
    string Read();
}

public class FileDataReader : IDataReader
{
    private readonly string _filename;
    public FileDataReader(string filename) { _filename = filename; }

    public string Read() => $"[Raw payload from {_filename}]";
}

public abstract class DataReaderDecorator : IDataReader
{
    protected readonly IDataReader _inner;
    protected DataReaderDecorator(IDataReader inner) { _inner = inner; }

    public virtual string Read() => _inner.Read();
}

public class EncryptionDecorator : DataReaderDecorator
{
    public EncryptionDecorator(IDataReader inner) : base(inner) { }

    public override string Read()
    {
        string data = _inner.Read();
        return $"🔓Decrypted({data})";
    }
}

public class LoggingDecorator : DataReaderDecorator
{
    public LoggingDecorator(IDataReader inner) : base(inner) { }

    public override string Read()
    {
        Console.WriteLine("  [LOG] Read operation started...");
        string data = _inner.Read();
        Console.WriteLine($"  [LOG] Read operation completed.");
        return data;
    }
}

public class TimingDecorator : DataReaderDecorator
{
    public TimingDecorator(IDataReader inner) : base(inner) { }

    public override string Read()
    {
        var sw = Stopwatch.StartNew();
        string data = _inner.Read();
        sw.Stop();
        Console.WriteLine($"  [TIMER] Read took {sw.Elapsed.TotalMilliseconds:F2}ms");
        return data;
    }
}

public class Program
{
    public static void RunDemo()
    {
        Console.WriteLine("=== DECORATOR PATTERN DEMO ===");
        Console.WriteLine("Problem Solved: Stacking Encryption + Logging + Timing dynamically around a reader\n");

        IDataReader decoratedReader = new TimingDecorator(
            new LoggingDecorator(
                new EncryptionDecorator(
                    new FileDataReader("secrets.dat")
                )
            )
        );

        Console.WriteLine($"  Final Data Result: {decoratedReader.Read()}");
        Console.WriteLine("==============================\n");
    }
}
