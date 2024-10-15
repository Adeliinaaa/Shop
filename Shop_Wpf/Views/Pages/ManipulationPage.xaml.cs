using Shop_Api.Services.ProductService;
using Shop_Wpf.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Shop_Wpf.Views.Pages
{
    public partial class ManipulationPage : Page
    {
        public ManipulationPage()
        {
            InitializeComponent();
            DataContext = new ManipulationPageViewModel();
        }
    }
}
