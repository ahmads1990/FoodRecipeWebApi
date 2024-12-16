using FoodRecipeWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace FoodRecipeWebApi.Data.Repo;

public class Repository<Entity> : IRepository<Entity> where Entity : BaseModel
{
    AppDbContext _dbContext;
    DbSet<Entity> _entities;

    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _entities = _dbContext.Set<Entity>();
    }

    public IQueryable<Entity> GetAll()
    {
        return _entities;
    }

    public IQueryable<Entity> GetAllWithoutDeleted()
    {
        return _entities.Where(x => !x.Deleted);
    }

    public IQueryable<Entity> GetByCondition(Expression<Func<Entity, bool>> expression)
    {
        return GetAll().Where(expression);
    }

    public async Task<Entity?> GetByID(int id)
    {
        return await GetByCondition(x => x.ID == id).FirstOrDefaultAsync();
    }

    public bool CheckByConidition(Expression<Func<Entity, bool>> expression)
    {
        return GetAll().Any(expression);
    }

    public bool CheckExistsByID(int id)
    {
        return CheckByConidition(x => x.ID == id);
    }

    public void Add(Entity entity)
    {
        entity.CreatedDate = DateTime.Now;

        _entities.Add(entity);
    }

    public IEnumerable<Entity> AddRange(IEnumerable<Entity> entities)
    {
        foreach (var entity in entities)
        {
            entity.CreatedDate = DateTime.Now;
        }

        _entities.AddRange();
        return entities;
    }

    public void Update(Entity entity)
    {
        _entities.Update(entity);
    }

    public bool IsFound(int id)
    {
        return _entities.Any(e => e.ID.Equals(id));
    }

    public void SaveInclude(Entity entity, params string[] properties)
    {
        var entry = _entities.Local.FindEntry(entity.ID) ?? _entities.Entry(entity);

        foreach (var property in entry.Properties)
        {
            if (properties.Contains(property.Metadata.Name))
            {
                property.CurrentValue = entity.GetType().GetProperty(property.Metadata.Name).GetValue(entity);
                property.IsModified = true;
            }
        }
    }

    public void SaveExclude(Entity entity, params string[] properties)
    {
        properties = properties
        .Concat([nameof(BaseModel.ID), nameof(BaseModel.CreatedDate), nameof(BaseModel.CreatedBy)])
        .ToArray();
        var entry = _entities.Local.FindEntry(entity.ID) ?? _entities.Entry(entity);

        foreach (var property in entry.Properties)
        {
            if (!properties.Contains(property.Metadata.Name))
            {
                property.CurrentValue = entity.GetType().GetProperty(property.Metadata.Name).GetValue(entity);
                property.IsModified = true;
            }
        }
    }

    public void Delete(Entity entity)
    {
        _entities.Remove(entity);
    }

    public void SoftDelete(Entity entity)
    {
        entity.Deleted = true;
        SaveInclude(entity, nameof(BaseModel.Deleted));
    }

    public bool SaveChanges()
    {
        return _dbContext.SaveChanges() > 0;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }
}