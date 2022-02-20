using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //ProductTest();

            //NewMethod(); 
            //GetViewProductDetails();

            ProductManager pm = new ProductManager(new EfProductDal());

            var sonuc = pm.GetAll();

            if (sonuc.Success)
            {
                foreach (var item in sonuc.Data)
                {
                    Console.WriteLine(item.ProductID + " - " + item.ProductName);
                }
            }
            else
            {
                Console.WriteLine(sonuc.Message);
            }
            Console.ReadLine();
        }

        private static void GetViewProductDetails()
        {
            ProductManager productManager = new ProductManager(new EfProductDal());

            var sonuc = productManager.GetViewProductDetails();
            if (sonuc.Success)
            {
                foreach (var item in sonuc.Data)
                {
                    Console.WriteLine(item.ProductName + " " + item.CategoryName);
                }
            }
            else
            {
                Console.WriteLine(sonuc.Message);
            }
            
            Console.ReadLine();
        }

        private static void NewMethod()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());

            foreach (var item in categoryManager.GetAllCategory().Data)
            {
                Console.WriteLine(item.CategoryID + " " + item.CategoryName);
            }
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal());

            foreach (var item in productManager.GetAllByUnitPrice(10, 20).Data)
            {
                Console.WriteLine(item.ProductName + " " + item.UnitPrice);
            }
            Console.ReadLine();
        }
    }
}
