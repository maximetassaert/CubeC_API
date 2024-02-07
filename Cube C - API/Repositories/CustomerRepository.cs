using Cube_C___API.Models;

namespace Cube_C___API.Repositories;


    public class CustomerRepository : BaseRepository, IRepositoryData<Customer>
    {

        public CustomerRepository(ApplicationDbContext dbContext): base(dbContext)
        {}
        
        
        public Customer? FindByUserId(int userId)
        {
            return _dbContext.Customers.FirstOrDefault(customer => customer.UserId == userId);
        }



        public Customer GetById(int id)
        {
            return _dbContext.Customers.Find(id);
        }

        public List<Customer> GetAll()
        {
            return _dbContext.Customers.ToList();
        }

        public bool Create(Customer entity)
        {
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
            return true;
        }

        public bool Update(Customer entity)
        {
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
            return true;
        }

        public bool Delete(Customer entity)
        {
            _dbContext.Remove(entity);
            _dbContext.SaveChanges();
            return true;
        }
    }
