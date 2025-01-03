using System.Text;
using Backend.Entities;
using DB.SingleDAO;
using Microsoft.Extensions.Primitives;

namespace DB;

public sealed class FacultadesDAO : SingleDAO<Facultades>, IFacultadDAO
{
    public FacultadesDAO()
    {
        _tableName = "Facultades";
    }

    private protected override Facultades MapReaderToEntity()
    {
        _entity = new Facultades()
        {
            FacultadID = _mySqlReader!.GetInt32(0),
            Nombre = _mySqlReader!.GetString(1),
            Descripcion = _mySqlReader!.GetString(2),
            UniversidadID = _mySqlReader!.GetInt32(3)
        };
        _mySqlReader.Close();
        return _entity;
    }

    private protected override List<Facultades> MapReaderToEntitiesList()
    {
        _entitiesList = new List<Facultades>();
        while (_mySqlReader!.Read())
        {
            _entity = new Facultades()
            {
                FacultadID = _mySqlReader!.GetInt32(0),
                Nombre = _mySqlReader!.GetString(1),
                Descripcion = _mySqlReader!.GetString(2),
                UniversidadID = _mySqlReader!.GetInt32(3)
            };
            _entitiesList.Add(_entity);
        }
        _mySqlReader.Close();
        return _entitiesList;
    }

    private protected override StringBuilder CreateCommandIntoStringBuilder(Facultades facultades)
    {
        string facultadesIdC = facultades.FacultadID.ToString();
        string facultadesNombreC = facultades.Nombre;
        string facultadesDescripcionC = facultades.Descripcion;
        string facultadesUniversidadC = facultades.UniversidadID.ToString();
        
        _sb = new StringBuilder();
        _sb.Append("INSERT INTO ").Append(_tableName).Append("(Id, Nombre, Descricpion, UniversidadID)")
            .Append("VALUES (").Append(facultadesIdC).Append(",'")
            .Append(facultadesNombreC).Append("','")
            .Append(facultadesDescripcionC).Append("',")
            .Append(facultadesUniversidadC).Append(");");
        return _sb;
    }

    private protected override StringBuilder UpdateCommandIntoStringBuilder(Facultades facultades)
    {
        string facultadesIdC = facultades.FacultadID.ToString();
        string facultadesNombreC = facultades.Nombre;
        string facultadesDescripcionC = facultades.Descripcion;
        string facultadesUniversidadC = facultades.UniversidadID.ToString();
        
        _sb = new StringBuilder();
        _sb.Append("UPDATE ").Append(_tableName)
            .Append(" SET Nombre = '").Append(facultadesNombreC).Append("', ")
            .Append("Descricpion = '").Append(facultadesDescripcionC).Append("', ")
            .Append("UniversidadID = ").Append(facultadesUniversidadC).Append(" ")
            .Append("WHERE Id = ").Append(facultadesIdC).Append(";");
        
        return _sb;
    }
    
}