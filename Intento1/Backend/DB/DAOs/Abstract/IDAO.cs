namespace DB;

public interface IDAO<T>
{
    int Create(T entity);
    List<T> ReadAll();
    int Update(T entity);
}