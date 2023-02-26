using Microsoft.EntityFrameworkCore;
using Northwind.DataBase;
using Northwind.Model;
using System.Linq;

namespace Northwind.ViewModel
{
    internal class ProductViewModel : BaseViewModel<Product>
    {
        public ProductViewModel(string title, NorthwindDbContext context) 
            : base(title, context)
        {

        }

        protected override DbSet<Product> Table => context.Products;

        protected override void ExecuteAddItemCommand(object? parameter)
        {
            if (SelectedItem != null)
            {
                var item = (Product)SelectedItem;
                item.Id = 0;
            }
            base.ExecuteAddItemCommand(parameter);
        }

        protected override void ExecuteRemoveItemCommand(object? parameter)
        {
            if (SelectedItem != null)
            {
                var item = (Product)SelectedItem;
                var details = context.OrderDetails
                    .Where(od => od.ProductId == item.Id)
                    .ToArray();
                context.OrderDetails.RemoveRange(details);
                context.Products.Remove(item);
                context.SaveChanges();
                Items.Remove(item);
            }
        }
    }
}
