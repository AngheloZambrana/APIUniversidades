namespace DB;

public class BecaDataInjector : DataInjector
{
    public BecaDataInjector()
    {
        _injectionCommand = @"LOAD DATA INFILE '/var/lib/mysql-files/Becas.csv'" +
                            " INTO TABLE Becas" +
                            " FIELDS TERMINATED BY ','" +
                            " LINES TERMINATED BY '\n'" +
                            " IGNORE 1 LINES" +
                            " (Id, Nombre, Descripcion, Criterios, UniversidadesID)" +
                            " SET Criterios = TRIM(TRAILING '\n' FROM Criterios)";
        Console.WriteLine(_injectionCommand);
    }
}