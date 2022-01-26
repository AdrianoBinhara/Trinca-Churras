using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TrincaChurras.Interfaces;
using TrincaChurras.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TrincaChurras.ViewModels
{
    public class ParticipantsViewModel: BaseViewModel
    {
        protected INavigation _navigation;
        protected TrincaMiddleware trincaMiddleware = new TrincaMiddleware();

        private ObservableCollection<Person> _participantsList;
        public ObservableCollection<Person> ParticipantsList
        {
            get { return _participantsList; }
            set
            {
                SetProperty(ref _participantsList, value);
            }
        }
        private Barbecue _churras;
        public Barbecue Churras
        {
            get { return _churras; }
            set { SetProperty(ref _churras, value); }
        }

        private int _totalParticipants;
        public int TotalParticipants
        {
            get { return _totalParticipants; }
            set
            {
                SetProperty(ref _totalParticipants, value);
            }
        }

        private double _totalValue;
        public double TotalValue
        {
            get { return _totalValue; }
            set
            {
                SetProperty(ref _totalValue, value);
            }
        }


        public ParticipantsViewModel(INavigation navigation, string churrasId)
        {
            _navigation = navigation;

            Task.Run(async () =>
            {
                await GetBarbecue(churrasId);
            });

            BackCommand = new Command(async () => await NavigateBackAsync());
        }

        public ICommand BackCommand { get; set; }

        private async Task GetBarbecue(string churrasId)
        {
            var token    = await SecureStorage.GetAsync("token");

            var response = await trincaMiddleware.GetBarbecueById(churrasId, token);

            Churras = response.Data;
            ParticipantsList = new ObservableCollection<Person>(response.Data.Participants);
            TotalParticipants = Churras.Participants.Count;

            foreach (var item in Churras.Participants)
            {
                TotalValue += item.Value_paid;
            }
        }

        private async Task NavigateBackAsync()
        {
            await _navigation.PopAsync();
        }
    }
}
