namespace DB;

public class FacultadesDataInjector : DataInjector
{
    public FacultadesDataInjector()
    {
        _injectionCommand = @"LOAD DATA INFILE '/var/lib/mysql-files/Facultades.csv' " +
                            "INTO TABLE Facultades " +
                            "FIELDS TERMINATED BY ',' " +
                            "LINES TERMINATED BY '\n' " +
                            "IGNORE 1 LINES " +
                            "(Id, Nombre, Descricpion, UniversidadID) " +
                            "SET Descricpion = TRIM(TRAILING '\r' FROM Descricpion);";
        Console.WriteLine(_injectionCommand);
    }
}
