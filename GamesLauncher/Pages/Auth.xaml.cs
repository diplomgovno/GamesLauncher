using System.Windows.Controls;

namespace GamesLauncher.Pages
{
    /// <summary>
    /// Interaction logic for Auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        public Auth()
        {
            InitializeComponent();
            BTNLogIn.Click += (s, e) =>
            {
                if (TBLogin.Text == "admin" && PBPassword.Password == "admin")
                {
                    Thread.Sleep(300);
                    NavigationService.Navigate(new Main());
                }
            };
        }
    }
}
