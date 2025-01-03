namespace DB;

public interface ISingleDAO<T> : IDAO<T>
{
    T? Read(int id);
    bool Delete(int id);
}