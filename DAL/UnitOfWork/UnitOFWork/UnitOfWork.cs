using DAL.DbContexApp;
using DAL.Repository.Repository;
using DAL.Repository.RepositoryInterface;
using DAL.UnitOfWork.Interface;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork.UnitOFWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContextApp _context;
      public  IRepository<Category> Categories { get; }
       public IRepository<TaskItem> TaskItems { get; }

        public UnitOfWork(DbContextApp context)
        {
            _context = context;

            Categories = new Repository<Category>(_context);

            TaskItems = new Repository<TaskItem>(_context);

        }

        public void Dispose()
        {
            _context.Dispose();
        }   

        public Task<int> SaveAsync()
        {
            try
            {
                return _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ex: {ex}");
                throw;

            }     
        }
    }
}
