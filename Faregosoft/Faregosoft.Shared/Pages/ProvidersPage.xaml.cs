using Faregosoft.Components;
using Faregosoft.Dialogs;
using Faregosoft.Helpers;
using Faregosoft.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Faregosoft.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProvidersPage : Page
    {
        public ProvidersPage()
        {
            InitializeComponent();
            LoadCustomersAsync();
        }

        public ObservableCollection<Provider> Providers { get; set; }

        private async void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            Provider provider = new Provider();
            ProviderDialog dialog = new ProviderDialog(provider);
            await dialog.ShowAsync();
            if (!provider.WasSaved)
            {
                return;
            }

            Loader loader = new Loader("Por favor espere...");
            loader.Show();
            Response response = await ApiService.PostAsync(Settings.GetApiUrl(), "api", "Customers", provider, MainPage.GetInstance().Token.Token);
            loader.Close();

            if (!response.IsSuccess)
            {
                MessageDialog messageDialog = new MessageDialog(response.Message, "Error");
                await messageDialog.ShowAsync();
                return;
            }

            Provider newProvider = (Provider)response.Result;
            Providers.Add(newProvider);
            Providers = new ObservableCollection<Provider>(Providers.OrderBy(c => c.FirstName).ToList());
            RefreshList();
        }

        private async void EditImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //Customer customer = Providers[CustomersListView.SelectedIndex];
            //customer.IsEdit = true;
            //CustomerDialog dialog = new CustomerDialog(customer);
            //await dialog.ShowAsync();

            //if (!customer.WasSaved)
            //{
            //    return;
            //}

            //Loader loader = new Loader("Por favor espere...");
            //loader.Show();
            //Response response = await ApiService.PutAsync(Settings.GetApiUrl(), "api", "Customers", customer, customer.Id, MainPage.GetInstance().Token.Token);
            //loader.Close();

            //if (!response.IsSuccess)
            //{
            //    MessageDialog messageDialog = new MessageDialog(response.Message, "Error");
            //    await messageDialog.ShowAsync();
            //    return;
            //}

            //Customer newCustomer = (Customer)response.Result;
            //Customer oldCustomer = Providers.FirstOrDefault(c => c.Id == newCustomer.Id);
            //oldCustomer = newCustomer;
            //RefreshList();
        }

        private async void DeleteImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //ContentDialogResult result = await ConfirmDeleteAsync();
            //if (result != ContentDialogResult.Primary)
            //{
            //    return;
            //}

            //Loader loader = new Loader("Por favor espere...");
            //loader.Show();
            //Customer customer = Providers[ProvidersListView.SelectedIndex];
            //Response response = await ApiService.DeleteAsync<Customer>(Settings.GetApiUrl(), "api", "Customers", customer.Id, MainPage.GetInstance().Token.Token);
            //loader.Close();

            //if (!response.IsSuccess)
            //{
            //    MessageDialog messageDialog = new MessageDialog(response.Message, "Error");
            //    await messageDialog.ShowAsync();
            //    return;
            //}

            //List<Customer> customers = Providers.Where(c => c.Id != customer.Id).ToList();
            //Providers = new ObservableCollection<Provider>(Providers);
            //RefreshList();
        }
        private async Task LoadCustomersAsync()
        {
            TokenResponse token = MainPage.GetInstance().Token;
            if (token.Expiration.ToLocalTime() < DateTime.Now)
            {
                MessageDialog dialog = new MessageDialog("Su sesión ha expirado.", "Error");
                await dialog.ShowAsync();
                MainPage.GetInstance().LogOut();
                return;
            }

            Loader loader = new Loader("Por favor espere...");
            loader.Show();
            Response response = await ApiService.GetListAsync<Provider>(Settings.GetApiUrl(), "api", "Providers", MainPage.GetInstance().Token.Token);
            loader.Close();

            if (!response.IsSuccess)
            {
                MessageDialog dialog = new MessageDialog(response.Message, "Error");
                await dialog.ShowAsync();
                return;
            }

            List<Provider> providers = (List<Provider>)response.Result;
            Providers = new ObservableCollection<Provider>(providers);
            RefreshList();
        }
        private void RefreshList()
        {
            ProvidersListView.ItemsSource = null;
            ProvidersListView.Items.Clear();
            ProvidersListView.ItemsSource = Providers;
        }

        private async Task<ContentDialogResult> ConfirmDeleteAsync()
        {
            ContentDialog confirmDialog = new ContentDialog
            {
                Title = "Confirmación",
                Content = "¿Estas seguro de querer borrar el registro?",
                PrimaryButtonText = "Si",
                CloseButtonText = "No"
            };

            return await confirmDialog.ShowAsync();
        }
    }
}

