using System;
using System.Collections.Concurrent;

namespace DesignPatterns.Creational.Singleton;

public sealed class AppConfigurationManager
{
    private static readonly Lazy<AppConfigurationManager> _instance = new(() => new AppConfigurationManager());
    
    public static AppConfigurationManager Instance => _instance.Value;

    private readonly ConcurrentDictionary<string, string> _settings = new();
    public DateTime LoadedTimestamp { get; }

    private AppConfigurationManager()
    {
        LoadedTimestamp = DateTime.Now;
        Console.WriteLine($"  [AppConfigurationManager] Initializing Global Configuration Instance at {LoadedTimestamp:HH:mm:ss.fff}...");

        _settings["Environment"] = "Production-US-East";
        _settings["DatabaseConnectionString"] = "Server=db.prod.internal;Database=FintechDB;User=app;Password=***;";
        _settings["MaxConnections"] = "100";
        _settings["Feature_NewCheckoutFlow"] = "true";
    }

    public string GetSetting(string key, string defaultValue = "")
    {
        return _settings.TryGetValue(key, out var val) ? val : defaultValue;
    }

    public void UpdateSetting(string key, string value)
    {
        _settings[key] = value;
        Console.WriteLine($"  [Config Update] Set '{key}' = '{value}'");
    }

    public void PrintAllSettings()
    {
        Console.WriteLine($"\n📋 Active App Configuration Snapshot (Loaded: {LoadedTimestamp:HH:mm:ss.fff}):");
        foreach (var kv in _settings)
        {
            Console.WriteLine($"   • {kv.Key,-30} = {kv.Value}");
        }
    }
}
