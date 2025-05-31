using QuizAmbiental.Helpers;
using QuizAmbiental.Models;

namespace QuizAmbiental;

public partial class LoginPage : ContentPage
{
    private const string AdminCode = "ADMIN123";

    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnEntrarClicked(object sender, EventArgs e)
    {
        string input = usernameEntry.Text?.Trim() ?? "";

        if (string.IsNullOrEmpty(input))
        {
            await DisplayAlert("Error", "Ingresa un usuario o código", "OK");
            return;
        }
 
        if (input == AdminCode)
        {
            await Navigation.PushAsync(new AdminPage());
            return;
        }

        // Solo permite acceso si el usuario ya existe
        var db = new DatabaseService();
        var usuario = db.GetUsuarioByUsername(input);
        if (usuario != null)
        {
            UserSession.CurrentUser = new User { Name = usuario.Username, Age = 0 }; // Ajusta si tienes edad almacenada
            await Navigation.PopToRootAsync(); // Vuelve al menú principal
        }
        else
        {
            await DisplayAlert("Error", "Usuario no registrado", "OK");
        }
    }

    private async void OnVolverClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
