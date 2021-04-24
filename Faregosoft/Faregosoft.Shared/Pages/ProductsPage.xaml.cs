using Faregosoft.Components;
using Faregosoft.Helpers;
using Faregosoft.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace Faregosoft.Pages
{
    public sealed partial class ProductsPage : Page
    {
        public ProductsPage()
        {
            InitializeComponent();
            LoadProductsAsync();
        }

        public ObservableCollection<Product> Products { get; set; }

        private async Task LoadProductsAsync()
        {
            Loader loader = new Loader("Por favor espere...");
            loader.Show();
            Response response = await ApiService.GetListAsync<Product>("https://localhost:44377/", "api", "Products");
            loader.Close();

            if (!response.IsSuccess)
            {
                MessageDialog dialog = new MessageDialog(response.Message, "Error");
                await dialog.ShowAsync();
                return;
            }

            List<Product> products = (List<Product>)response.Result;
            Products = new ObservableCollection<Product>(products);
            RefreshList();
        }

        private void RefreshList()
        {
            ProductsListView.ItemsSource = null;
            ProductsListView.Items.Clear();
            ProductsListView.ItemsSource = Products;
        }
    }
}
