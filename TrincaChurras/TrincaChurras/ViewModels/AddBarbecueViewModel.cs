using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using TrincaChurras.Interfaces;
using TrincaChurras.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TrincaChurras.ViewModels
{
    public class AddBarbecueViewModel : BaseViewModel
    {
        protected INavigation _navigation;
        protected TrincaMiddleware trincaMiddleware = new TrincaMiddleware();

        private ObservableCollection<Person> _participantsList = new ObservableCollection<Person>();
        public ObservableCollection<Person> ParticipantsList
        {
            get { return _participantsList; }
            set
            {
                SetProperty(ref _participantsList, value);
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value);
            }
        }

        private double _paidValue = 0.0;
        public double PaidValue
        {
            get { return _paidValue; }
            set
            {
                SetProperty(ref _paidValue, value);
            }
        }

        private bool _isPresent = false;
        public bool IsPresent
        {
            get { return _isPresent; }
            set
            {
                SetProperty(ref _isPresent, value);
            }
        }

        private string _totalParticipants = 0.ToString();
        public string TotalParticipants
        {
            get { return _totalParticipants; }
            set { SetProperty(ref _totalParticipants, value); }
        }

        public string Title { get; set; }
        public DateTime Date { get; set; }
        public double ValuePerPeson { get; set; }
        public Person Participant { get; private set; }
        public string ChurrasId { get; private set; }

        private int id = 0;

        public AddBarbecueViewModel(INavigation navigation)
        {
            _navigation = navigation;

            AddPariticipantsCommand = new Command(() => AddParticipantAsync());
            SelectedItemCommand = new Command<CollectionView>((collection) => SelectedItemAsync(collection));
            BackButtonCommand = new Command(async () => await BackButtonAsync());
            SaveParticipantCommand = new Command(() => SaveParticipantAsync());
        }

        public ICommand AddPariticipantsCommand { get; set; }
        public ICommand BackButtonCommand { get; set; }
        public ICommand SelectedItemCommand { get; set; }
        public ICommand SaveParticipantCommand { get; set; }


        private void AddParticipantAsync()
        {
            id++;
            ParticipantsList.Add(new Person {Id = id.ToString() });
            TotalParticipants = ParticipantsList.Count.ToString();
        }

        private void SelectedItemAsync(CollectionView collection)
        {
            var selected = (Person)collection.SelectedItem;

            if (selected == null)
                return;

            Participant = selected;
            Name = Participant.Name;
            PaidValue = Participant.Value_paid ==  0? 1: Participant.Value_paid;
            IsPresent = Participant.Confirmed ??false;
        }

        private void SaveParticipantAsync()
        {
            Person person = new Person
            {
                Id = Participant.Id,
                Name = this.Name,
                Value_paid = PaidValue == 0 ? 0.0 : this.PaidValue,
                Confirmed = this.IsPresent
            };
            var index = ParticipantsList.IndexOf(Participant);
            ParticipantsList.Remove(Participant);
            ParticipantsList.Insert(index, person);
        }

        private async Task BackButtonAsync()
        {
            if (Date == null || ParticipantsList == null || Title == null)
            {
                await _navigation.PopAsync();
                return;
            }

            IsBusy = !IsBusy;

            var token = await SecureStorage.GetAsync("token");

            var barbecue = new Barbecue
            {
                Date = this.Date.ToString("yyyy-MM-dd"),
                Description = " ",
                Title = this.Title,
                Value_per_person = this.ValuePerPeson == 0? 1: ValuePerPeson,
                Participants = new List<Person>(ParticipantsList)

            };

            var result =  await trincaMiddleware.PostBarbecure(barbecue, token);
            if (!result.Sucess)
            {
                UserDialogs.Instance.Toast("A criação falhou, verifique os dados preenchidos");
                IsBusy = !IsBusy;
                return;
            }
            
            IsBusy = !IsBusy;
        }
    }
}
