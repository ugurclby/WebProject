using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Core.CrossCuttinConcerns.Validation;
using Core.Aspects.Autofac.Validation;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Core.Aspects.Transaction;
using Core.Utilities.Business;
namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Ürünlerin tamamının çekildiği method
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")] 
        public IActionResult GetAll()
        {

            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        /// <summary>
        /// Tek ürün çekildiği method
        /// </summary>
        /// <returns></returns>
        //[HttpGet("{id}")] // bu şekilde yapıldığı zaman https://localhost:44368/api/products/79 istek böyle olur
        [HttpGet("GetById")] //bu şekilde yapıldığı zaman https://localhost:44368/api/products/GetById?id=79 istek böyle olur
        public IActionResult GetById(int id)
        { 
            var result = _productService.Get(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        /// <summary>
        /// Ürünlerin kayıt edildiği method
        /// </summary>
        /// <returns></returns>
        [HttpPost("Add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Created("", result);
            }
            return BadRequest(result);
        }
        /// <summary>
        /// Ürünlerin update edildiği method
        /// </summary>
        /// <returns></returns>
        [HttpPost("Update")]
        public IActionResult Update(Product product)
        {
            if (product.ProductID > 0)
            {

                var sonuc = UpdateControl(product);
                //resultProduct.Data.CategoryID = product.CategoryID;
                //resultProduct.Data.ProductName = product.ProductName;
                //resultProduct.Data.UnitPrice = product.UnitPrice;
                //resultProduct.Data.UnitsInStock = product.UnitsInStock;
                var resultUpdate = _productService.Update(sonuc);
                return Ok(resultUpdate);
            }
            return BadRequest("Kayıt yok");
        }
        /// <summary>
        /// Ürünlerin update dilmeden önceki kontrolü
        /// </summary>
        /// <returns></returns>
        public Product UpdateControl(Product product)
        {
            var resultProduct = _productService.Get(product.ProductID);
            if (resultProduct.Data != null)
            {
                List<string> formcollist = new List<string>();
                foreach (var key in product.GetType().GetProperties())
                {
                    var sonuc = key.GetValue(product, null).ToString();
                    if (!String.IsNullOrEmpty(sonuc) && sonuc !="0")
                    {
                        formcollist.Add(key.Name);
                    } 
                }
                foreach (var prop in resultProduct.Data.GetType().GetProperties())
                {
                    if (formcollist.Contains(prop.Name))
                    {
                        prop.SetValue(resultProduct.Data, product.GetType().GetProperty(prop.Name).GetValue(product, null));
                    }
                }
            }

            return resultProduct.Data; 

        }

    }
}
