using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lab08AMM
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new BatteryDemo();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
