using Business.Abstract;
using ConstResource;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _CategoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _CategoryDal = categoryDal;
        }

        public IDataResult<List<Category>> GetAllCategory()
        { 
            return new SuccessDataResult<List<Category>>(_CategoryDal.GetAll(),ResTr.ListelemeBasarili);

        }

        public IDataResult<Category> GetCategory(int CategoryId)
        {
            if (CategoryId < 1)
            {
                return new ErrorDataResult<Category>("Hata aldığı durum!!");
            }
            return new SuccessDataResult<Category>(_CategoryDal.Get(x => x.CategoryID == CategoryId), ResTr.KayitGetirildi);
        }
    }
}
