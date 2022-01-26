using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TrincaChurras.Interfaces;
using TrincaChurras.Models;
using Xamarin.Forms;

namespace TrincaChurras.ViewModels
{
    public class AgendaViewModel : BaseViewModel
    {
        private TrincaMiddleware trincaMiddleware = new TrincaMiddleware();
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
            Task.Run(async () =>
            {
                await LoadItem();
            });
        }

        private async Task LoadItem()
        {
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
            var response = await trincaMiddleware.GetBarbecue(token);
            ListaAgenda = new ObservableCollection<Barbecue>(response.Data);
            ListaAgenda.Insert(ListaAgenda.Count,new Barbecue { Title = "Adicionar Churras", Image = "icon_bbq.png", });
        }
    }
}
