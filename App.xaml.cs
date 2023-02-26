using Northwind.DataBase;
using Northwind.Model;
using Northwind.ViewModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Windows;

namespace Northwind
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var connectionString = ConfigurationManager
                .ConnectionStrings["Connection"]
                .ConnectionString; // строка подключения к базе данных
            var context = new NorthwindDbContext(connectionString);
            // Здесь создается список моделей, которые будут использоваться в базе данных
            // Именно эти модели появляются в списке на главном окне
            var viewModels = new List<INotifyPropertyChanged>()
            {
                 new EmployeeViewModel("Employees", context),
                 new CategoryViewModel("Categories", context),
                 new CustomerViewModel("Customers", context),
                 new ProductViewModel("Products", context),
                 new OrderViewModel("Orders", context),
                 new SupplierViewModel("Suppliers", context),
                 new TerritoryViewModel("Territories", context),
            };
            var mainWindowViewModel = new MainWindowViewModel(viewModels);
            var window = new MainWindow() { DataContext = mainWindowViewModel };
            window.Closing += (s, e) => context.Dispose();
            window.Show();
            base.OnStartup(e);
        }
    }
}
