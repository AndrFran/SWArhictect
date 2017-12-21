using System.Windows;
using MahApps.Metro.Controls;
using SWArchitecture.Models;

namespace SWArchitecture
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public SystemUser User { get; set; } = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void ShowTaskForm(SystemUser user)
        {
            if (user.UserType == UserTypes.Student)
            {
                StudentTaskForm.Visibility = Visibility.Visible;
                StudentTaskForm.User = user;
            }
            else
                TeacherTaskForm.Visibility = Visibility.Visible;
        }
    }
}
