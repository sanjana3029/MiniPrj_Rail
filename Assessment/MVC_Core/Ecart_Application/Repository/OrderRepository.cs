using Ecart_Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecart_Application.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly NorthwindContext _context;

        public OrderRepository(NorthwindContext context)
        {
            _context = context;
        }
        public void PlaceOrder(Order order)
        {
            //    _context.Orders.Add(order);
            //    _context.SaveChanges();
            //}
            if (_context.Customers.Any(c => c.CustomerId == order.CustomerId))
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            else
            {
                // Handle the case when the provided customer ID does not exist
                throw new ArgumentException("Invalid CustomerID");
            }
        }

        public Order GetOrderById(int orderId)
        {
            //throw new NotImplementedException();
            return _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
        }
   
        public List<Order> GetOrdersByDate(DateTime? orderDate)
        {
            // throw new NotImplementedException();
            //return _context.Orders.Where(o => o.OrderDate.Date == orderDate.Date).ToList();
            if (orderDate.HasValue)
            {
                return _context.Orders.Where(o => o.OrderDate.HasValue && o.OrderDate.Value.Date == orderDate.Value.Date).ToList();
            }
            else
            {
                // Handle the case when orderDate is null
                // For example, return all orders if orderDate is not specified
                return _context.Orders.ToList();
            }
        }

        public List<Order> GetOrdersByCustomer(string customerId)
        {
            return _context.Orders.Where(o => o.CustomerId == customerId).ToList();
        }
        public Order GetHighestOrder()
        {
            //return _context.Orders.OrderByDescending(o => o.Total).FirstOrDefault();
            throw new NotImplementedException();
        }
    }
}
