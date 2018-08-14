using System;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RecordStore.Extensions;
using RecordStore.Models;
using Newtonsoft.Json;
using TicTacToe.Services;

namespace RecordStore.Controllers
{
    public class CartController : Controller
    {
		private readonly IRecordRepository recordRepository;

		public CartController(IRecordRepository recordrepository)
		{
			this.recordRepository = recordrepository;
		}

		public IActionResult Index()
        {
			Cart cart = HttpContext.Session.Get<Cart>("cart");
			if (cart != null)
				return View(cart);
			return View(new Cart());
        }

		public IActionResult Add(int id, int count)
		{
			Cart cart = HttpContext.Session.Get<Cart>("cart");
			if(cart == null)
				cart = new Cart();
			Record record = recordRepository.GetById(id);
			record.Artist.Records = null;
			cart.Add(record, count);
			HttpContext.Session.Set<Cart>("cart", cart);
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public IActionResult Order()
		{
			Cart cart = HttpContext.Session.Get<Cart>("cart");
			if(cart == null)
				return RedirectToAction("List", "Records");
			return View(new OrderViewModel());
		}

		[HttpPost]
		public IActionResult Order(OrderViewModel order, [FromServices]IEmailService emailService)
		{
			Cart cart = HttpContext.Session.Get<Cart>("cart");
			emailService.SendEmail(order.Email, "Record Store", "Dear " + order.Name + " " + order.Surname + ", you ordered:" + Environment.NewLine + cart.ToString());
			return RedirectToAction("List", "Records");
		}

		[Produces("application/json")]
		[HttpDelete("api/DeleteItem/{id}")]
		public IActionResult Delete(int id)
		{
			Cart cart = HttpContext.Session.Get<Cart>("cart");
			if(cart == null)
				return BadRequest("Cart does not exist.");
			cart.Delete(id);
			HttpContext.Session.Set<Cart>("cart", cart);
			return Ok("success");
		}
    }
}