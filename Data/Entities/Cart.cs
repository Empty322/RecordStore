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
			Products.Add(new CartLine { Record = record, Amount = amount });
		}

		public void Delete(int id)
		{
			Products.Remove(Products.FirstOrDefault(p => p.Record.RecordId == id));
		}

		public void Clear()
		{
			Products.Clear();
		}
    }

	public class CartLine
	{
		public Record Record { get; set; }
		public int Amount { get; set; }	
	}
}