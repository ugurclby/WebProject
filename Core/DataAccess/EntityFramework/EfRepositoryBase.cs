using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EfRepositoryBase<TEntity, TContext> : IRepositoryDal<TEntity> where TEntity : class, IEntity, new() where TContext : DbContext,new()
    {
        public void Add(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        } 

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public void Delete(IEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return filter == null ? context.Set<TEntity>().FirstOrDefault() : context.Set<TEntity>().Where(filter).FirstOrDefault();
            }
        }

        public IEntity Get(Expression<Func<IEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public List<IEntity> GetAll(Expression<Func<IEntity, bool>> filter = null)
        {
            throw new NotImplementedException();
        } 
        
        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        //public TEntity UpdateControl<T>(TEntity entity,int pkId)
        //{
        //    var resultProduct = Get(x=>x.);
        //    if (resultProduct.Data != null)
        //    {
        //        List<string> formcollist = new List<string>();
        //        foreach (var key in product.GetType().GetProperties())
        //        {
        //            var sonuc = key.GetValue(product, null).ToString();
        //            if (!String.IsNullOrEmpty(sonuc) && sonuc != "0")
        //            {
        //                formcollist.Add(key.Name);
        //            }
        //        }
        //        foreach (var prop in resultProduct.Data.GetType().GetProperties())
        //        {
        //            if (formcollist.Contains(prop.Name))
        //            {
        //                prop.SetValue(resultProduct.Data, product.GetType().GetProperty(prop.Name).GetValue(product, null));
        //            }
        //        }
        //    }

        //    return resultProduct.Data;
        //}
    }
}
