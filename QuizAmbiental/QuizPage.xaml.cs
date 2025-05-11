using QuizAmbiental.Helpers;
using QuizAmbiental.Models;

namespace QuizAmbiental
{
    public partial class QuizPage : ContentPage
    {
        private string dificultadSeleccionada = "";

        public QuizPage()
        {
            InitializeComponent();
            PopulateRankingTable();
        }

        // Tabla de puntuaciones
        private void PopulateRankingTable()
        {
            rankingList.Children.Clear();

            // Agrega el encabezado
            rankingList.Children.Add(new Label
            {
                Text = "Top Jugadores",
                FontSize = 20,
                TextColor = Color.FromArgb("#3E8C62"),
                HorizontalOptions = LayoutOptions.Center
            });

            // Obtiene las puntuaciones ordenadas
            var rankingEntries = ScoreManager.GetScores();
            int rank = 1;
            foreach (var entry in rankingEntries)
            {
                var lblEntry = new Label
                {
                    Text = $"{rank}. {entry.Username} - {entry.Score} Pts",
                    FontSize = 16,
                    TextColor = Colors.Black,
                    Margin = new Thickness(0, 2)
                };
                rankingList.Children.Add(lblEntry);
                rank++;
            }
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
        private async void OnMenuClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}