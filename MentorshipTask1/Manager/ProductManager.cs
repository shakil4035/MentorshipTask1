using MentorshipTask1.DbFile;
using MentorshipTask1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MentorshipTask1.Manager
{
    public class ProductManager
    {
        public DbContextFile _dbcontext;

        public ProductManager()
        {
            _dbcontext = new DbContextFile();
        }

        public int Add(Product vm)
        {
            var product = new Product()
            {
                ProductId = vm.ProductId,
                ProductName = vm.ProductName,
                UnitPrice = vm.UnitPrice,
                NumStock = vm.NumStock
            };
            _dbcontext.Products.Add(product);
            var isSave = _dbcontext.SaveChanges();
            return isSave;
        }

        public IEnumerable<Product> GetDataApi6()
        {
            var products = _dbcontext.Products.Where(p=> p.NumStock >=100).ToList();
            return products;
        }

        public IEnumerable<Product> GetDataApi8()
        {
            var products = _dbcontext.Products.ToList(); 
            var orders = _dbcontext.Orders.Select(o => o.ProductId).ToList(); 

            var unorderedProducts = products
                .Where(p => !orders.Contains(p.ProductId))
                .ToList();

            return unorderedProducts;
        }

    }
}