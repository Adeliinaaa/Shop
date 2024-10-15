using Shop_Api.Services.UserService;
using Shop_Wpf.ViewModels.Pages;
using System.Windows.Controls;

namespace Shop_Wpf.Views.Pages
{
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage(IUserService iUserService)
        {
            InitializeComponent();
            DataContext = new AuthorizationPageViewModel(iUserService);
        }
    }
}
