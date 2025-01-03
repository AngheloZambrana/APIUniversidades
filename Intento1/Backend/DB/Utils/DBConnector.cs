using System.Data;
using MySql.Data.MySqlClient;

namespace DB;

public static class DBConnector
{
    private static MySqlConnection _conn = new MySqlConnection("");

    private static string GetConnectionString()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("/webapp/dbsettings.json", optional: true)
            .Build();

        Console.WriteLine($"Connection String: {config["DbSettings:ConnectionUrl"]}");
        return config["DbSettings:ConnectionUrl"] ?? "";
    }


    public static MySqlConnection GetConnection()
    {
        return _conn;
    }

    public static void OpenConnection()
    {
        _conn = new MySqlConnection(GetConnectionString());
        _conn.Open();
    }
}