using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TrincaChurras.Views;
using Xamarin.Forms;
using Xamarin.Essentials;
using TrincaChurras.Models;
using TrincaChurras.Interfaces;

namespace TrincaChurras.ViewModels
{
    public class LoginViewModel :BaseViewModel
    {
        protected INavigation _navigation;
        protected TrincaMiddleware trincaMiddleware = new TrincaMiddleware();

        public string Username { get; set; }
        public string Password { get; set; }

        public LoginViewModel(INavigation navigation)
        {
            _navigation = navigation;

            LoginCommand = new Command(async() =>await LoginAsync());
        }
        public ICommand LoginCommand { get; set; }

        private async Task LoginAsync()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                return;

            var hasToken = await GetToken();
            if (!hasToken)
                return;

            await _navigation.PushAsync(new AgendaPage());
        }

        private async Task<bool> GetToken()
        {
            IsBusy = !IsBusy;
            try
            {                
                var response = await trincaMiddleware.PostUser(new  {Username,Password });
                if (!response.Sucess)
                {
                    await App.Current.MainPage.DisplayAlert("Atenção", response.Message, "OK");
                    return false;
                }

                SecureStorage.RemoveAll();
                await SecureStorage.SetAsync("token", response.Message);
                return true;
             
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                IsBusy = !IsBusy;
            }
        }
    }
}
