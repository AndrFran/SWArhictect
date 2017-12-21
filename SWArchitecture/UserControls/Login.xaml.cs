using System.Linq;
using System.Windows;
using System.Windows.Controls;
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

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text;
            string password = textBoxPass.Password;
            ApplicationDbContext entities = new ApplicationDbContext();
            //var user = entities.SystemUsers.FirstOrDefault(c => c.login==Login || c.password==Password);
            //if(user==null)
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text;
            string password = textBoxPass.Password;
            var entities = new ApplicationDbContext();
            var user = entities.SystemUsers.FirstOrDefault(c => (c.Login==login&&c.Password==password));
            
            if (user != null)
            {
                ((MainWindow)((Grid)Parent).Parent).ShowTaskForm(user);
                ((MainWindow)((Grid)Parent).Parent).label123.Content=user.Login;

            }
            else
            {
                //error
            }
        }
    }
}
