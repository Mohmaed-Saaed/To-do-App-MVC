using DAL.Repository.RepositoryInterface;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork.Interface
{
    public interface IUnitOfWork : IDisposable
    {
      public  IRepository<Category> Categories { get; }

       public IRepository<TaskItem> TaskItems { get; }

        public Task<int> SaveAsync();
    }
}
