namespace Parker.Interfaces;

public interface IEntityService<TEntity> where TEntity : class
{
    List<TEntity> GetAllEntities();
    TEntity GetEntityById(int id);
    void AddEntity(TEntity entity);
    void UpdateEntity(TEntity entity);
    void DeleteEntity(int id);
}