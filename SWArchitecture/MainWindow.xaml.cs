using System.Windows;
using SWArchitecture.Models;

namespace SWArchitecture
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SystemUser User { get; set; } = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void ShowTaskForm(SystemUser user)
        {
            if (user.UserType == UserTypes.Student) StudentTaskForm.Visibility = Visibility.Visible;
            else TeacherTaskForm.Visibility = Visibility.Visible;
        }
    }
}
