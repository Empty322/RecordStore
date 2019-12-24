using System;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RecordStore.Extensions;
using RecordStore.Models;
using Newtonsoft.Json;
using TicTacToe.Services;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;

namespace RecordStore.Controllers
{
    public class CartController : Controller
    {
		private readonly IRecordRepository recordRepository;
		private readonly IOrderRepository orderRepository;
		private readonly YandexOptions options;

		public CartController(IRecordRepository recordRepository, IOrderRepository orderRepository, IOptions<YandexOptions> options)
		{
			this.recordRepository = recordRepository;
			this.orderRepository = orderRepository;
			this.options = options.Value;
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
			return View(new ContactsViewModel());
		}

		[HttpPost]
		public IActionResult Order(ContactsViewModel order)
		{
			if(ModelState.IsValid)
			{
				Cart cart = HttpContext.Session.Get<Cart>("cart");
				if(cart == null)
					return RedirectToAction("List", "Records");
				Order newOrder = new Order() {
					Name = order.Name,
					Surname = order.Surname,
					Email = order.Email,
					Adress = order.Address,
					Sum = cart.Total,
					Id = Guid.NewGuid().ToString(),
					Cart = cart.ToString()
				};
				orderRepository.Create(newOrder);
				return RedirectToAction(nameof(Payment), new { orderId = newOrder.Id, sum = newOrder.Sum });
			}
			else
			{
				return View(order);
			}
		}

		[HttpGet]
		public IActionResult Payment(string orderId, int sum)
		{
			return View(new OrderViewModel() { OrderId = orderId, Sum = sum });
		}

		[HttpPost]
		public IActionResult PaymentSuccessfull(string notification_type, string operation_id, string label, string datetime,
			decimal amount, decimal withdraw_amount, string sender, string sha1_hash, string currency, bool codepro, 
			[FromServices]IEmailService emailService)
		{
			string key = options.Secret;

			string paramString = String.Format("{0}&{1}&{2}&{3}&{4}&{5}&{6}&{7}&{8}",
				notification_type, operation_id, amount, currency, datetime, sender,
				codepro.ToString().ToLower(), key, label);
			string paramStringHash1 = GetHash(paramString);

			StringComparer comparer = StringComparer.OrdinalIgnoreCase;

			if(comparer.Compare(paramStringHash1, sha1_hash) == 0)
			{
				Order order = orderRepository.GetById(label);
				order.Operation_Id = operation_id;
				order.Date = DateTime.Now;
				order.Amount = amount;
				order.WithdrawAmount = withdraw_amount;
				order.Sender = sender;
				orderRepository.Update(order);

				emailService.SendEmail(order.Email, "Record Store", "Dear " + order.Name + " " + order.Surname + ", you have ordered:" + Environment.NewLine + order.Cart);
				return Ok();
			}
			return BadRequest();
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

		public string GetHash(string val)
		{
			SHA1 sha = new SHA1CryptoServiceProvider();
			byte[] data = sha.ComputeHash(Encoding.Default.GetBytes(val));

			StringBuilder sBuilder = new StringBuilder();

			for(int i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}
			return sBuilder.ToString();
		}
    }

	public class YandexOptions
	{
		public string Secret { get; set; }
	}
}