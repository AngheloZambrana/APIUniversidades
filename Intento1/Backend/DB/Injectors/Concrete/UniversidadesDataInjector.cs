namespace DB;

public sealed class UniversidadesDataInjector : DataInjector
{
    public UniversidadesDataInjector()
    {
        _injectionCommand = @"LOAD DATA INFILE '/var/lib/mysql-files/Universidades.csv' " +
                            "INTO TABLE Universidades " +
                            "FIELDS TERMINATED BY ',' " +
                            "LINES TERMINATED BY '\n' " +
                            "IGNORE 1 LINES " +
                            "(Id, Name, Descripcion, Tipo) " +
                            "SET Descripcion = TRIM(TRAILING '\r' FROM Descripcion);";
        Console.WriteLine(_injectionCommand);
    }
}
