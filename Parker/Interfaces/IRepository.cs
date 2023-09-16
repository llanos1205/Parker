namespace Parker.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    TEntity GetById(int id);
    List<TEntity> GetAll();
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(int id);
}