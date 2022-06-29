using System.Windows;
using TollStationSystem.Core.TollStations.Model;
using TollStationSystem.Core.Users.Model;
using TollStationSystem.Database;
using TollStationSystem.GUI.Controller.Users;
using TollStationSystem.GUI.Controllers.TollStations;
using TollStationSystem.GUI.View.AdministratorView;
using TollStationSystem.GUI.View.BossView;
using TollStationSystem.GUI.View.ClerkView;
using TollStationSystem.GUI.View.ManagerView;

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
            tollStationController = new(serviceBuilder.TollStationService);
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
                AdministratorWindow administratorWindow = new(serviceBuilder);
                administratorWindow.Show();
                Close();
            }
            else if (user.UserType == UserType.BOSS)
            {
                BossWindow bossWindow = new(serviceBuilder, (Boss)user);
                bossWindow.Show();
                Close();
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
                ManagerWindow managerWindow = new(serviceBuilder);
                managerWindow.Show();
                Close();
            }
        }
    }
}
