using FoodRecipeWebApi.Helpers;
using FoodRecipeWebApi.Models;
using System.Linq.Expressions;
namespace FoodRecipeWebApi.Data.Repo;

public interface IRepository<Entity> where Entity : BaseModel
{
    IQueryable<Entity> GetAll();
    IQueryable<Entity> GetAllWithoutDeleted();
    IQueryable<Entity> GetByPage(PaginationHelper paginationParams);
    IQueryable<Entity> GetByCondition(Expression<Func<Entity, bool>> expression);
    Task<Entity?> GetByIDAsync(int id);
    Entity? GetByID(int id);
    bool CheckByConidition(Expression<Func<Entity, bool>> expression);
    bool CheckExistsByID(int id);
    void Add(Entity entity);
    IEnumerable<Entity> AddRange(IEnumerable<Entity> entities);
    void Update(Entity entity);
    void SaveInclude(Entity entity, params string[] properties);
    void SaveExclude(Entity entity, params string[] properties);
    void Delete(Entity entity);
    void SoftDelete(Entity entity);
    bool SaveChanges();
    Task<bool> SaveChangesAsync();
}