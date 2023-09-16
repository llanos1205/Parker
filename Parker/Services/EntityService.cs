using Parker.Interfaces;

namespace Parker.Services;


// Services/EntityService.cs
public class EntityService<TEntity> : IEntityService<TEntity> where TEntity : class
{
    private readonly IRepository<TEntity> _repository;

    public EntityService(IRepository<TEntity> repository)
    {
        _repository = repository;
    }

    public List<TEntity> GetAllEntities()
    {
        return _repository.GetAll();
    }

    public TEntity GetEntityById(int id)
    {
        return _repository.GetById(id);
    }

    public void AddEntity(TEntity entity)
    {
        _repository.Add(entity);
    }

    public void UpdateEntity(TEntity entity)
    {
        _repository.Update(entity);
    }

    public void DeleteEntity(int id)
    {
        _repository.Delete(id);
    }
}
