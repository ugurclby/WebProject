using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess;
using Entities.Dto;

namespace DataAccess.Abstract
{
    public interface IProductDal: IRepositoryDal<Product>
    {
        List<ProductDetail> GetProductDetails();
        List<ViewProductDetail> GetViewProductDetails();
    }
}
