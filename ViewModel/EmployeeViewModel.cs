using Microsoft.EntityFrameworkCore;
using Northwind.DataBase;
using Northwind.Model;
using System.Linq;

namespace Northwind.ViewModel
{
    internal class EmployeeViewModel : BaseViewModel<Employee>
    {
        public EmployeeViewModel(string title, NorthwindDbContext context) 
            : base(title, context)
        {

        }

        protected override DbSet<Employee> Table => context.Employees;

        protected override void ExecuteAddItemCommand(object? parameter)
        {
            if (SelectedItem != null)
            {
                var item = (Employee)SelectedItem;
                item.Id = 0;
            }
            base.ExecuteAddItemCommand(parameter);
        }

        protected override void ExecuteRemoveItemCommand(object? parameter)
        {
            if (SelectedItem != null)
            {
                var item = (Employee)SelectedItem;
                var territories = context.EmployeeTerritories
                    .Where(t => t.EmployeeId == item.Id)
                    .ToArray();
                var orders = context.Orders
                    .Where(o => o.EmployeeId == item.Id)
                    .ToArray();
                foreach (var order in orders)
                {
                    item.Orders.Remove(order);
                }
                context.EmployeeTerritories.RemoveRange(territories);
                context.Employees.Remove(item);
                context.SaveChanges();
                Items.Remove(item);
            }
        }
    }
}
