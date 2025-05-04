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

            if (!int.TryParse(edad, out int edadNumerica))
            {
                DisplayAlert("Error", "La edad debe ser un número válido", "OK");
                return;
            }

            DisplayAlert("Registro Exitoso", $"Bienvenido {nombre}, edad {edadNumerica} años", "OK");

            // Redirigir a la página del Quiz
            Navigation.PushAsync(new QuizPage());
        }
    }
}