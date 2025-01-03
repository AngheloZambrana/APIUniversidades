using System.Text;
using Backend.Entities;
using DB.SingleDAO;
using Microsoft.Extensions.Primitives;


namespace DB;

public sealed class UniversidadesDAO : SingleDAO<Universidades>, IUniversidadesDAO
{
    public UniversidadesDAO()
    {
        _tableName = "Universidades";
    }

    private protected override Universidades MapReaderToEntity()
    {
        _entity = new Universidades()
        {
            UniversidadID = _mySqlReader!.GetInt32(0),
            Name = _mySqlReader!.GetString(1),
            Descripcion = _mySqlReader!.GetString(2),
            Tipo = _mySqlReader!.GetString(3)
        };
        _mySqlReader.Close();
        return _entity;
    }

    private protected override List<Universidades> MapReaderToEntitiesList()
    {
        _entitiesList = new List<Universidades>();
        while (_mySqlReader!.Read())
        {
            _entity = new Universidades
            {
                UniversidadID = _mySqlReader!.GetInt32(0),
                Name = _mySqlReader!.GetString(1),
                Descripcion = _mySqlReader!.GetString(2),
                Tipo = _mySqlReader!.GetString(3)
            };
            _entitiesList.Add(_entity);
        }
        _mySqlReader.Close();
        return _entitiesList;
    }

    private protected override StringBuilder CreateCommandIntoStringBuilder(Universidades universidades)
    {
        string universidadesIdC = universidades.UniversidadID.ToString();
        string universidadesNameC = universidades.Name;
        string universidadesDescripcionC = universidades.Descripcion;
        string universidadesTipoC = universidades.Tipo;
        
        _sb = new StringBuilder();
        _sb.Append("INSERT INTO ").Append(_tableName).Append(" (Id, Name, Descripcion, Tipo)")
            .Append("VALUES (").Append(universidadesIdC).Append(",'")
            .Append(universidadesNameC).Append("','")
            .Append(universidadesDescripcionC).Append("','")
            .Append(universidadesTipoC).Append("');");
        return _sb;
    }

    private protected override StringBuilder UpdateCommandIntoStringBuilder(Universidades universidades)
    {
        string universidadesIdC = universidades.UniversidadID.ToString();
        string universidadesNameC = universidades.Name;
        string universidadesDescripcionC = universidades.Descripcion;
        string universidadesTipoC = universidades.Tipo;
        
        _sb = new StringBuilder();
        _sb.Append("UPDATE ").Append(_tableName)
            .Append(" SET Name = '").Append(universidadesNameC).Append("',")
            .Append("Descripcion = '").Append(universidadesDescripcionC).Append("',")
            .Append("Tipo = '").Append(universidadesTipoC).Append("' ")
            .Append("WHERE Id = ").Append(universidadesIdC).Append(";");
        return _sb;
    }
}