using System;
using System.Collections.Generic;
using TrincaChurras.Models;
using TrincaChurras.ViewModels;
using Xamarin.Forms;

namespace TrincaChurras.Views
{
    public partial class AgendaPage : ContentPage
    {
        public AgendaPage()
        {
            InitializeComponent();
            BindingContext = new AgendaViewModel(Navigation);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Send<Page>(this, "UpdateList");
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Page>(this, "UpdateList");
        }
    }
}
