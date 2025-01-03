using System.Data;
using MySql.Data.MySqlClient;

namespace DB;

public static class DBInjector
{
    private static MySqlConnection _conn = DBConnector.GetConnection();
    
    public static void TruncateAllTables()
    {
        if (_conn.State != ConnectionState.Open)
        {
            _conn.Open();
        }

        using (MySqlCommand com = new MySqlCommand("TruncateAllTables", _conn))
        {
            com.CommandType = CommandType.StoredProcedure;
            com.ExecuteNonQuery();
        }
    }

    public static void InjectData()
    {
        try 
        {
            Console.WriteLine("Iniciando inserción de Universidades...");
            IDataInjector injector = new UniversidadesDataInjector();
            injector.InjectData(_conn);
            Console.WriteLine("Universidades insertadas correctamente");
        
            Console.WriteLine("Iniciando inserción de Facultades...");
            injector = new FacultadesDataInjector();
            injector.InjectData(_conn);
            Console.WriteLine("Facultades insertadas correctamente");

            Console.WriteLine("Iniciando inserción de Becas...");
            injector = new BecaDataInjector();
            injector.InjectData(_conn);
            Console.WriteLine("Becas insertadas correctamente");
            
            Console.WriteLine("Iniciando inserción de Carreras...");
            injector = new CarrerasDataInjector();
            injector.InjectData(_conn);
            Console.WriteLine("Carreras insertadas correctamente");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error durante la inserción: {ex.Message}");
            throw;
        }
    }
    
}