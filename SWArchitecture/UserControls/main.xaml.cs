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

namespace SWArchitecture.UserControls
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class main : Page
    {
        public main()
        {
            InitializeComponent();
        }
        public class Frame
        {
            public string Header { get; set; }
            public string FrameSource { get; set; }
        }
        public static class Facade
        {
            public static void ShowHome(Frame content)
            {
                content.Header = "Домашня сторінка";
                content.FrameSource = @"UserControl\main.xaml";
            }
            public static void Showlogin(Frame content)
            {
                content.Header = "Вхід на сторінку";
                content.FrameSource = @"UserControl\Login.xaml";
            }
            public static void ShowTests(Frame content)
            {if (MainWindow.Student_teach == 0)
                {
                    content.Header = "Тести";
                    content.FrameSource = @"UserControl\TaskForm.xaml";
                }else
                {
                    content.Header = "Додати тест";
                    content.FrameSource = @"UserControl\CreateTaskForm.xaml";
                }
            }
            public static void Showsstats(Frame content)
            {
                content.Header = "Статистика";
                content.FrameSource = @"UserControl\Stats.xaml";
            }
            public static void Showtheory(Frame content)
            {
                content.Header = "Посилання";
                content.FrameSource = @"UserControl\theory.xaml";
            }
        }
        public void ShowFrame(object sender, RoutedEventArgs e)
        {
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
                case "Constructor":
                    Facade.Showsstats(frame);
                    break;
                case "Reporting":
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
