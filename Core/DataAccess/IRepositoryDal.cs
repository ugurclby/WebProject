using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    // where : Generic constraint demek
    // class : Referans tip olduğu belirlenir
    // IEntity tipinde bir referans tip istenir
    // New'lenebilir bir IEntity tipinde bir referans tip istenir

    public interface IRepositoryDal<T> where T:class, IEntity,new()
    {
        List<T> GetAll(Expression<Func<T,bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
