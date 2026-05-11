using BLL.DTO.Category;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICategoryService 
    {

        Task<IEnumerable<DTOCategory>> GetAll();
        Task<DTOCategory> GetOne(int id);
        Task<bool> Update(DTOCreateCategory category);
        Task<bool> Create(DTOCreateCategory category);
        Task<bool> Delete(int id);
    }
}
