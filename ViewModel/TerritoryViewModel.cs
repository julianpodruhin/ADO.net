using Microsoft.EntityFrameworkCore;
using Northwind.DataBase;
using Northwind.Model;
using System;

namespace Northwind.ViewModel
{
    internal class TerritoryViewModel : BaseViewModel<Territory>
    {
        public TerritoryViewModel(string title, NorthwindDbContext context) 
            : base(title, context)
        {

        }

        protected override DbSet<Territory> Table => context.Territories;

        protected override void ExecuteAddItemCommand(object? parameter)
        {
            if (SelectedItem != null)
            {
                var item = (Territory)SelectedItem;
                item.TerritoryID = string.Empty;
            }
            base.ExecuteAddItemCommand(parameter);
        }

        protected override void ExecuteRemoveItemCommand(object? parameter)
        {
            if (SelectedItem != null)
            {
                var item = (Territory)SelectedItem;
                context.Territories.Remove(item);
                context.SaveChanges();
                Items.Remove(item);
            }
        }
    }
}
