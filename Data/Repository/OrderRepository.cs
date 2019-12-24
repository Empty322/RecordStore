using Data.Entities;
using Data.Interfaces;

namespace Data.Repository
{
	public class OrderRepository : Repository<Order, string>, IOrderRepository
	{
		public OrderRepository(ApplicationDbContext db) : base(db)
		{

		}
	}
}
