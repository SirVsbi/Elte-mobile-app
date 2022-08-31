using CryTogether.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CryTogether.ViewModels
{
    [QueryProperty("ItemId", "itemId")]
    public class ItemDetailViewModel : BaseViewModel
    {

        private string text;
        private string description;
        private Breakdown breakdown;

        public Breakdown item
        {
            get => breakdown;
            set => SetProperty(ref breakdown, value);
        }

        public string Id { get; set; }


        public ItemDetailViewModel()
        {
             LoadItemId(Uri.EscapeDataString("itemId"));
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        string itemId;
        public string ItemId
        {
            get
            {
                return ItemId;
            }
            set
            {
                itemId = Uri.EscapeDataString(value);
                LoadItemId(itemId);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                breakdown = item;
                Id = item.Id;
                Text = breakdown.Name;
                Description = item.Description;
                this.item = item;

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
