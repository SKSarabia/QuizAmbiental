namespace QuizAmbiental
{
    public partial class QuizPage : ContentPage
    {
        private string dificultadSeleccionada = "";

        public QuizPage()
        {
            InitializeComponent();
        }

        private void OnDificultadSeleccionada(object sender, EventArgs e)
        {
            if (sender is Button botonSeleccionado)
            {
                btnFacil.BackgroundColor = Colors.Gray;
                btnMedio.BackgroundColor = Colors.Gray;
                btnDificil.BackgroundColor = Colors.Gray;

                dificultadSeleccionada = botonSeleccionado.Text;
                botonSeleccionado.BackgroundColor = Colors.LightGreen;

                // Activar el botón "Comenzar"
                btnComenzar.IsEnabled = true;
            }
        }


        private void OnComenzarQuiz(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dificultadSeleccionada))
            {
                DisplayAlert("Error", "Selecciona una dificultad antes de comenzar", "OK");
                return;
            }

            // Navegar según la dificultad elegida
            ContentPage? quizDestino = dificultadSeleccionada switch
            {
                "Fácil" => new QuizFacilPage(),
                "Medio" => new QuizMedioPage(),
                "Difícil" => new QuizDificilPage(),
                _ => null
            };

            if (quizDestino != null)
                Navigation.PushAsync(quizDestino);
        }
    }
}