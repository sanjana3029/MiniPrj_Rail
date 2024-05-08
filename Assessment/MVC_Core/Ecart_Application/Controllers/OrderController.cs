// OrderController.cs
using Microsoft.AspNetCore.Mvc;
using Ecart_Application.Repository; // Adjust the namespace as per your project structure
using Ecart_Application.Models; // Adjust the namespace as per your project structure
using System;

public class OrderController : Controller
{
    private readonly IOrderRepository _orderRepository;

    public OrderController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public IActionResult Index()
    {
        // Implement action logic here if needed
        return View();
    }

    public IActionResult PlaceOrder()
    {
        // Implement logic for placing order
        return View();
    }

    [HttpPost]
    public IActionResult PlaceOrder(Order order)
    {
        if (ModelState.IsValid)
        {
            _orderRepository.PlaceOrder(order);
            return RedirectToAction(nameof(Index));
        }
        return View(order);
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

    public IActionResult OrdersByDate(DateTime orderDate)
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
}
