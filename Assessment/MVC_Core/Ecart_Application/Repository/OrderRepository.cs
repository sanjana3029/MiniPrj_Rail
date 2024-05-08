using Ecart_Application.Models;
using System;
using System.Collections.Generic;

namespace Ecart_Application.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private List<Order> _orders; // Assuming orders are stored in-memory in a list

        public OrderRepository()
        {
            // Initialize the list of orders (can be replaced with database access)
            _orders = new List<Order>();
        }

        public void Create(Order order)
        {
            // Add the order to the list of orders
            _orders.Add(order);
        }

        public Order GetOrderById(int orderId)
        {
            // Find and return the order by its ID
            return _orders.Find(o => o.OrderId == orderId);
        }

        public List<Order> GetOrdersByDate(DateTime? orderDate)
        {
            if (orderDate.HasValue)
            {
                // Filter orders by the specified date and return the list
                //return _orders.FindAll(o => o.OrderDate.Date == orderDate.Value.Date);
                return _orders.FindAll(o => o.OrderDate.HasValue && o.OrderDate.Value.Date == orderDate.Value.Date);

            }
            else
            {
                // If orderDate is null, return an empty list or handle it as per your requirement
                return new List<Order>();
            }
        }

        public List<Order> GetOrdersByCustomer(string customerId)
        {
            // Filter orders by the specified customer ID and return the list
            return _orders.FindAll(o => o.Customer.CustomerId == customerId);
        }

        public Order GetHighestOrder()
        {
            // Find and return the order with the highest order ID
            return _orders.Count > 0 ? _orders[_orders.Count - 1] : null;
        }
        public IEnumerable<Order> GetOrders()
        {
            // Return all orders
            return _orders.ToList();
        }
    }
}
