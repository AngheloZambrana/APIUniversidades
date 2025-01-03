using System.Text;
using Backend.Entities;
using DB.SingleDAO;

namespace DB;

public sealed class CarrerasDAO : SingleDAO<Carreras>, ICarreraDAO
{
    public CarrerasDAO()
    {
        _tableName = "Carreras";
    }

    private protected override Carreras MapReaderToEntity()
    {
        _entity = new Carreras()
        {
            CarrerasID = _mySqlReader!.GetInt32(0),
            Nombre = _mySqlReader!.GetString(1),
            Duracion = _mySqlReader!.GetInt32(2),
            FacultadID = _mySqlReader!.GetInt32(3)
        };
        _mySqlReader.Close();
        return _entity;
    }

    private protected override List<Carreras> MapReaderToEntitiesList()
    {
        _entitiesList = new List<Carreras>();
        while (_mySqlReader!.Read())
        {
            _entity = new Carreras()
            {
                CarrerasID = _mySqlReader!.GetInt32(0),
                Nombre = _mySqlReader!.GetString(1),
                Duracion = _mySqlReader!.GetInt32(2),
                FacultadID = _mySqlReader!.GetInt32(3)
            };
            _entitiesList.Add(_entity);
        }
        _mySqlReader.Close();
        return _entitiesList;
    }

    private protected override StringBuilder CreateCommandIntoStringBuilder(Carreras carreras)
    {
        string carrerasIdC = carreras.CarrerasID.ToString();
        string carrerasNombreC = carreras.Nombre;
        string carrerasDuracionC = carreras.Duracion.ToString();
        string carrerasFacultadIdC = carreras.FacultadID.ToString();
        
        _sb = new StringBuilder();
        _sb.Append("INSERT INTO ").Append(_tableName).Append("(Id, Nombre, Duracion, FacultadID)")
            .Append(" VALUES (").Append(carrerasIdC).Append(", '")
            .Append(carrerasNombreC).Append("',")
            .Append(carrerasDuracionC).Append(",")
            .Append(carrerasFacultadIdC).Append(");");
        
        return _sb;
    }

    private protected override StringBuilder UpdateCommandIntoStringBuilder(Carreras carreras)
    {
        string carrerasIdC = carreras.CarrerasID.ToString();
        string carrerasNombreC = carreras.Nombre;
        string carrerasDuracionC = carreras.Duracion.ToString();
        string carrerasFacultadIdC = carreras.FacultadID.ToString();
        
        _sb = new StringBuilder();
        _sb.Append("UPDATE ").Append(_tableName)
            .Append(" SET Nombre = '").Append(carrerasNombreC).Append("', ")
            .Append("Duracion = ").Append(carrerasDuracionC).Append(", ")
            .Append("FacultadID = ").Append(carrerasFacultadIdC).Append(" ")
            .Append("WHERE Id = ").Append(carrerasIdC).Append(";");

        return _sb;
    }
    
}