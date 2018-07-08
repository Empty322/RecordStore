using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RecordStore.Extensions;

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

		[Produces("application/json")]
		[HttpPost("/api/AddToCart/{id}")]
		public IActionResult Add(int id)
		{
			Cart cart = HttpContext.Session.Get<Cart>("cart");
			if(cart == null)
				cart = new Cart();
			Record record = recordRepository.GetById(id);
			record.Artist.Records = null;
			cart.Add(record, 1);
			HttpContext.Session.Set<Cart>("cart", cart);
			return Ok("success");
		}
    }
}