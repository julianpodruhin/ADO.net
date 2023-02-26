using Microsoft.EntityFrameworkCore;
using Northwind.DataBase;
using Northwind.Model;
using System.Linq;

namespace Northwind.ViewModel
{
    internal class OrderViewModel : BaseViewModel<Order>
    {
        public OrderViewModel(string title, NorthwindDbContext context) 
            : base(title, context)
        {
           
        }

        protected override DbSet<Order> Table => context.Orders;

        protected override void ExecuteAddItemCommand(object? parameter)
        {
            if (SelectedItem != null)
            {
                var item = (Order)SelectedItem;
                item.Id = 0;
            }
            base.ExecuteAddItemCommand(parameter);
        }

        protected override void ExecuteRemoveItemCommand(object? parameter)
        {
            if (SelectedItem != null)
            {
                var item = (Order)SelectedItem;
                var details = context.OrderDetails
                    .Where(od => od.Id == item.Id)
                    .ToArray();
                context.OrderDetails.RemoveRange(details);
                context.Orders.Remove(item);
                context.SaveChanges();
                Items.Remove(item);
            }
        }
    }
}
