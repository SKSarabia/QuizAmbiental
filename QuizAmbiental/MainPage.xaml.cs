using QuizAmbiental.Helpers;
using System;
using Microsoft.Maui.Controls;

namespace QuizAmbiental
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Activa el botón de Start solo si hay usuario registrado
            bool isRegistered = (UserSession.CurrentUser != null);
            btnStart.IsEnabled = isRegistered;
            btnRegistro.Text = isRegistered ? "Cerrar sesión" : "Registrarse";
        }

        private void OnStartClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new QuizPage());
        }

        private async void OnRegistroClicked(object sender, EventArgs e)
        {
            if (UserSession.CurrentUser == null)
            {
                await Navigation.PushAsync(new RegistroPage());
            }
            else
            {
                bool confirm = await DisplayAlert("Cerrar sesión", "¿Estás seguro de que deseas cerrar sesión?", "Sí", "No");
                if (confirm)
                {
                    // Se cierra la sesión
                    UserSession.CurrentUser = null;
                    // Botón Start deshabilitado
                    btnStart.IsEnabled = false;
                    btnRegistro.Text = "Registrarse";
                    await DisplayAlert("Sesión Cerrada", "Has cerrado sesión", "OK");
                }
            }
        }

        private void OnPuntuacionClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PuntuacionPage());
        }
    }
}
