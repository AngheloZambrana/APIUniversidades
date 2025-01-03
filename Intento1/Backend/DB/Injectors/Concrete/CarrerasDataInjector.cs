namespace DB;

public class CarrerasDataInjector : DataInjector
{
    public CarrerasDataInjector()
    {
        _injectionCommand = @"LOAD DATA INFILE '/var/lib/mysql-files/Carreras.csv' " +
                            "INTO TABLE Carreras " +
                            "FIELDS TERMINATED BY ',' " +
                            "LINES TERMINATED BY '\n' " +
                            "IGNORE 1 LINES " +
                            "(Id, Nombre, Duracion, FacultadID);";
        Console.WriteLine(_injectionCommand);
    }
}