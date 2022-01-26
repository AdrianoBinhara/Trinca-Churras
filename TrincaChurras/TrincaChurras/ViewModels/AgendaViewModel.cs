using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TrincaChurras.Interfaces;
using TrincaChurras.Models;
using TrincaChurras.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TrincaChurras.ViewModels
{
    public class AgendaViewModel : BaseViewModel
    {
        protected TrincaMiddleware trincaMiddleware = new TrincaMiddleware();
        protected INavigation _navigation;

        private ObservableCollection<Barbecue> _listaAgenda;
        public ObservableCollection<Barbecue> ListaAgenda
        {
            get { return _listaAgenda; }
            set { SetProperty(ref _listaAgenda, value); }
        }

        public AgendaViewModel(INavigation navigation)
        {
            _navigation = navigation;

            SelectedChurrasCommand = new Command<CollectionView>(async (churras) => await SelectChurrasAsync(churras));

            Task.Run(async () =>
            {
                await LoadItem();
            });
        }

        public ICommand SelectedChurrasCommand { get; set; }

        private async Task SelectChurrasAsync(CollectionView churras)
        {
            if(churras.SelectedItem != null)
            {
                var selectedChurras = churras.SelectedItem as Barbecue;
                await _navigation.PushAsync(new ParticipantsPage(selectedChurras.Id));
                churras.SelectedItem = null;
            }
        }

        private async Task LoadItem()
        {
            IsBusy =! IsBusy;

            var token = await SecureStorage.GetAsync("token");
            var response = await trincaMiddleware.GetBarbecue(token);

            ListaAgenda = new ObservableCollection<Barbecue>(response.Data);
            ListaAgenda.Insert(ListaAgenda.Count,new Barbecue { Title = "Adicionar Churras", Image = "icon_bbq.png", });

            IsBusy = !IsBusy;
        }
    }
}
