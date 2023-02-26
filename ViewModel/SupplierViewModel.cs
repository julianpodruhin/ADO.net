using Microsoft.EntityFrameworkCore;
using Northwind.DataBase;
using Northwind.Model;
using System.Linq;

namespace Northwind.ViewModel
{
    internal class SupplierViewModel : BaseViewModel<Supplier>
    {
        public SupplierViewModel(string title, NorthwindDbContext context) 
            : base(title, context)
        {
        }

        protected override DbSet<Supplier> Table => context.Suppliers;

        protected override void ExecuteAddItemCommand(object? parameter)
        {
            if (SelectedItem != null)
            {
                var item = (Supplier)SelectedItem;
                item.Id = 0;
            }
            base.ExecuteAddItemCommand(parameter);
        }

        protected override void ExecuteRemoveItemCommand(object? parameter)
        {
            if(SelectedItem!= null)
            {
                var item = (Supplier)SelectedItem;
                var products = context.Products
                    .Where(p => p.SupplierId == item.Id)
                    .ToArray();
                foreach (var product in products)
                {
                    item.Products.Remove(product);
                }
                context.Suppliers.Remove(item);
                context.SaveChanges();
                Items.Remove(item);
            }
        }
    }
}
