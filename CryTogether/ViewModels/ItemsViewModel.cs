using CryTogether.Models;
using CryTogether.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CryTogether.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Breakdown _selectedItem;

        public ObservableCollection<Breakdown> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }

        public Command LoadAllItemsCommand { get; }
        public Command<Breakdown> ItemTapped { get; }

        public ItemsViewModel()
        {
            Title = "Browse The Misery Catalog";
            Items = new ObservableCollection<Breakdown>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoadAllItemsCommand = new Command(async () => await ExecuteLoadAllItemsCommand());
            ItemTapped = new Command<Breakdown>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }




        public async Task ExecuteLoadAllItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.RefreshDataAsync();
                foreach (var item in items)
                {
                      Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }


        public async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.RefreshDataAsync();
                foreach (var item in items)
                {
                        Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Breakdown SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync("NewItemPage");
        }

        public async void OnItemSelected(Breakdown item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?={item.Id}");
        }


        public async Task DeleteItem(string id)
        {
            await DataStore.DelteDataAsync(id);
            await this.ExecuteLoadItemsCommand();
        }

    }
}