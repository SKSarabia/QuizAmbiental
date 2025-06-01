using QuizAmbiental.Models;
using QuizAmbiental.Helpers;
using Microsoft.Maui.Storage;
using Microsoft.Maui.ApplicationModel;
namespace QuizAmbiental
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Application.Current.UserAppTheme = AppTheme.Light;

            // Restaurar usuario desde la base de datos si hay sesión guardada
            if (Preferences.ContainsKey("UserName"))
            {
                string name = Preferences.Get("UserName", string.Empty);
                if (!string.IsNullOrEmpty(name))
                {
                    var db = new DatabaseService();
                    var usuario = db.GetUsuarioByUsername(name);
                    if (usuario != null)
                    {
                        UserSession.CurrentUser = new User { Name = usuario.Username, Age = 0 };
                    }
                }
            }

            MainPage = new NavigationPage(new MainPage());

        }
    }
}
