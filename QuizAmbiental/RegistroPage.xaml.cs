using QuizAmbiental.Models;
using QuizAmbiental.Helpers;
using System.Text.RegularExpressions;

namespace QuizAmbiental
{
    public partial class RegistroPage : ContentPage
    {
        public RegistroPage()
        {
            InitializeComponent();
        }

        private void OnRegisterClicked(object sender, EventArgs e)
        {
            string nombre = nameEntry.Text;
            string edad = ageEntry.Text;

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(edad))
            {
                DisplayAlert("Error", "Por favor, ingrese todos los datos", "OK");
                return;
            }

            // Valida el nombre
            if (!Regex.IsMatch(nombre, @"^[a-zA-Z]+$"))
            {
                DisplayAlert("Error", "El nombre debe contener solo letras", "OK");
                return;
            }

            // Valida la edad
            if (!int.TryParse(edad, out int edadNumerica))
            {
                DisplayAlert("Error", "La edad debe ser un número válido", "OK");
                return;
            }

            var usuario = new User()
            {
                Name = nombre,
                Age = edadNumerica
            };
            UserSession.CurrentUser = usuario;
            DisplayAlert("Registro Exitoso", $"Bienvenido {usuario.Username}", "OK");

            Navigation.PopAsync();
        }
    }
}