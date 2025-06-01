using QuizAmbiental.Helpers;
using System;
using Microsoft.Maui.Controls;
using QuizAmbiental.Models;
using Microsoft.Maui.Storage;

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
            btnLogin.Text = isRegistered ? "Cerrar sesión" : "Iniciar sesión";
        }

        private void OnStartClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new QuizPage());
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            if (UserSession.CurrentUser == null)
            {
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                bool confirm = await DisplayAlert("Cerrar sesión", "¿Estás seguro de que deseas cerrar sesión?", "Sí", "No");
                if (confirm)
                {
                    UserSession.CurrentUser = null;
                    Preferences.Remove("UserName");
                    Preferences.Remove("UserAge");
                    btnStart.IsEnabled = false;
                    btnLogin.Text = "Iniciar sesión";
                    await DisplayAlert("Sesión Cerrada", "Has cerrado sesión", "OK");
                }
            }
        }

        private void OnPuntuacionClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PuntuacionPage());
        }

        private async void OnSobreNosotrosClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SobreNosotrosPage());
        }

        private async void OnConfiguracionClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ConfiguracionPage());
        }
    }
}
