using Cube_C___API.Models;
using Microsoft.EntityFrameworkCore;
using AppContext = Cube_C___API.AppContext;

namespace Cube_C___API.Repositories;


    public class CustomersRepository : IDisposable
    {
        private readonly AppContext _context;

        public CustomersRepository(AppContext context)
        {
            _context = context;
        }

        public IEnumerable<Customer> FindAll()
        {
            return _context.Customers.ToList();
        }

        public Customer FindById(int id)
        {
            return _context.Customers.Find(id);
        }

        public void Insert(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public void Delete(int id)
        {
            Customer customer = _context.Customers.Find(id);
            _context.Customers.Remove(customer);
        }

        public void Update(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
