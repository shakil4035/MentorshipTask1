using MentorshipTask1.DbFile;
using MentorshipTask1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MentorshipTask1.Manager
{
    public class OrderManager
    {
        public DbContextFile _dbContext;

        public OrderManager()
        {
            _dbContext = new DbContextFile();
        }

        public int Add(Order vm)
        {

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var getProduct = _dbContext.Products.FirstOrDefault(c => c.ProductId == vm.ProductId);
                    if (getProduct == null || getProduct.NumStock < vm.Quantity)
                    {
                        return 0; 
                    }

                    var order = new Order
                    {
                        OrderId = vm.OrderId,
                        ProductId = vm.ProductId,
                        CustomerName = vm.CustomerName,
                        Quantity = vm.Quantity,
                        OrderDate = DateTime.Now
                    };
                    _dbContext.Orders.Add(order);

                    getProduct.NumStock -= vm.Quantity;

                    _dbContext.SaveChanges(); 
                    transaction.Commit();

                    return order.OrderId;
                }
                catch
                {
                    transaction.Rollback(); 
                    throw; 
                }
            }



        }

        public int Update(int orderId, Order vm)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var getProduct = _dbContext.Products.FirstOrDefault(c => c.ProductId == vm.ProductId);
                    var getOrder = _dbContext.Orders.FirstOrDefault(c => c.OrderId == vm.OrderId);
                    if (getProduct == null || getProduct.NumStock+getOrder.Quantity < vm.Quantity)
                    {
                        return 0;
                    }


                    getProduct.NumStock += getOrder.Quantity;
                    getProduct.NumStock -= vm.Quantity;
                    getOrder.Quantity = vm.Quantity;
                    _dbContext.SaveChanges();
                    transaction.Commit();

                    return getOrder.OrderId;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }

            //var entity = _dbContext.Orders.SingleOrDefault(c => c.OrderId == vm.OrderId);
            //entity.OrderId = vm.OrderId;
            //entity.ProductId = vm.ProductId;
            //entity.CustomerName = vm.CustomerName;
            //entity.Quantity = vm.Quantity;
            //entity.OrderDate = vm.OrderDate;
            //var isUpdate = _dbContext.SaveChanges();
            //return isUpdate;
        }

        public IEnumerable<Order> GetAll()
        {
            var orders = _dbContext.Orders
                                   .Include("Product")
                                   .ToList();
            return orders;
        }
        public IEnumerable<OrderViewModel> GetData()
        {
            var orders = _dbContext.Orders
                                    .Include("Product")
                                   .ToList();

            var orderViewModels = new List<OrderViewModel>();

            foreach (var order in orders)
            {
                var orderViewModel = new OrderViewModel
                {
                    OrderId = order.OrderId,
                    ProductId=order.ProductId,
                    ProductName = order.Product?.ProductName,  
                    Quantity = order.Quantity,
                    CustomerName = order.CustomerName,
                    OrderDate = order.OrderDate,
                    UnitPrice=order.Product.UnitPrice,
                    NumStock= order.Product.NumStock,
                };

                orderViewModels.Add(orderViewModel);
            }

            return orderViewModels;  
        }


        public IEnumerable<ViewModelApi5> GetDataApi5()
        {
            var orders = _dbContext.Orders
                                   .Include("Product") 
                                   .ToList();

            var groupedOrders = orders
                                .GroupBy(o => o.ProductId)  
                                .Select(g => new ViewModelApi5
                                {
                                    ProductId = g.Key,  
                                    ProductName = g.FirstOrDefault().Product?.ProductName,  
                                    TotalQuantity = g.Sum(o => o.Quantity),  
                                    Revenue = g.Sum(o => o.Quantity * o.Product.UnitPrice)  
                                })
                                .ToList();

            return groupedOrders;
        }

        public IEnumerable<CustomerSummary> GetApi7()
        {
            var orders = _dbContext.Orders.ToList(); 

            var topCustomers = orders
                .GroupBy(o => o.CustomerName)
                .Select(group => new CustomerSummary
                {
                    CustomerName = group.Key,
                    TotalQuantity = group.Sum(o => o.Quantity)
                })
                .OrderByDescending(x => x.TotalQuantity) 
                .Take(3) 
                .ToList();

            return topCustomers;
        }

        public int GetApi9(List<Order> orders)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var order in orders)
                    {
                        // Validate stock availability
                        var product = _dbContext.Products.FirstOrDefault(p => p.ProductId == order.ProductId);
                        if (product == null || product.NumStock < order.Quantity)
                        {
                            return 0;
                        }
                        else
                        {
                            product.NumStock -= order.Quantity;
                            var o = new Order
                            {
                                ProductId = order.ProductId,
                                CustomerName = order.CustomerName,
                                Quantity = order.Quantity,
                                OrderDate = DateTime.Now
                            };

                            _dbContext.Orders.Add(o);
                        }
                    }
                    _dbContext.SaveChanges();
                    transaction.Commit();

                    return 1;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Transaction failed: {ex.Message}");
                    return 0;
                }
            }
        }


        public Order Get(int orderId)
        {
            var order = _dbContext.Orders.SingleOrDefault(c => c.OrderId == orderId);
            return order;
        }
        
        public int Remove(int orderId)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var getOrder = _dbContext.Orders.FirstOrDefault(c => c.OrderId == orderId);
                    if (getOrder == null)
                    {
                        return 0;
                    }
                    var getProduct = _dbContext.Products.FirstOrDefault(c => c.ProductId == getOrder.ProductId);

                    getProduct.NumStock += getOrder.Quantity;
                    _dbContext.Orders.Remove(getOrder);
                    var isRemove = _dbContext.SaveChanges();
                    transaction.Commit();

                    return isRemove;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }

            //var order = _dbContext.Orders.SingleOrDefault(c => c.OrderId == orderId);
            //_dbContext.Orders.Remove(order);
            //var isRemove = _dbContext.SaveChanges();
            //return isRemove;
        }
    }
}