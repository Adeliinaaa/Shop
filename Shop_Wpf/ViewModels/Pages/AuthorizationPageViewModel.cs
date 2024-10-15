using GalaSoft.MvvmLight.Command;
using Microsoft.AspNetCore.Mvc;
using Shop_Api.Controllers;
using Shop_Api.Data;
using Shop_Api.Models;
using Shop_Api.Repositories.ProductRepository;
using Shop_Api.Services.ProductService;
using Shop_Api.Services.UserService;
using Shop_Common.Dtos;
using Shop_Wpf.ViewModels.Windows;
using Shop_Wpf.Views.Pages;
using Shop_Wpf.Views.Windows;
using System.ComponentModel;
using System.Windows.Input;

namespace Shop_Wpf.ViewModels.Pages
{
    internal class AuthorizationPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string login;
        private string password;
        private string error;
        private readonly IUserService _iUserService;
        public ICommand AuthorizationCommand { get; private set; }
        public ICommand RegistrationCommand { get; private set; }

        public AuthorizationPageViewModel(IUserService iUserService)
        {
            _iUserService = iUserService;
            AuthorizationCommand = new RelayCommand(async () => await AuthorizationExecute());
            RegistrationCommand = new RelayCommand(RegistrationExecute);
        }

        public string Login
        {
            get { return login; }
            set
            {
                if (login != value)
                {
                    login = value;
                    OnPropertyChanged("Login");
                }
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        public string Error
        {
            get { return error; }
            set
            {
                if (error != value)
                {
                    error = value;
                    OnPropertyChanged("Error");
                }
            }
        }

        private void RegistrationExecute()
        {
            MainWindowViewModel.Frame.Content = new RegistrationPage(_iUserService);
        }

        private async Task AuthorizationExecute()
        {
            if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
            {
                Error = "Поля должны быть заполнены";
                return;
            }

            switch (await new UserController(_iUserService).Authorization(new UserAuthorizationDto(login, password)))
            {
                case OkObjectResult okResultResponse:
                    User user = okResultResponse.Value as User;
                    IProductService productService = new ProductService(new ProductRepository(new ShopApiDatabaseContext()));
                    MainWindowViewModel.Frame.Content = new ViewProductsPage(productService);
                    return;

                case UnauthorizedObjectResult unauthorizedResult:
                    Error = unauthorizedResult.Value?.ToString();
                    return;
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            ((RelayCommand)AuthorizationCommand).RaiseCanExecuteChanged();
        }
    }
}