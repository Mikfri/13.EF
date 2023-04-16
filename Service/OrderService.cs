using ItemRazorV1.Models;

namespace ItemRazorV1.Service
{
    public class OrderService
    {

        public DbGenericService<Order> _dbGenericService;
        public List<Order> OrderList { get; set; }

        public OrderService(DbGenericService<Order> dbGenericService)
        {
            _dbGenericService = dbGenericService;
            OrderList = new List<Order>();
        }


        public void AddOrder(Order order)
        {
            OrderList.Add(order);
            _dbGenericService.AddObjectAsync(order);
        }

    }
}
