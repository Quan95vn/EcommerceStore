using Store.Data.Infrastructure;
using Store.Data.Repositories;
using Store.Model.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Store.Service
{
    public interface IProductService
    {
        Product Add(Product Product);

        void Update(Product Product);

        Product Delete(int id);

        IQueryable<Product> GetAll();

        IQueryable<Product> GetAll(string keyword);

        IEnumerable<Product> GetLatestProduct(int top);

        IEnumerable<Product> GetHotProduct(int top);

        //IEnumerable<Product> GetListProductByCategoryPaging(int categoryID, int page, int pageSize, string sort, out int totalRow);

        IEnumerable<Product> GetSaleProducts(int top);

        IEnumerable<Product> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        IEnumerable<Product> GetListProductByName(string name);

        IEnumerable<Product> GetListProduct(string keyword);

        Product GetById(int id);

        IEnumerable<Product> GetAllByParentId(int parentId);

        IEnumerable<Product> GetRelatedProduct(int id, int top);

        IEnumerable<Product> GetFeatureProduct(int top);

        IEnumerable<Product> GetListProductByCategoryPaging(int categoryId, int page, int pageSize, string sort_by, out int totalRow);

        void IncreaseView(int id);

        bool SellProduct(int productId, int quantity);

        void Save();

    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public Product Add(Product Product)
        {
            return _productRepository.Add(Product);
        }

        public Product Delete(int id)
        {
            return _productRepository.Delete(id);
        }

        public IQueryable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public IQueryable<Product> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _productRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _productRepository.GetAll();
        }

        public IEnumerable<Product> GetAllByParentId(int parentId)
        {
            return _productRepository.GetMulti(x => x.Status && x.CategoryId == parentId);
        }

        public Product GetById(int id)
        {
            return _productRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Product Product)
        {
             _productRepository.Update(Product);
        }

        public IEnumerable<Product> GetLatestProduct(int top)
        {
            return _productRepository.GetMulti(x => x.Status).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetHotProduct(int top)
        {
            return _productRepository.GetMulti(x => x.Status && x.HotFlag == true).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        //public IEnumerable<Product> GetListProductByCategoryPaging(int categoryID, int page, int pageSize, string sort, out int totalRow)
        //{
        //    var query = _productRepository.GetMulti(x => x.Status && x.CategoryId == categoryID);
        //    switch (sort)
        //    {
        //        case "popular":
        //            query = query.OrderByDescending(x => x.ViewCount);
        //            break;
        //        case "discount":
        //            query = query.OrderByDescending(x => x.PromotionPrice.HasValue);
        //            break;
        //        case "price":
        //            query = query.OrderBy(x => x.Price);
        //            break;
        //        default:
        //            query = query.OrderByDescending(x => x.CreatedDate);
        //            break;
        //    }
        //    totalRow = query.Count();
        //    return query.Skip((page - 1) * pageSize).Take(pageSize);
        //}

        public IEnumerable<Product> GetListProductByName(string name)
        {
            
            var result =  _productRepository.GetMulti(x => x.Status && x.Name.Contains(name));
            return result;
        }

        public IEnumerable<Product> Search(string keyword, int page, int pageSize, string sort_by, out int totalRow)
        {
            var query = _productRepository.GetMulti(x => x.Status && x.Name.Contains(keyword));
            switch (sort_by)
            {
                case "most-viewed":
                    query = query.Where(x => x.ViewCount >= 20).OrderByDescending(x => x.CreatedDate);
                    break;
                case "discount":
                    query = query.Where(x => x.PromotionPrice.HasValue).OrderByDescending(x => x.CreatedDate);
                    break;
                case "featured":
                    query = query.Where(x => x.IsFeature == true).OrderByDescending(x => x.CreatedDate);
                    break;
                case "best-selling":
                    query = query.Where(x => x.HotFlag == true).OrderByDescending(x => x.CreatedDate);
                    break;
                case "price-ascending":
                    query = query.OrderBy(x => x.Price);
                    break;
                case "price-descending":
                    query = query.OrderByDescending(x => x.Price);
                    break;
                case "date-descending":
                    query = query.OrderByDescending(x => x.Price);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Product> GetRelatedProduct(int id, int top)
        {
            var product = _productRepository.GetSingleById(id);
            return _productRepository.GetMulti(x => x.Status && x.Id != id && x.CategoryId == product.CategoryId).OrderByDescending(x => x.CreatedDate).Take(top);

        }

      
        public IEnumerable<Product> GetSaleProducts(int top)
        {
            return _productRepository.GetMulti(x => x.Status == true && x.PromotionPrice.HasValue)
                .OrderByDescending(x => x.PromotionPrice)
                .Take(top);
        }

        public IEnumerable<Product> GetFeatureProduct(int top)
        {
            return _productRepository.GetMulti(x => x.Status == true && x.IsFeature == true).OrderByDescending(x=>x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetListProductByCategoryPaging(int categoryId, int page, int pageSize, string sort_by, out int totalRow)

        {
            var query = _productRepository.GetMulti(x => x.Status && x.CategoryId == categoryId);

            switch (sort_by)
            {
                case "most-viewed":
                    query = query.Where(x => x.ViewCount >= 20).OrderByDescending(x=>x.CreatedDate);
                    break;
                case "discount":
                    query = query.Where(x => x.PromotionPrice.HasValue).OrderByDescending(x => x.CreatedDate);
                    break;
                case "featured":
                    query = query.Where(x => x.IsFeature == true).OrderByDescending(x=>x.CreatedDate);
                    break;
                case "best-selling":
                    query = query.Where(x=>x.HotFlag ==true).OrderByDescending(x => x.CreatedDate);
                    break;
                case "price-ascending":
                    query = query.OrderBy(x => x.Price);
                    break;
                case "price-descending":
                    query = query.OrderByDescending(x => x.Price);
                    break;
                case "date-descending":
                    query = query.OrderByDescending(x => x.Price);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public void IncreaseView(int id)
        {
            var product = _productRepository.GetSingleById(id);
            if (product.ViewCount.HasValue)
            {
                product.ViewCount += 1;
            }
            else
            {
                product.ViewCount = 1;
            }
        }

        public bool SellProduct(int productId, int quantity)
        {
            var product = _productRepository.GetSingleById(productId);
            if (product.Quantity < quantity)
                return false;

            product.Quantity -= quantity;
            return true;

        }

        public IEnumerable<Product> GetListProduct(string keyword)
        {
            IEnumerable<Product> query;
            if (!string.IsNullOrEmpty(keyword))
                query = _productRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                query = _productRepository.GetAll();
            return query;
        }
    }
}
