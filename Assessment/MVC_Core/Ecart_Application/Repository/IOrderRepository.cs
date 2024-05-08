using Ecart_Application.Models;
using System.Collections.Generic; 


namespace Ecart_Application.Repository
{
    public interface IOrderRepository
    {
        void Create(Order order);
        Order GetOrderById(int orderId);
        List<Order> GetOrdersByDate(DateTime? orderDate);
        List<Order> GetOrdersByCustomer(string customerId);
        Order GetHighestOrder();
        IEnumerable<Order> GetOrders();


    }
}
