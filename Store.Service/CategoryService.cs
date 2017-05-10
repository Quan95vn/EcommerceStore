using Store.Data.Infrastructure;
using Store.Data.Repositories;
using Store.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Store.Service
{
    public interface ICategoryService
    {
        Category Add(Category Category);

        void Update(Category Category);

        Category Delete(int id);

        IQueryable<Category> GetAll();

        IQueryable<Category> GetAll(int page, int pageSize, string keyword);

        Category GetById(int id);

        void Save();

    }

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public Category Add(Category Category)
        {
            return _categoryRepository.Add(Category);
        }

        public Category Delete(int id)
        {
            return _categoryRepository.Delete(id);
        }

        public IQueryable<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public IQueryable<Category> GetAll(int page, int pageSize, string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _categoryRepository
                    .GetMulti(x => x.Name.Contains(keyword));
            }
            else
            {
                return _categoryRepository.GetAll();
            }
        }

        public Category GetById(int id)
        {
            return _categoryRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Category Category)
        {
            _categoryRepository.Update(Category);
        }
    }
}
