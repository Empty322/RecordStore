using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Entities
{
    public class Cart
    {
		public List<CartLine> Products { get; private set; }

		public float Total {
			get {
				return GetTotal();
			}
		}

		public Cart()
		{
			Products = new List<CartLine>();
		}

		public void Add(Record record, int amount)
		{
			CartLine findedLine = Products.FirstOrDefault(c => c.Record.RecordId == record.RecordId);
			if(findedLine == null)
				Products.Add(new CartLine { Record = record, Amount = amount });
			else
				findedLine.Amount += amount;
		}

		public void Delete(int id)
		{
			CartLine product = Products.FirstOrDefault(p => p.Record.RecordId == id);
			Products.Remove(product);
		}

		public void Clear()
		{
			Products.Clear();
		}

		private float GetTotal()
		{
			float sum = 0;
			foreach(CartLine product in Products)
				sum += product.Record.Price * product.Amount;
			return sum;
		}

		public override string ToString()
		{
			StringBuilder result = new StringBuilder();
			foreach(CartLine line in Products)
				result.Append(line.Record.Artist.Name + " - " + line.Record.Title + " " + line.Record.Price + " x " + line.Amount + "\n");
			result.Append("Total price: " + Total + "\n");
			return result.ToString();
		}
	}

	public class CartLine
	{
		public Record Record { get; set; }
		public int Amount { get; set; }	
	}
}