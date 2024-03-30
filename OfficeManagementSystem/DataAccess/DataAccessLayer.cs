using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using OfficeManagementSystem.Models;

namespace OfficeManagementSystem.DataAccess
{
    public class DataAccessLayer
    {
        private readonly string _connectionString;
        /// <summary>
        /// Constructor to initialize connection string from configuration.
        /// </summary>

        public DataAccessLayer()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["OfficeManagementConnectionString"].ConnectionString;
        }

        /// <summary>
        /// Retrieves a collection of all employees from the database.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Employee> GetEmployees()
        {
            ObservableCollection<Employee> employees = new ObservableCollection<Employee>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Employees";
                var command = new SqlCommand(query, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            EmployeeId = (int)reader["EmployeeId"],
                            EmployeeName = reader["EmployeeName"].ToString(),
                            Position = reader["Position"].ToString()
                        });
                    }
                }
            }

            return employees;
        }

        /// <summary>
        /// Adds a new employee to the database.
        /// </summary>
        /// <param name="employee"></param>
        public void AddEmployee(Employee employee)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Employees (EmployeeName, Position) VALUES (@EmployeeName, @Position)";
                command.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                command.Parameters.AddWithValue("@Position", employee.Position);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Updates an existing employee in the database.
        /// </summary>
        /// <param name="employee"></param>
        public void UpdateEmployee(Employee employee)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE Employees SET EmployeeName = @EmployeeName, Position = @Position WHERE EmployeeId = @EmployeeId";
                command.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                command.Parameters.AddWithValue("@Position", employee.Position);
                command.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Deletes an employee from the database by their ID.
        /// </summary>
        /// <param name="employeeId"></param>
        public void DeleteEmployee(int employeeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Employees WHERE EmployeeId = @EmployeeId";
                command.Parameters.AddWithValue("@EmployeeId", employeeId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
