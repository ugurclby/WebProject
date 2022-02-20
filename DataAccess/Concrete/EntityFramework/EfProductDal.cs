using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfRepositoryBase<Product, NorthWindContext>, IProductDal
    {
        public List<ProductDetail> GetProductDetails()
        {
            using (NorthWindContext context = new NorthWindContext())
            {
                var result = (from p in context.Products
                             join c in context.Categories on
                             p.CategoryID equals c.CategoryID
                             select new ProductDetail { CategoryID = c.CategoryID, CategoryName = c.CategoryName, ProductID = p.ProductID, ProductName = p.ProductName }).ToList();
                return result;

            }
        }

        public List<ViewProductDetail> GetViewProductDetails()
        {
            using (NorthWindContext context = new NorthWindContext())
            { 
                var test = context.Set<ViewProductDetail>().FromSqlRaw("SELECT * FROM ViewProductDetail").ToList(); 
                return test; 
            }

        }
    }
}
