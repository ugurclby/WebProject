using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using System.Reflection;
using System.Threading;
using Business.BusinessAspects.Autofac;
using ConstResource;
using FluentValidation;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttinConcerns.Validation;
using Core.Aspects.Autofac.Validation;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Core.Aspects.Transaction;
using Core.Utilities.Business;
using DataAccess.Concrete.EntityFramework;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService; // bir manager sınıfı başka bir entity ile çalışması gerekirse asla dal ile iletişime geçmez. Service interface ile iletişim kurar.

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [CacheRemoveAspect("IProductService.Get")]
        [SecuredOperation("admin,product.add")]
        [ValidationAspect(typeof(ProductValidator))]
        [TransactionScopeAspect]
        public IResult Add(Product product)
        {
            //Aşğaıdaki kod fluentvalidation a geçmiştir.
            //if (product.ProductName.Length<2)
            //{  
            //    //return new ErrorResult(Messages.UrunUzunluk);
            //    return new ErrorResult(ResTr.UrunUzunluk);
            //}
            //ValidationTool.Validate(new ProductValidator(), product); 

            IResult Result = BusinessRule.Run(ProductAddCategoryControl(product.CategoryID),
                SameNameProductControl(product.ProductName), 
                CategorySonuc(_categoryService), CategorySonuc(_categoryService));
            
            if (Result != null)
            {
                return Result;
            } 

            _productDal.Add(product);

            // Transaction yönetimi için test : 58. satırdaki kod çalışır. Fakat [TransactionScopeAspect] sayesinde savechanges yapmaz. Methodun hata almadan bitmesini bekler. 
            // hata alırsa aşağıdaki gibi. Rollback yapar ve fırlatır.
            //if (product.UnitPrice<100)
            //{
            //    throw new Exception("hata");
            //}

            //_productDal.Add(product);

            return new Result(true,Messages.KayitEklendi);
        }
        public IResult CategorySonuc(ICategoryService categoryService)
        {
            if (categoryService.GetAllCategory().Data.Count>=10)
            {
                return new ErrorResult("Bir kategoride en fazla 10 ürün olabilir");
            }

            return new SuccessResult();

        }
        private IResult ProductAddCategoryControl(int categoryId)
        {
            if (_productDal.GetAll(x => x.CategoryID == categoryId).Count >= 10)
            {
                return new ErrorResult("Bir kategoride en fazla 10 ürün olabilir");
            }

            return new SuccessResult();
        }

        private IResult SameNameProductControl(string productName)
        {
            if (_productDal.GetAll(x => x.ProductName.ToLower() == productName.ToLower()).Count> 0)
            {
                return new ErrorResult("Aynı ürün ismine sahip başka ürün var");
            }

            return new SuccessResult();
        }


        [CacheAspect()]
        public IDataResult<Product> Get(int productId)
        {
            return  new SuccessDataResult<Product>(_productDal.Get(x => x.ProductID == productId),ResTr.KayitGetirildi); 
        }

        [CacheAspect()]
        [PerformanceAspect(1)]
        public IDataResult<List<Product>> GetAll()
        {
            //Thread.Sleep(TimeSpan.FromSeconds(10));
            // İş Kodları - Yetkisi var mı kontrolleri

            //if (DateTime.Now.Hour == 13)
            //{
            //    return new ErrorDataResult<List<Product>>(ResTr.SaatOnHata);
            //}
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), ResTr.ListelemeBasarili);
        } 

        public IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(x => x.UnitPrice > min && x.UnitPrice < max), ResTr.ListelemeBasarili); 
        }

        public IDataResult<List<ViewProductDetail>> GetViewProductDetails()
        {
            return new SuccessDataResult<List<ViewProductDetail>>(_productDal.GetViewProductDetails(), ResTr.ListelemeBasarili); 
        }
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            _productDal.Update(product);

            return new SuccessResult("Update Başarılı");
        }
    }
}
