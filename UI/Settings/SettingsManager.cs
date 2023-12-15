using System.Text.Json;

namespace UI.Settings;

public class SettingsManager
{
    private readonly string _filePath;
    public SettingsManager(string filePath)
    {
        _filePath = filePath;
    }

    public SettingsModel Load()
    {
        if (!File.Exists(_filePath))
        {
            throw new FileNotFoundException("Settings file not found", _filePath);
        }

        var json = File.ReadAllText(_filePath);
        var settings = JsonSerializer.Deserialize<SettingsModel>(json);
        return settings ?? throw new JsonException("Settings file isn't valid");
    }

    public void Save(SettingsModel settingsModel)
    {
        var json = JsonSerializer.Serialize(settingsModel, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
    
    public void UpdateConnectionSettings(ConnectionSettingsModel connectionSettingsModel)
    {
        Save(new SettingsModel { ConnectionSettings = connectionSettingsModel });
    }
}