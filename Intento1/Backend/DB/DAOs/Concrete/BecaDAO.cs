using System.Text;
using Backend.DTOs.WithID;
using Backend.Entities;
using DB.SingleDAO;
using Microsoft.Extensions.Primitives;

namespace DB;

public sealed class BecaDAO : SingleDAO<Becas>, IBecaDAO
{
    public BecaDAO()
    {
        _tableName = "Becas";
    }

    private protected override Becas MapReaderToEntity()
    {
        _entity = new Becas()
        {
            BecaID = _mySqlReader!.GetInt32(0),
            Nombre = _mySqlReader!.GetString(1),
            Descripcion = _mySqlReader!.GetString(2),
            Criterios = _mySqlReader!.GetString(3),
            UniversidadID = _mySqlReader!.GetInt32(4)
        };
        _mySqlReader.Close();
        return _entity;
    }

    private protected override List<Becas> MapReaderToEntitiesList()
    {
        _entitiesList = new List<Becas>();
        while (_mySqlReader!.Read())
        {
            _entity = new Becas()
            {
                BecaID = _mySqlReader!.GetInt32(0),
                Nombre = _mySqlReader!.GetString(1),
                Descripcion = _mySqlReader!.GetString(2),
                Criterios = _mySqlReader!.GetString(3),
                UniversidadID = _mySqlReader!.GetInt32(4)
            };
            _entitiesList.Add(_entity);
        }

        _mySqlReader.Close();
        return _entitiesList;
    }

    private protected override StringBuilder CreateCommandIntoStringBuilder(Becas becas)
    {
        string becasIdC = becas.BecaID.ToString();
        string becasNombreC = becas.Nombre;
        string becasDescripcionC = becas.Descripcion;
        string becasCriteriosC = becas.Criterios;
        string becasUniversidadC = becas.UniversidadID.ToString();

        _sb = new StringBuilder();
        _sb.AppendLine("INSERT INTO ").Append(_tableName).Append("(Id, Nombre, Descripcion, Criterios, UniversidadesID) ")
            .AppendLine("VALUES (").Append(becasIdC).Append(",'")
            .Append(becasNombreC).Append("','")
            .Append(becasDescripcionC).Append("','")
            .Append(becasCriteriosC).Append("', ")
            .Append(becasUniversidadC).Append(");");
        
        return _sb;
    }

    private protected override StringBuilder UpdateCommandIntoStringBuilder(Becas becas)
    {
        string becasIdC = becas.BecaID.ToString();
        string becasNombreC = becas.Nombre;
        string becasDescripcionC = becas.Descripcion;
        string becasCriteriosC = becas.Criterios;
        string becasUniversidadC = becas.UniversidadID.ToString();
    
        _sb = new StringBuilder();
        _sb.AppendLine("UPDATE ").Append(_tableName)
            .Append(" SET Nombre = '").Append(becasNombreC).Append("', ")
            .Append("Descripcion = '").Append(becasDescripcionC).Append("', ")
            .Append("Criterios = '").Append(becasCriteriosC).Append("', ")
            .Append("UniversidadesID = ").Append(becasUniversidadC).Append(" ") 
            .Append("WHERE Id = ").Append(becasIdC).Append(";");
    
        return _sb;
    }


}
