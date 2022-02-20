using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();

        IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max);

        IDataResult<List<ViewProductDetail>> GetViewProductDetails();

        IResult Add(Product product);

        IDataResult<Product> Get(int productId);

        IResult Update(Product product);
    }
}
