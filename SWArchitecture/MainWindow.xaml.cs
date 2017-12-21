using System.Windows;
using MahApps.Metro.Controls;
using SWArchitecture.Models;
using System.Windows.Controls;

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
        public static int Student_teach = 0;
        public void ShowTaskForm(SystemUser user)
        {
            if (user.UserType == UserTypes.Student)
            {
                Student_teach = 0;
                StudentTaskForm.Visibility = Visibility.Visible;
                StudentTaskForm.User = user;
                loginctrl.Visibility = Visibility.Collapsed;

            }
            else
            {
                Student_teach = 1;
                TeacherTaskForm.Visibility = Visibility.Visible;

                loginctrl.Visibility = Visibility.Collapsed;
            }


        }
    }
}
