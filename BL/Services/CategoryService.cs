using BLL.DTO.Category;
using BLL.Interfaces;
using DAL.Repository.RepositoryInterface;
using DAL.UnitOfWork.Interface;
using Domain.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly IRepository<Category> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService( IRepository<Category> repository, IUnitOfWork unitOfWork )
        {
            _repository = repository;
            _unitOfWork = unitOfWork;

        }

        public async Task<bool> Create(DTOCreateCategory category)
        {
            if (await _repository.CreateAsync(category.Adapt<Category>()))
                if(await _unitOfWork.SaveAsync() > 0)
                    return true;
            
            return false;
        }

        public async Task<bool> Delete(int id)
        {

            var category = await _repository.GetOneAsync(c => c.Id == id)
                ?? throw new Exception("Category not found");

            if (await _repository.DeleteAsync(category))
                if (await _unitOfWork.SaveAsync() > 0)
                    return true;

             return false;
        }

        public async Task<DTOCategory> GetOne(int id)
        {
            var category = await _repository.GetOneAsync(c => c.Id == id);

            return category == null ? throw new Exception("Category not found") : category.Adapt<DTOCategory>();
        }

        public async Task<bool> Update(DTOCreateCategory category)
        {
          if(await _repository.UpdateAsync(category.Adapt<Category>()))
                if (await _unitOfWork.SaveAsync() > 0)
                    return true;
            return false;
        }

        async Task<IEnumerable<DTOCategory>> ICategoryService.GetAll()
        {
            return (await _repository.GetAsync()).Adapt<IEnumerable<DTOCategory>>().ToList();
        }
    }
}
