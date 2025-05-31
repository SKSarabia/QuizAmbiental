using QuizAmbiental.Helpers;
using QuizAmbiental.Models;

namespace QuizAmbiental;

public partial class AdminPage : ContentPage
{
    private DatabaseService dbService = new DatabaseService();
    private Usuario usuarioSeleccionado;

    public AdminPage()
    {
        InitializeComponent();
        CargarUsuarios();
    }

    private void CargarUsuarios()
    {
        var usuarios = dbService.GetAllUsuarios();
        usuariosList.ItemsSource = usuarios;
        editFrame.IsVisible = false;
    }

    private void OnRegisterClicked(object sender, EventArgs e)
    {
        string nombre = nameEntry.Text?.Trim();
        string edadStr = ageEntry.Text?.Trim();

        if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(edadStr) || !int.TryParse(edadStr, out int edad))
        {
            DisplayAlert("Error", "Nombre y edad válidos requeridos", "OK");
            return;
        }

        string username = $"{nombre}{edad}";
        int userId = dbService.GetOrCreateUser(username);

        if (userId > 0)
        {
            DisplayAlert("Éxito", $"Usuario {username} registrado", "OK");
            nameEntry.Text = "";
            ageEntry.Text = "";
            CargarUsuarios();
        }
        else
        {
            DisplayAlert("Error", "No se pudo registrar el usuario", "OK");
        }
    }

    private void OnUsuarioSeleccionado(object sender, SelectionChangedEventArgs e)
    {
        usuarioSeleccionado = e.CurrentSelection.FirstOrDefault() as Usuario;
        if (usuarioSeleccionado != null)
        {
            editNameEntry.Text = usuarioSeleccionado.Username; // Puedes separar nombre y edad si lo deseas
            editAgeEntry.Text = ""; // No tienes edad en Usuario, solo en el username
            editFrame.IsVisible = true;
        }
    }

    private void OnActualizarUsuario(object sender, EventArgs e)
    {
        if (usuarioSeleccionado == null) return;

        string nuevoNombre = editNameEntry.Text?.Trim();
        string nuevaEdad = editAgeEntry.Text?.Trim();

        if (string.IsNullOrWhiteSpace(nuevoNombre) || string.IsNullOrWhiteSpace(nuevaEdad) || !int.TryParse(nuevaEdad, out int edad))
        {
            DisplayAlert("Error", "Nombre y edad válidos requeridos", "OK");
            return;
        }

        string nuevoUsername = $"{nuevoNombre}{edad}";
        usuarioSeleccionado.Username = nuevoUsername;
        dbService.UpdateUsuario(usuarioSeleccionado);
        DisplayAlert("Éxito", "Usuario actualizado", "OK");
        CargarUsuarios();
    }

    private void OnEliminarUsuario(object sender, EventArgs e)
    {
        if (usuarioSeleccionado == null) return;
        dbService.DeleteUsuario(usuarioSeleccionado.ID);
        DisplayAlert("Éxito", "Usuario eliminado", "OK");
        CargarUsuarios();
    }

    private void OnCancelarEdicion(object sender, EventArgs e)
    {
        editFrame.IsVisible = false;
        usuarioSeleccionado = null;
    }

    private async void OnCerrarSesionAdmin(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
}
