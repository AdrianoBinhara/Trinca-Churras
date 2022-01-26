using System;
using System.Collections.Generic;
using TrincaChurras.ViewModels;
using Xamarin.Forms;

namespace TrincaChurras.Views
{
    public partial class ParticipantsPage : ContentPage
    {
        public ParticipantsPage(string id)
        {
            InitializeComponent();
            BindingContext = new ParticipantsViewModel(Navigation, id);
        }
    }
}
