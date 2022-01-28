using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TrincaChurras.Views;
using Xamarin.Forms;
using Xamarin.Essentials;
using TrincaChurras.Models;
using TrincaChurras.Interfaces;
using Acr.UserDialogs;

namespace TrincaChurras.ViewModels
{
    public class LoginViewModel :BaseViewModel
    {
        #region Properties
        protected INavigation _navigation;
        protected TrincaMiddleware trincaMiddleware = new TrincaMiddleware();

        public string Username { get; set; }
        public string Password { get; set; }
        #endregion

        #region Constructor
        public LoginViewModel(INavigation navigation)
        {
            _navigation = navigation;

            LoginCommand = new Command(async() =>await LoginAsync());
        }
        #endregion

        #region Commands
        public ICommand LoginCommand { get; set; }
        #endregion

        #region Methods
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
                    UserDialogs.Instance.Toast( "Usuário não autenticado");
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
        #endregion
    }
}
