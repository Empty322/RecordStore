using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Order
    {
		[Key]
		public string Id { get; set; } // id заказа

		public DateTime? Date { get; set; } // дата

		public float Sum { get; set; } // сумма заказа

		public string Sender { get; set; } // отправитель - кошелек в ЯД

		public string Operation_Id { get; set; } // id операции в ЯД

		public decimal? Amount { get; set; } // сумма, которую заплатали с учетом комиссии

		public decimal? WithdrawAmount { get; set; } // сумма, которую заплатали без учета комиссии

		public string Email { get; set; } // почта заказчика

		public string Adress { get; set; } // адрес заказчика

		public string Name { get; set; } // имя заказчика

		public string Surname { get; set; } // фамилия заказчика

		public string Cart { get; set; } // корзина
	}
}
