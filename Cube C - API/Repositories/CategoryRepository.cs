using Cube_C___API.Models;

namespace Cube_C___API.Repositories;

public class CategoryRepository : BaseRepository, IRepositoryData<Category>
{
    
    public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {}

    public Category GetById(int id)
    {
        return _dbContext.Categories.Find(id);
    }

    public List<Category> GetAll()
    {
        return _dbContext.Categories.ToList();
    }

    public bool Create(Category entity)
    {
        _dbContext.Add(entity);
        _dbContext.SaveChanges();
        return true;
    }

    public bool Update(Category entity)
    {
        _dbContext.Update(entity);
        _dbContext.SaveChanges();
        return true;
    }

    public bool Delete(Category entity)
    {
        _dbContext.Remove(entity);
        _dbContext.SaveChanges();
        return true;
    }
}
