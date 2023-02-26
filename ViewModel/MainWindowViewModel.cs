using Northwind.Model;
using System.Collections.Generic;
using System.ComponentModel;

namespace Northwind.ViewModel
{
    internal class MainWindowViewModel
    {
        public IReadOnlyCollection<INotifyPropertyChanged> ViewModels { get; }

        public INotifyPropertyChanged SelectedViewModel { get; set; } = null!;

        public MainWindowViewModel(IReadOnlyCollection<INotifyPropertyChanged> viewModels)
        {
            ViewModels = viewModels ?? new List<INotifyPropertyChanged>();
        }
    }
}
