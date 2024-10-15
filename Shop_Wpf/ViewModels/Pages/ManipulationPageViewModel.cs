using GalaSoft.MvvmLight.CommandWpf;
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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shop_Wpf.ViewModels.Pages
{
    internal class ManipulationPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string costAsString;
        private string quantityInStockAsString;
        private string currentDiscountAsString;
        private string maxDiscountAsString;
        private string error;
        private readonly IProductService _iProductService;
        public ObservableCollection<Manufacturer> Manufacturers { get; set; }
        public ObservableCollection<Supplier> Suppliers { get; set; }
        public ObservableCollection<Producttype> ProductTypes { get; set; }

        public ICommand DeleteCommand { get; private set; }
        public ICommand ApplyCommand { get; private set; }

        public ManipulationPageViewModel(IProductService iProductService)
        {
            _iProductService = iProductService;

            DeleteCommand = new RelayCommand(async () => await ApplyExecute());
            ApplyCommand = new RelayCommand(DeleteExecute);
            LoadData();
        }

        public string ProductArticleNumber
        {
            get ; private set;
        }
        public int Cost
        {
            get { return costAsString; }
            set
            {
                if (costAsString != value)
                {
                    costAsString = value;
                    OnPropertyChanged("Cost");
                }
            }
        }

        public string QuantityInStock
        {
            get { return quantityInStockAsString; }
            set
            {
                if (quantityInStockAsString != value)
                {
                    quantityInStockAsString = value;
                    OnPropertyChanged("QuantityInStock");
                }
            }
        }

        public string CurrentDiscount
        {
            get { return currentDiscountAsString; }
            set
            {
                if (CurrentDiscount != value)
                {
                    currentDiscountAsString = value;
                    OnPropertyChanged("CurrentDiscount");
                }
            }
        }

        public string MaxDiscount
        {
            get { return maxDiscountAsString; }
            set
            {
                if (MaxDiscount != value)
                {
                    maxDiscountAsString = value;
                    OnPropertyChanged("MaxDiscountAsString");
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
        private async void LoadData()
        {
            Manufacturers = new ObservableCollection<Manufacturer>(await _iProductService.GetManufacturers());
            Suppliers = new ObservableCollection<Supplier>(await _iProductService.GetSuppliers());
            ProductTypes = new ObservableCollection<Producttype>(await _iProductService.GetProductTypes());
        }

            private void DeleteExecute()
        {
            _iProductService.DeleteProduct(ProductArticleNumber);
            MainWindowViewModel.Frame.Content = new ViewProductsPage(_iProductService);
        }

        private async Task ApplyExecute()
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
            ((RelayCommand)ApplyCommand).RaiseCanExecuteChanged();
        }
    }
}
}
