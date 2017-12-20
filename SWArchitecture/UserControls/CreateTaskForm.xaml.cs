using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SWArchitecture.Models;

namespace SWArchitecture.UserControls
{
    /// <summary>
    /// Interaction logic for CreateTaskForm.xaml
    /// </summary>
    public partial class CreateTaskForm : UserControl
    {
        List<string> ListBoxItems = new List<string>{ "Refactor Code", "Write Code", "Code Style" };
        public CreateTaskForm()
        {
            InitializeComponent();

            ListBoxModuleType.Items.Add(ListBoxItems[0]);
            ListBoxModuleType.Items.Add(ListBoxItems[1]);
            ListBoxModuleType.Items.Add(ListBoxItems[2]);
        }

        private void ButtonAddTask_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            using (var context = new ApplicationDbContext())
            {
                var task = new Task()
                {
                    Description = RichTextBoxTaskDesc.Text,
                    Upcode = RichTextBoxTaskUp.Text,
                    Answer = RichTextBoxAnswer.Text,
                    DownCode = RichTextBoxTaskLower.Text,
                    RuleForTask = RichTextBoxTaskRules.Text,
                    Type = (TaskType) ListBoxItems.IndexOf(ListBoxModuleType.SelectionBoxItemStringFormat)
                };
                context.Task.Add(task);
                if (context.SaveChanges() != 0)
                {
                    MessageBox.Show("Succsessfully added!", "Task", MessageBoxButton.OK);
                }
            }
        }
    }
}
