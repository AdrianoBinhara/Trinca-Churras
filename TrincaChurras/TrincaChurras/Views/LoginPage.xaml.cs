using System;
using System.Collections.Generic;
using TrincaChurras.ViewModels;
using Xamarin.Forms;

namespace TrincaChurras.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(Navigation);
        }
    }
}
