using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;


namespace Lab08AMM
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnScan_Clicked(object sender, EventArgs e)
        {
            var scan = new ZXingScannerPage();
            await Lab08AMM.App.Current.MainPage.Navigation.PushModalAsync(scan);

            scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Lab08AMM.App.Current.MainPage.Navigation.PopModalAsync();

                    if (!string.IsNullOrEmpty(result.Text))
                        txtBarCode.Text = result.Text;

                });
            };
        }
    }
}
