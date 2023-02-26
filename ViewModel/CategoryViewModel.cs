using Microsoft.EntityFrameworkCore;
using Northwind.DataBase;
using Northwind.Model;
using System.Linq;

namespace Northwind.ViewModel
{
    internal class CategoryViewModel : BaseViewModel<Category>
    {
        protected override DbSet<Category> Table => context.Categories;

        public CategoryViewModel(string title, NorthwindDbContext context) 
            : base(title, context)
        {
        }

        protected override void ExecuteAddItemCommand(object? parameter)
        {
            if (SelectedItem != null)
            {
                var item = (Category)SelectedItem;
                item.Id = 0;
            }
            base.ExecuteAddItemCommand(parameter);
        }

        protected override void ExecuteRemoveItemCommand(object? parameter)
        {
            if (SelectedItem != null)
            {
                var item = (Category)SelectedItem;
                var products = context.Products
                    .Where(p => p.CategoryId == item.Id)
                    .ToArray();
                foreach (var product in products)
                {
                    item.Products.Remove(product);
                }
                context.Categories.Remove(item);
                context.SaveChanges();
                Items.Remove(item);
            }
        }
    }
}
