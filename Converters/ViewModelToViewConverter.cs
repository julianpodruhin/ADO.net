using Northwind.View;
using Northwind.ViewModel;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Northwind.Converters
{
    // Данный класс нужен для того, чтобы основываясь на типе модели сгенерировать для нее представление базы данных
    internal sealed class ViewModelToViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                EmployeeViewModel => new EmployeeUserControl() { DataContext = value },
                CategoryViewModel => new CategoryUserControl() { DataContext = value },
                CustomerViewModel => new CustomerUserControl() { DataContext = value },
                ProductViewModel => new ProductUserControl() { DataContext = value },
                OrderViewModel => new OrderUserControl() { DataContext = value },
                SupplierViewModel => new SupplierUserControl() { DataContext = value },
                TerritoryViewModel => new TerritoryUserControl { DataContext = value },
                _ => Binding.DoNothing
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
