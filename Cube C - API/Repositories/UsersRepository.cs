using Cube_C___API.Models;
using Microsoft.EntityFrameworkCore;
using AppContext = Cube_C___API.Models.AppContext;

namespace Cube_C___API.Repositories;


    public class UsersRepository : IDisposable
    {
        private AppContext context;

        public UsersRepository(AppContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> GetStudents()
        {
            return context.Users.ToList();
        }

        public User GetUserByID(int id)
        {
            return context.Users.Find(id);
        }

        public void InsertUser(User user)
        {
            context.Users.Add(user);
        }

        public void DeleteUser(int userId)
        {
            User user = context.Users.Find(userId);
            context.Users.Remove(user);
        }

        public void UpdateUser(User user)
        {
            context.Entry(user).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
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
