using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TrincaChurras.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AgendaPage());
        }
    }
}
