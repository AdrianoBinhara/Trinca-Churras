using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TrincaChurras.Views;
using Xamarin.Forms;

namespace TrincaChurras.ViewModels
{
    public class LoginViewModel
    {
        protected INavigation _navigation;
        public LoginViewModel(INavigation navigation)
        {
            _navigation = navigation;

            LoginCommand = new Command(async() =>await LoginAsync());
        }
        public ICommand LoginCommand { get; set; }

        private async Task LoginAsync()
        {
            await _navigation.PushAsync(new AgendaPage());
        }
    }
}
