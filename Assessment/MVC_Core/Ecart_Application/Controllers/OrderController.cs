using Microsoft.AspNetCore.Mvc;
using Ecart_Application.Repository;
using Ecart_Application.Models;
using System;
using System.Collections.Generic;

namespace Ecart_Application.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IActionResult Index()
        {
            // Get all orders
            var orders = _orderRepository.GetOrders();
            return View(orders);
        }

        public IActionResult Details(int id)
        {
            var order = _orderRepository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        public IActionResult OrdersByDate(DateTime? orderDate)
        {
            var orders = _orderRepository.GetOrdersByDate(orderDate);
            return View(orders);
        }

        public IActionResult OrdersByCustomer(string customerId)
        {
            var orders = _orderRepository.GetOrdersByCustomer(customerId);
            return View(orders);
        }

        public IActionResult HighestOrder()
        {
            var highestOrder = _orderRepository.GetHighestOrder();
            return View(highestOrder);
        }
      
        [HttpGet] // Specify HTTP method
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _orderRepository.Create(order);
                    // After creating the order, retrieve all orders again
                    var orders = _orderRepository.GetOrders();
                    return View("Index", orders);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred while creating the order: {ex.Message}");
                }
            }
            return View(order);
        }



    }
}
