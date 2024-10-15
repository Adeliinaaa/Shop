using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.AspNetCore.Mvc;
using Shop_Api.Controllers;
using Shop_Api.Data;
using Shop_Api.Models;
using Shop_Api.Repositories;
using Shop_Api.Repositories.ProductRepository;
using Shop_Api.Services.ProductService;
using Shop_Api.Services.UserService;
using Shop_Common.Dtos;
using Shop_Wpf.ViewModels.Windows;
using Shop_Wpf.Views.Pages;
using Shop_Wpf.Views.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shop_Wpf.ViewModels.Pages
{
    internal class RegistrationPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private UserRegistrationDto _userRegistrationDto;
        private readonly IUserService _iUserService;

        public ICommand AuthorizationCommand { get; private set; }
        public ICommand RegistrationCommand { get; private set; }

        private string retypePassword;
        private string error;

        public RegistrationPageViewModel(IUserService iUserService)
        {
            RegistrationCommand = new RelayCommand(async () => await RegistrationExecute());
            AuthorizationCommand = new RelayCommand(AuthorizationExecute);

            _iUserService = iUserService;
            _userRegistrationDto = new UserRegistrationDto("", "", "", "", "");
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

        public string Login
        {
            get { return _userRegistrationDto.Login; }
            set
            {
                if (_userRegistrationDto.Login != value)
                {
                    _userRegistrationDto.Login = value;
                    OnPropertyChanged("Login");
                }
            }
        }

        public string Password
        {
            get { return _userRegistrationDto.Password; }
            set
            {
                if (_userRegistrationDto.Password != value)
                {
                    _userRegistrationDto.Password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        public string RetypePassword
        {
            get { return retypePassword; }
            set
            {
                if (retypePassword != value)
                {
                    retypePassword = value;
                    OnPropertyChanged("RetypePassword");
                }
            }
        }

        public string Name
        {
            get { return _userRegistrationDto.Name; }
            set
            {
                if (_userRegistrationDto.Name != value)
                {
                    _userRegistrationDto.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public string Surname
        {
            get { return _userRegistrationDto.Surname ?? ""; }
            set
            {
                if (_userRegistrationDto.Surname != value)
                {
                    _userRegistrationDto.Surname = value;
                    OnPropertyChanged("Surname");
                }
            }
        }
        public string Patronymic
        {
            get { return _userRegistrationDto.Patronymic ?? ""; }
            set
            {
                if (_userRegistrationDto.Patronymic != value)
                {
                    _userRegistrationDto.Patronymic = value;
                    OnPropertyChanged("Patronymic");
                }
            }
        }

        private void AuthorizationExecute()
        {
            MainWindowViewModel.Frame.Content = new AuthorizationPage(_iUserService);
        }

        private async Task RegistrationExecute()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Error = "Имя не может быть пустым";
                return;
            }
            if (Name.Length < 3)
            {
                Error = "Имя не может быть короче 3х символов";
                return;
            }
            if (Name.Length > 20)
            {
                Error = "Имя не может быть длинее 20 символов";
                return;
            }
            if (Surname.Length < 3)
            {
                Error = "Фамилия не может быть короче 3х символов";
                return;
            }
            if (Surname.Length > 50)
            {
                Error = "Фамилия не может быть длинее 50 символов";
                return;
            }
            if (Patronymic.Length < 3 && Patronymic.Length != 0)
            {
                Error = "Отчество не может быть короче 3х символов";
                return;
            }
            if (Patronymic.Length > 50)
            {
                Error = "Отчество не может быть длинее 50 символов";
                return;
            }
            if (!Regex.IsMatch(Login, @"^login[a-zA-Z]{5}[\d!@#$%^&*]{4}$"))
            {
                Error = "Логин должен начинаться со слова 'login', далее 5 букв и 4 символа (цифры или специальные символы).";
                return;
            }
            else if (!Regex.IsMatch(Password, @"^(?=.*[A-Z])(?=.*[a-z].*[a-z])(?=.*\d.*\d)(?=.*[!@#$%^&*]).{6,}$"))
            {
                Error = "Пароль должен содержать минимум 6 символов, 1 заглавную букву, 2 строчные буквы, 2 цифры и 1 специальный символ.";
                return;
            }
            if (Password.Length > 100)
            {
                Error = "Пароль не может быть длинее 100 символов";
                return;
            }
            if (Password != RetypePassword)
            {
                Error = "Пароли не совпадают";
                return;
            }

            switch (await new UserController(_iUserService).Registration(_userRegistrationDto))
            {
                case CreatedAtActionResult createdAtActionResult:
                    User user = createdAtActionResult.Value as User;
                    IProductService productService = new ProductService(new ProductRepository(new ShopApiDatabaseContext()));
                    MainWindowViewModel.Frame.Content = new ViewProductsPage(productService);
                    break;

                case BadRequestObjectResult badRequestResult:
                    Error = badRequestResult.Value?.ToString();
                    break;
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            ((RelayCommand)RegistrationCommand).RaiseCanExecuteChanged();
        }
    }
}
