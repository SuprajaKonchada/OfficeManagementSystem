using System;
using System.Windows;
using System.Windows.Controls;
using OfficeManagementSystem.Models;

namespace OfficeManagementSystem
{
    public partial class EmployeeForm : UserControl
    {
        public event Action<string, string> SaveClicked;

        public EmployeeForm() => InitializeComponent();

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string name = txtEmployeeName.Text;
            string position = txtPosition.Text;
            SaveClicked?.Invoke(name, position);

            // Close the window to hide the popup
            Window.GetWindow(this).Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

    }
}
