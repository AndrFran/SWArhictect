using System.Windows.Controls;

namespace SWArchitecture
{
    /// <summary>
    /// Interaction logic for CreateTaskForm.xaml
    /// </summary>
    public partial class CreateTaskForm : UserControl
    {
        public CreateTaskForm()
        {
            InitializeComponent();
            ListBoxModuleType.Items.Add("Refactor Code");
            ListBoxModuleType.Items.Add("Write Code");
            ListBoxModuleType.Items.Add("Code Style");
        }
    }
}
