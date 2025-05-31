using Microsoft.Maui.Storage;
using QuizAmbiental.Models;

namespace QuizAmbiental;

public partial class ConfiguracionPage : ContentPage
{
    public ConfiguracionPage()
    {
        InitializeComponent();
    }

    private void ActivarNotificaciones(object sender, ToggledEventArgs e)
    {
        if (e.Value)
            DisplayAlert("Notificaciones", "Notificaciones activadas", "OK");
        else
            DisplayAlert("Notificaciones", "Notificaciones desactivadas", "OK");
    }

    private void CambiarTema(object sender, EventArgs e)
    {
        if (Application.Current.UserAppTheme == AppTheme.Dark)
        {
            Application.Current.UserAppTheme = AppTheme.Light;
            DisplayAlert("Tema", "Modo Claro activado", "OK");
        }
        else
        {
            Application.Current.UserAppTheme = AppTheme.Dark;
            DisplayAlert("Tema", "Modo Oscuro activado", "OK");
        }
    }
}
