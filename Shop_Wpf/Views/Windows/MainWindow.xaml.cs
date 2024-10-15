using Shop_Api.Data;
using Shop_Api.Repositories;
using Shop_Api.Services;
using Shop_Wpf.ViewModels.Windows;
using Shop_Wpf.Views.Pages;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Shop_Wpf.Views.Windows
{
    public partial class MainWindow : Window
    {
        public static Frame? Frame;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel(MainFrame);
        }
    }
}