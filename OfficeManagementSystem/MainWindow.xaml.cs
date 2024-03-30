using OfficeManagementSystem.ViewModels;
using OfficeManagementSystem.DataAccess;
using System.Windows;

namespace OfficeManagementSystem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataAccessLayer dataAccessLayer = new DataAccessLayer();
            DataContext = new MainViewModel(dataAccessLayer);
        }
    }
}
