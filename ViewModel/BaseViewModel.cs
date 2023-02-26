using Microsoft.EntityFrameworkCore;
using Northwind.DataBase;
using Northwind.Infrastructure;
using Northwind.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Northwind.ViewModel
{
    // Базовый класс для все моделей данныч
    // Содержит в себе команды по добавлению элементов в базу данных,
    // по изменению даных в базе и по их удалению
    internal abstract class BaseViewModel<TEnitity> : INotifyPropertyChanged
        where TEnitity : Entity, new()
    {
        public virtual event PropertyChangedEventHandler? PropertyChanged;
        
        protected readonly NorthwindDbContext context;

        // Таблица базы данных, которая будет использоваться для работы
        protected abstract DbSet<TEnitity> Table { get; }

        // Команда, которая загружает данные их базы и выводит их на экран
        public ICommand ShowAllItemsCommand { get; }

        // Команда, которая очищает поля с информацией об элементе в базе данных
        public ICommand ClearDataCommand { get; }

        // Команда, которая добавляет элемент в базу данных
        public ICommand AddItemCommand { get; }

        // Команда, которая удаляет элемент из базы данных
        public ICommand RemoveItemCommand { get; }

        public ICommand UpdateItemCommand { get; }

        // Элемент, который в данных момент обрабатывается.
        private Entity? item = new TEnitity();
        public Entity? SelectedItem
        {
            get => item;
            set 
            { 
                item = value; 
                PropertyChanged?.Invoke(this, new (nameof(SelectedItem))); 
            }
        }

        // Данные из базы данных
        public ObservableCollection<Entity> Items { get; } = new();

        public string Title { get; }

        protected BaseViewModel(string title, NorthwindDbContext context)
        {
            Title = title;
            this.context = context;
            ShowAllItemsCommand = new SafeRelayCommand(new RelayCommand(ExecuteShowAllItemsCommand));
            AddItemCommand = new SafeRelayCommand(new RelayCommand(ExecuteAddItemCommand));
            RemoveItemCommand = new SafeRelayCommand(new RelayCommand(ExecuteRemoveItemCommand));
            UpdateItemCommand = new SafeRelayCommand(new RelayCommand(ExecuteUpdateItemCommand));
            ClearDataCommand = new SafeRelayCommand(new RelayCommand(ExecuteClearDataCommand));
        }

        protected virtual void ExecuteClearDataCommand(object? parameter)
        {
            SelectedItem = null;
            SelectedItem = new TEnitity();
        }

        // В данном методе происходит добавление элементов из базы данных на экран
        private void FetchData()
        {
            Items.Clear();
            foreach (var item in Table)
            {
                Items.Add(item);
            }
        }

        protected virtual void ExecuteShowAllItemsCommand(object? parameter)
        {            
            FetchData();
        }

        protected virtual void ExecuteAddItemCommand(object? parameter)
        {
            // Здесь происходит добавление элементов в базу данных
            if (SelectedItem != null)
            {
                var item = (TEnitity)SelectedItem;
                Table.Add(item);
                context.SaveChanges();
                FetchData();
            }
        }

        protected abstract void ExecuteRemoveItemCommand(object? parameter);

        // В данном методе происходит удаление элемента и всех его связей из базы данных
        protected virtual void ExecuteUpdateItemCommand(object? parameter)
        {
            if (SelectedItem != null)
            {
                var item = (TEnitity)SelectedItem;
                Table.Update(item);
                context.SaveChanges();
            }
        }
    }
}
