using QuizAmbiental.Models;
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
            // Restaurar usuario al entrar a la aplicación
            if (Preferences.ContainsKey("UserName") && Preferences.ContainsKey("UserAge"))
            {
                string name = Preferences.Get("UserName", string.Empty);
                int age = Preferences.Get("UserAge", 0);
                if (!string.IsNullOrEmpty(name) && age > 0)
                {
                    UserSession.CurrentUser = new User { Name = name, Age = age };
                }
            }

            MainPage = new NavigationPage(new MainPage());

        }
    }
}
