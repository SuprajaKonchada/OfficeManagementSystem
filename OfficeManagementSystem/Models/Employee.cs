using System.ComponentModel;

namespace OfficeManagementSystem.Models
{
    public class Employee : INotifyPropertyChanged
    {
        private int _employeeId;
        public int EmployeeId
        {
            get { return _employeeId; }
            set
            {
                _employeeId = value;
                OnPropertyChanged(nameof(EmployeeId));
            }
        }

        private string _employeeName;
        public string EmployeeName
        {
            get { return _employeeName; }
            set
            {
                _employeeName = value;
                OnPropertyChanged(nameof(EmployeeName));
            }
        }

        private string _position;
        public string Position
        {
            get { return _position; }
            set
            {
                _position = value;
                OnPropertyChanged(nameof(Position));
            }
        }

        // Event triggered when a property value changes.
        public event PropertyChangedEventHandler PropertyChanged;

        // Raises the PropertyChanged event.
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
