using MySql.Data;
using MySql.Data.MySqlClient;

namespace DB;

public class DataInjector : IDataInjector
{
    private protected string? _injectionCommand;

    public int InjectData(MySqlConnection connection)
    {
        int injectionResult = 0;

        try
        {
            MySqlCommand injectionCommand = new MySqlCommand(_injectionCommand,connection);
            injectionCommand.ExecuteNonQuery();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex);
            injectionResult = ex.Number;
        }
        return injectionResult;
    }
}