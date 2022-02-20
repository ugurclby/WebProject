using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dto
{
    public class ProductDetail:IDto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } 
    }
}
