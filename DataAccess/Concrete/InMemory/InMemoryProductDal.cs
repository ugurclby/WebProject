using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;
        public InMemoryProductDal()
        {
            _products = new List<Product>()
            {
                new Product{ProductID=1,CategoryID=1,ProductName="Telefon",UnitPrice=10,UnitsInStock=5},
                new Product{ProductID=2,CategoryID=2,ProductName="Kamera",UnitPrice=112,UnitsInStock=7},
                new Product{ProductID=3,CategoryID=3,ProductName="Araba",UnitPrice=334,UnitsInStock=88},
                new Product{ProductID=4,CategoryID=4,ProductName="Uçak",UnitPrice=556,UnitsInStock=90},
                new Product{ProductID=5,CategoryID=5,ProductName="Kalem",UnitPrice=55,UnitsInStock=4},
            };
        }
        public void Add(Product product)
        {
            _products.Add(product); 
        }

        public void Delete(Product product)
        {
            Product deletedProduct=_products.Where(x => x.ProductID == product.ProductID).FirstOrDefault();

            _products.Remove(deletedProduct);
        }

        public Product Get(Expression<Func<Product, bool>> fileter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            return _products;
        }

        public List<Product> GetByCategory(int categoryId)
        {
            return _products.Where(x => x.CategoryID == categoryId).ToList();

        }

        public List<ProductDetail> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public List<ViewProductDetail> GetViewProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            Product deletedUpdate = _products.Where(x => x.ProductID == product.ProductID).FirstOrDefault();
            deletedUpdate.CategoryID = product.CategoryID;
            deletedUpdate.ProductName = product.ProductName;
            deletedUpdate.UnitPrice = product.UnitPrice;
            deletedUpdate.UnitsInStock = product.UnitsInStock;

        }
    }
}
