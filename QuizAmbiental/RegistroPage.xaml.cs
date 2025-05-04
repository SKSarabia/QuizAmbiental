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
                DisplayAlert("Error", "La edad debe ser un n�mero v�lido", "OK");
                return;
            }

            DisplayAlert("Registro Exitoso", $"Bienvenido {nombre}, edad {edadNumerica} a�os", "OK");

            // Redirigir a la p�gina del Quiz
            Navigation.PushAsync(new QuizPage());
        }
    }
}