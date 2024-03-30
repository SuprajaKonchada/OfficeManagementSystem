using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using OfficeManagementSystem.DataAccess;
using OfficeManagementSystem.Models;

namespace OfficeManagementSystem.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly DataAccessLayer _dataAccessLayer;

        // Collection of employees bound to the UI.
        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();
        // Selected employee in the UI.
        public Employee SelectedEmployee { get; set; }

        public RelayCommand AddCommand { get; }
        public RelayCommand UpdateCommand { get; }
        public RelayCommand DeleteCommand { get; }

        public MainViewModel(DataAccessLayer dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;

            AddCommand = new RelayCommand(AddEmployee);
            UpdateCommand = new RelayCommand(UpdateEmployee, CanUpdateDelete);
            DeleteCommand = new RelayCommand(DeleteEmployee, CanUpdateDelete);

            LoadData();
        }

        /// <summary>
        /// Loads employee data from the database.
        /// </summary>
        private void LoadData()
        {
            Employees.Clear();
            foreach (var employee in _dataAccessLayer.GetEmployees())
            {
                Employees.Add(employee);
            }
        }

        /// <summary>
        /// Determines if updating or deleting an employee is possible.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanUpdateDelete(object obj)
        {
            return SelectedEmployee != null;
        }

        /// <summary>
        /// Adds a new employee.
        /// </summary>
        /// <param name="obj"></param>
        private void AddEmployee(object obj)
        {
            var employeeForm = new EmployeeForm();
            employeeForm.SaveClicked += (name, position) =>
            {
                Employee newEmployee = new Employee { EmployeeName = name, Position = position };
                _dataAccessLayer.AddEmployee(newEmployee);
                LoadData();
            };

            ShowDialog(employeeForm, "Add Employee");
        }

        /// <summary>
        /// Updates the selected employee.
        /// </summary>
        /// <param name="obj"></param>
        private void UpdateEmployee(object obj)
        {
            var employeeForm = new EmployeeForm();
            employeeForm.txtEmployeeName.Text = SelectedEmployee.EmployeeName;
            employeeForm.txtPosition.Text = SelectedEmployee.Position;

            employeeForm.SaveClicked += (name, position) =>
            {
                SelectedEmployee.EmployeeName = name;
                SelectedEmployee.Position = position;
                _dataAccessLayer.UpdateEmployee(SelectedEmployee);
                LoadData();
            };

            ShowDialog(employeeForm, "Update Employee");
        }

        /// <summary>
        /// Deletes the selected employee
        /// </summary>
        /// <param name="obj"></param>
        private void DeleteEmployee(object obj)
        {
            if (SelectedEmployee != null)
            {
                _dataAccessLayer.DeleteEmployee(SelectedEmployee.EmployeeId);
                LoadData();
            }
        }

        /// <summary>
        /// Displays a dialog window.
        /// </summary>
        /// <param name="employeeForm"></param>
        /// <param name="title"></param>
        private void ShowDialog(EmployeeForm employeeForm, string title)
        {
            var window = new Window
            {
                Title = title,
                Content = employeeForm,
                SizeToContent = SizeToContent.WidthAndHeight
            };
            window.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}