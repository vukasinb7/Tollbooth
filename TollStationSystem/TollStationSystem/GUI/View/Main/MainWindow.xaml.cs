using System.Windows;
using TollStationSystem.Core.TollStations.Model;
using TollStationSystem.Core.Users.Model;
using TollStationSystem.Database;
using TollStationSystem.GUI.Controller.Users;
using TollStationSystem.GUI.Controllers.TollStations;
using TollStationSystem.GUI.View.ClerkView;

namespace TollStationSystem
{
    public partial class MainWindow : Window
    {
        ServiceBuilder serviceBuilder;
        UserController userController;
        TollStationController tollStationController;

        public MainWindow()
        {
            serviceBuilder = new();
            userController = new(serviceBuilder.UserService);
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTb.Text;
            string password = passwordTb.Password;

            User user = userController.Login(username, password);
            if (user is null)
            {
                MessageBox.Show("Invalid username or password!");
                passwordTb.Clear();
            }
            else if (user.UserType == UserType.ADMINISTRATOR)
            {
                MessageBox.Show("Logged in as admin");
            }
            else if (user.UserType == UserType.BOSS)
            {
                MessageBox.Show("Logged in as boss");
            }
            else if (user.UserType == UserType.CLERK)
            {
                TollStation tollStation = tollStationController.FindByWorkerJmbg(user.Jmbg);
                ClerkWindow clerkWindow = new ClerkWindow(tollStation, serviceBuilder);
                clerkWindow.Show();
                Close();
            }
            else if (user.UserType == UserType.MANAGER)
            {
                MessageBox.Show("Logged in as manager");
            }
        }
    }
}
