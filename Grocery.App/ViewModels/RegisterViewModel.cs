
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.App.ViewModels
{
    public partial class RegisterViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        private readonly GlobalViewModel _global;

        [ObservableProperty]
        private string name = "";
        
        [ObservableProperty]
        private string email = "";

        [ObservableProperty]
        private string password = "";
        
        [ObservableProperty]
        private string passwordRepetition = "";
        
        [ObservableProperty]
        private string registerMessage;

        public RegisterViewModel(IAuthService authService, GlobalViewModel global)
        { //_authService = App.Services.GetServices<IAuthService>().FirstOrDefault();
            _authService = authService;
            _global = global;
        }

        [RelayCommand]
        private void Register()
        {
            Client? authenticatedClient = _authService.Register(Name, Email, Password);
            
            if (authenticatedClient != null && Password == PasswordRepetition)
            {
                RegisterMessage = $"Welkom nieuwe gebruiker {authenticatedClient.Name}!";
                _global.Client = authenticatedClient;
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                RegisterMessage = "Ongeldige registratiegegevens";
            }
        }
    }
}
