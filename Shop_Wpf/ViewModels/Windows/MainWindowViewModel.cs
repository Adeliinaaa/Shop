using Shop_Api.Data;
using Shop_Api.Repositories;
using Shop_Api.Repositories.UserRepository;
using Shop_Api.Services;
using Shop_Api.Services.UserService;
using Shop_Wpf.Views.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Shop_Wpf.ViewModels.Windows
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static Frame Frame;

        private object _currentPage;

        public object CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPage)));
            }
        }

        public ICommand NavigateCommand { get; private set; }

        public MainWindowViewModel(Frame frame)
        {
            Frame = frame;
            CurrentPage = new AuthorizationPage(new UserService(
                new UserRepository(new ShopApiDatabaseContext())));
        }
    }
}
