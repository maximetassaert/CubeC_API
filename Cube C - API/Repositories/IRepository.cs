namespace Cube_C___API.Repositories;

public interface IRepositoryData<T>
{
    T GetById(int id);
    List<T> GetAll();
    bool Create(T entity);
    bool Update(T entity);
    bool Delete(T entity);
}