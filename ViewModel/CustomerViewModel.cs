using Microsoft.EntityFrameworkCore;
using Northwind.DataBase;
using Northwind.Model;
using System.Linq;

namespace Northwind.ViewModel
{
    internal class CustomerViewModel : BaseViewModel<Customer>
    {
        public CustomerViewModel(string title, NorthwindDbContext context) 
            : base(title, context)
        {
        }

        protected override DbSet<Customer> Table => context.Customers;

        protected override void ExecuteAddItemCommand(object? parameter)
        {
            if (SelectedItem != null)
            {
                var item = (Customer)SelectedItem;
                item.CustomerId = string.Empty;
            }
            base.ExecuteAddItemCommand(parameter);
        }

        protected override void ExecuteRemoveItemCommand(object? parameter)
        {
            if (SelectedItem != null)
            {
                var item = (Customer)SelectedItem;
                var orders = context.Orders
                    .Where(o => o.CustomerId == item.CustomerId)
                    .ToArray();
                foreach (var order in orders)
                {
                    item.Orders.Remove(order);
                }
                var demos = context.CustomerCustomerDemo
                    .Where(cd => cd.CustomerId == item.CustomerId)
                    .ToArray();
                context.CustomerCustomerDemo.RemoveRange(demos);
                context.Customers.Remove(item);
                context.SaveChanges();
                Items.Remove(item);
            }
        }
    }
}
