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
            // Quitar el borde de los botones de dificultad
            btnFacil.BorderWidth = 0;
            btnMedio.BorderWidth = 0;
            btnDificil.BorderWidth = 0;

            if (sender is Button botonSeleccionado)
            {
                // Dificultad seleccionada
                dificultadSeleccionada = botonSeleccionado.Text;

                // Resaltar la opci�n seleccionada
                botonSeleccionado.BorderWidth = 3;
                botonSeleccionado.BorderColor = Color.FromArgb("#FFFF00");

                // Activar el bot�n "Comenzar"
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

            // Navegar seg�n la dificultad elegida
            ContentPage? quizDestino = dificultadSeleccionada switch
            {
                "F�cil" => new QuizFacilPage(),
                "Medio" => new QuizMedioPage(),
                "Dif�cil" => new QuizDificilPage(),
                _ => null
            };

            if (quizDestino != null)
                Navigation.PushAsync(quizDestino);
        }
    }
}