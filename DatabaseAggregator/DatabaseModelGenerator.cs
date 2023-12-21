using System.Diagnostics;
using System.Reflection;
using MySqlConnector;

namespace DatabaseAggregator;

public class DatabaseModelGenerator
{
    private readonly string _connectionString;
    private readonly string _server;
    private readonly string _database;
    private readonly string _uid;
    private readonly string _pwd;
    private readonly string _netVersion;
    private readonly string _dllPath;
    
    public bool IsGenerated { get; private set; }
    public string DbContextTypeName { get; }

    public DatabaseModelGenerator(string server, string database, string uid, string pwd, string netVersion = "net8.0")
    {
        _server = server;
        _database = database;
        _uid = uid;
        _pwd = pwd;
        _netVersion = netVersion;
        _connectionString = $"server={server};database={database};uid={uid};pwd={pwd};";
        
        DbContextTypeName = $"DatabaseModel.Context.{database}DatabaseContext";

        // Проверка существования проекта DatabaseModel
        if (!Directory.Exists("DatabaseModel") || !File.Exists("DatabaseModel/DatabaseModel.csproj"))
        {
            throw new DirectoryNotFoundException("Project DatabaseModel not found");
        }

        _dllPath = Path.Combine("DatabaseModel", "bin", "Release", netVersion, "DatabaseModel.dll");
    }

    /// <summary>
    /// Вызывает scaffoldDB.bat для генерации DatabaseModel.dll
    /// </summary>
    /// <returns>True, если генерация прошла успешно</returns>
    public bool TryGenerate()
    {
        // Проверка возможности подключения к БД
        using var connection = new MySqlConnection(_connectionString);
        try
        {
            connection.Open();
        }
        catch (MySqlException)
        {
            return false;
        }

        // Вызов scaffoldDB.bat, не отображая консольное окно
        var process = new Process
        {
            StartInfo =
            {
                FileName = Path.Combine("DatabaseModel", "scaffoldDB.bat"),
                Arguments = $"{_server} {_database} {_uid} {_pwd} {_netVersion}",
                CreateNoWindow = false, // TODO: true
                UseShellExecute = false
            }
        };

        process.Start();

        // Ожидание завершения процесса
        process.WaitForExit();

        // Проверка успешности генерации
        if (process.ExitCode != 0)
        {
            return false;
        }

        // Проверка существования dll
        if (!File.Exists(_dllPath))
        {
            return false;
        }

        IsGenerated = true;
        return true;
    }

    /// <summary>
    /// Возвращает сборку DatabaseModel.dll, если она сгенерирована
    /// </summary>
    /// <returns>Сборка DatabaseModel.dll или null, если она не сгенерирована</returns>
    public Assembly? GetAssembly()
    {
        if (!IsGenerated)
        {
            return null;
        }

        return Assembly.LoadFrom(_dllPath);
    }
}