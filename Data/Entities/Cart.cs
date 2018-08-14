using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Entities
{
    public class Cart
    {
		public List<CartLine> Products { get; private set; }

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
			Products.Remove(Products.FirstOrDefault(p => p.Record.RecordId == id));
		}

		public void Clear()
		{
			Products.Clear();
		}

		public override string ToString()
		{
			StringBuilder result = new StringBuilder();
			foreach(CartLine line in Products)
				result.Append(line.Record.Artist.Name + " - " + line.Record.Title + " x " + line.Amount + "\n");
			return result.ToString();
		}
	}

	public class CartLine
	{
		public Record Record { get; set; }
		public int Amount { get; set; }	
	}
}