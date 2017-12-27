using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using SWArchitecture.Models;

namespace SWArchitecture.UserControls
{
    /// <summary>
    /// Interaction logic for UserControl2.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public SystemUser User = null;
        public Login()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text;
            string password = textBoxPass.Text;
            var entities = new ApplicationDbContext();
            var user = entities.SystemUsers.FirstOrDefault(c => (c.Login==login&&c.Password==password));
            if (user != null)
            {
                var mw = ((MainWindow)Application.Current.MainWindow);
                MainWindow.User = user;
                mw.MainGrid.ColumnDefinitions[0].Width = new GridLength(128);
                MainWindow.Facade.ShowTests(new MainWindow.Frame());
                mw.ShowFrame(new Button(){Name = "Testing" },null);
               //((MainWindow)((Grid)Parent).Parent).label123.Content = user.Login;

            }
            else
            {
                MessageBox.Show("No such user!", "Error", MessageBoxButton.OK);
            }
        }
    }
}
