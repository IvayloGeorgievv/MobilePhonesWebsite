using MobilePhonesWebsite.Data;
using MobilePhonesWebsite.Models;
using MobilePhonesWebsite.ViewModels.OrderVM;
using MobilePhonesWebsite.ViewModels.PhoneCaseVM;
using System.Data.Entity;

namespace MobilePhonesWebsite.Repository
{
    public class OrderRepository
    {
        static readonly ApplicationDbContext applicationDbContext;
        public static List<Order> Items = new List<Order>();

        static OrderRepository()
        {
            applicationDbContext = new ApplicationDbContext();
            Items = applicationDbContext.Orders.ToList();
        }
        public async Task AddOrderAsync(Order item)
        {
            Order order = new Order();
            order.UserId = item.UserId;
            order.OrderNum = GetLastAddedOrderNumber() + 1;
            order.FirstAndLastName = item.FirstAndLastName;
            order.PhoneNumber = item.PhoneNumber;
            order.DateOrdered = item.DateOrdered;
            order.ProductsId = item.ProductsId;
            order.ProductsTypes = item.ProductsTypes;
            order.ProductsQuantities = item.ProductsQuantities;
            order.OrderStatus = item.OrderStatus;
            order.ShippingAddress = item.ShippingAddress;
            order.ShippingMethod = item.ShippingMethod;
            order.OrderTotalPrice = item.OrderTotalPrice;

            applicationDbContext.Orders.Add(order);
            await applicationDbContext.SaveChangesAsync();
        }
        public async Task<List<Order>> GetByUserIdAsync(int userId)
        {
            List<Order> AllOrders = await applicationDbContext.Orders.ToListAsync();
            List<Order> Orders = new List<Order>();
            foreach (var item in AllOrders)
            {
                if (item.UserId == userId)
                {
                    Orders.Add(item);
                }
            }
            return Orders;
        }
        public void UpdateOrder(EditOrderVM item)
        {
            Order order = applicationDbContext.Orders.Find(item.Id);

            if (order != null)
            {
               order.OrderStatus = item.OrderStatus;

                applicationDbContext.Entry(order).State = EntityState.Modified;
                applicationDbContext.SaveChanges();
            }
        }
        public int GetLastAddedOrderNumber()
        {
            try
            {
                var lastOrder = applicationDbContext.Orders.OrderByDescending(x => x.Id).FirstOrDefault();

                return lastOrder != null ? lastOrder.OrderNum : 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task CancelOrder(int id)
        {
            Order order = applicationDbContext.Orders.Find(id);
            if(order != null)
            {
                order.OrderStatus = Enumerators.OrderEnum.OrderStatusEnum.Отменена;
                applicationDbContext.Entry(order).State = EntityState.Modified;
                applicationDbContext.SaveChanges();
            }
        }
    }
}
