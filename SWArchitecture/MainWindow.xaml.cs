using System.Windows;
using MahApps.Metro.Controls;
using SWArchitecture.Models;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SWArchitecture
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public class Frame
        {
            public string Header { get; set; }
            public string FrameSource { get; set; }
        }

        

        public  class Facade
        {
            public static void ShowHome(Frame content)
            {
                content.Header = "Домашня сторінка";
                content.FrameSource = "UserControls\\Login.xaml";
            }
            public static void Showlogin(Frame content)
            {
                content.Header = "Вхід на сторінку";
                content.FrameSource = @"UserControls\Login.xaml";
            }

            public static void ShowTests(Frame content)
            {
                if (User.UserType == UserTypes.Student)
                {
                    content.Header = "Тести";
                    content.FrameSource = @"UserControls\TaskForm.xaml";
                }
                else
                {


                    ; content.Header = "Додати тест";
                    content.FrameSource = @"UserControls\CreateTaskForm.xaml";
                }
            }

            public static void Showsstats(Frame content)
            {
                content.Header = "Статистика";
                content.FrameSource = @"UserControls\Stats.xaml";
            }
            public static  void Showtheory(Frame content)
            {
                content.Header = "Посилання";
                content.FrameSource = @"UserControls\theory.xaml";
            }
        }


        public static SystemUser User { get; set; } = null;
        public MainWindow()
        {
            InitializeComponent();
            MainGrid.ColumnDefinitions[0].Width = new GridLength(0);//только логин, больше ничего не отображаем
            ShowFrame(Home, new RoutedEventArgs());
        }
        public static int Student_teach = 0;
   
        public void ShowFrame(object sender, RoutedEventArgs e)
        {
            if (User != null)
            {
                if (User.UserType == UserTypes.Teacher)
                {
                    UserImage.Source = new BitmapImage(new Uri(@"/Images/lecturer.png",UriKind.Relative));
                }
                else
                {
                    UserImage.Source = new BitmapImage(new Uri(@"/Images/student.png",UriKind.Relative));
                }
                UserLoginTextBlock.Text = User.Login;
                UserFullNameTextBlock.Text = User.Name+" "+User.Surname;
            }
            // buttons.Select(b => b.Background = new SolidColorBrush(Color.FromRgb(41, 41, 41)));
            if (!(sender is Button)) return;

            var button = (Button)sender;
            Frame frame = new Frame() { };
            switch (button.Name)
            {
                case "Home":
                    Facade.ShowHome(frame);
                    break;
                case "login":
                    Facade.Showlogin(frame);
                    break;
                case "stats":
                    Facade.Showsstats(frame);
                    break;
                case "theory":
                    Facade.Showtheory(frame);
                    break;
                case "Testing":
                    Facade.ShowTests(frame);
                    break;
            }
            Content.Source = new Uri(frame.FrameSource, UriKind.RelativeOrAbsolute);//new Uri(AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug","")+frame.FrameSource,UriKind.Absolute);
            Header.Text = frame.Header;
            //button.Background = Brushes.DarkGray;
            //buttons.Where(x => x.Name != button.Name).Select(b => b.Background=new  SolidColorBrush(Color.FromRgb(41,41,41)));
        }
    }
}

