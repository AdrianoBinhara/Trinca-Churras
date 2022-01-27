using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
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

        private string token;
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value);
            }
        }

        private double _paidValue;
        public double PaidValue
        {
            get { return _paidValue; }
            set
            {
                SetProperty(ref _paidValue, value);
            }
        }

        private bool _isPresent;
        public bool IsPresent
        {
            get { return _isPresent; }
            set
            {
                SetProperty(ref _isPresent, value);
            }
        }

        public Person Participant { get; private set; }
        public string ChurrasId { get; private set; }


        public ParticipantsViewModel(INavigation navigation, string churrasId)
        {
            _navigation = navigation;
            ChurrasId = churrasId;
            Task.Run(async () =>
            {
                await GetBarbecue();
            });

            AddPariticipantsCommand = new Command(() => AddParticipantAsync());
            BackCommand = new Command(async () => await NavigateBackAsync());
            SelectedItemCommand = new Command<CollectionView>( (collection) =>  SelectedItemAsync(collection));
            SaveParticipantCommand = new Command(async () => await SaveParticipantAsync());
        }

        public ICommand AddPariticipantsCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand SelectedItemCommand { get; set; }
        public ICommand SaveParticipantCommand { get; set; }

        private void AddParticipantAsync()
        {
            ParticipantsList.Add(new Person());
            
        }

        private async Task GetBarbecue()
        {
            IsBusy =! IsBusy;

            TotalValue = 0;
            token = await SecureStorage.GetAsync("token");

            var response = await trincaMiddleware.GetBarbecueById(ChurrasId, token);

            if (response.Data == null)
            {
                UserDialogs.Instance.Toast("Houve um erro ao exibir seu churras.");
                return;
            }

            Churras = response.Data;
            ParticipantsList = new ObservableCollection<Person>(response.Data.Participants.OrderBy(x=>x.Name).ToList());
            TotalParticipants = Churras.Participants.Count;

            foreach (var item in Churras.Participants)
            {
                TotalValue += item.Value_paid;
            }
            IsBusy = !IsBusy;
        }

        private async Task SaveParticipantAsync()
        {
            IsBusy = !IsBusy;
            if(Participant.Id ==null)
            {
                Person person = new Person
                {
                    Bbq_id = Churras.Id,
                    Name = this.Name,
                    Value_paid = this.PaidValue,
                    Confirmed = this.IsPresent
                };
                await trincaMiddleware.PostParticipants(person, token);
            }
            else
            {
                Person person = new Person
                {
                    Id = Participant.Id,
                    Bbq_id = Churras.Id,
                    Name = this.Name,
                    Value_paid = this.PaidValue,
                    Confirmed = this.IsPresent
                };
                await trincaMiddleware.PutParticipant(person, token);
            }
            
            await GetBarbecue();
            IsBusy = !IsBusy;
        }

        private void SelectedItemAsync(CollectionView collection)
        {
            var selected =(Person)collection.SelectedItem;

            if (selected == null)
                return;

            Participant = selected;
            Name = Participant.Name;
            PaidValue = Participant.Value_paid;
            IsPresent = Participant.Confirmed;
        }

        private async Task NavigateBackAsync()
        {
            await _navigation.PopAsync();
        }
    }
}
