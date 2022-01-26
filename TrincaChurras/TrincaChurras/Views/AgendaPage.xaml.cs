using System;
using System.Collections.Generic;
using TrincaChurras.Models;
using Xamarin.Forms;

namespace TrincaChurras.Views
{
    public partial class AgendaPage : ContentPage
    {

        List<Barbecue> BarbecueList = new List<Barbecue>();

        public AgendaPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BarbecueList = new List<Barbecue>
            {
                new Barbecue
                {
                    Title = "Churrasco",
                    Description = "Lorem ipsum asd",
                    Date = "2020-07-18 21:00:00",
                    Value_per_person = 23.4,
                    Participants_count = 12
                },
                new Barbecue
                {
                    Title = "Final de ano",
                    Description = "Lorem ipsum asd",
                    Date = "2020-07-18 21:00:00",
                    Value_per_person = 33.4,
                    Participants_count = 15
                },
                new Barbecue
                {
                    Title = "Aniversario do Gui",
                    Description = "Lorem ipsum asd",
                    Date = "2020-07-18 21:00:00",
                    Value_per_person = 29.4,
                    Participants_count = 18
                }
            };
            BarbecueList.Insert(BarbecueList.Count , new Barbecue { Title = "Adicionar Churras", Image = "icon_bbq.png", });
            collectionBarb.ItemsSource = BarbecueList;  
        }
    }
}
