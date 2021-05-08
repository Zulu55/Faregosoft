using Faregosoft.Helpers;
using Faregosoft.Models;
using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Faregosoft.Dialogs
{
    public sealed partial class ProviderDialog : ContentDialog
    {
        public ProviderDialog(Provider provider)
        {
            InitializeComponent();
            Provider = provider;
            if (Provider.IsEdit)
            {
                TitleTextBlock.Text = $"Editar Proveedor: {Provider.FullName}";
            }
            else
            {
                TitleTextBlock.Text = "Nuevo Proveedor";
            }
        }

        public Provider Provider { get; set; }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private async void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = await ValidateFormAsync();
            if (!isValid)
            {
                return;
            }

            Provider.WasSaved = true;
            Hide();
        }

        private async Task<bool> ValidateFormAsync()
        {
            MessageDialog messageDialog;

            if (string.IsNullOrEmpty(Provider.FirstName))
            {
                messageDialog = new MessageDialog("Debes ingresar nombres del cliente.", "Error");
                await messageDialog.ShowAsync();
                return false;
            }

            if (string.IsNullOrEmpty(Provider.LastName))
            {
                messageDialog = new MessageDialog("Debes ingresar apellidos del cliente.", "Error");
                await messageDialog.ShowAsync();
                return false;
            }

           
            if (string.IsNullOrEmpty(Provider.Address))
            {
                messageDialog = new MessageDialog("Debes ingresar dirección del cliente.", "Error");
                await messageDialog.ShowAsync();
                return false;
            }
  
            return true;
        }

        private void CloseImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Hide();
        }
    }
}