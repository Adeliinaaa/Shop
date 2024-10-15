using Microsoft.AspNetCore.Mvc;
using Shop_Api.Controllers;
using Shop_Api.Data;
using Shop_Common.Dtos;
using Shop_Api.Models;
using Shop_Api.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Shop_Wpf.Views.Windows;
using Shop_Wpf.ViewModels.Pages;
using Shop_Api.Services.UserService;

namespace Shop_Wpf.Views.Pages
{
    public partial class RegistrationPage : Page
    {
        public RegistrationPage(IUserService userService)
        {
            InitializeComponent();
            DataContext = new RegistrationPageViewModel(userService);
        }
    }
}
