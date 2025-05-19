using QuizAmbiental.Helpers;
using QuizAmbiental.Models;
using System;

namespace QuizAmbiental
{
    public partial class ScorePage : ContentPage
    {
        DatabaseService dbService = new DatabaseService();
        private string dificultadActual;

        public ScorePage(int correctAnswers, int elapsedTime, string dificultadSeleccionada)
        {
            InitializeComponent();

            dificultadActual = dificultadSeleccionada; // Guardar la dificultad jugada

            // Definir multiplicador según la dificultad
            double multiplier = dificultadSeleccionada switch
            {
                "Fácil" => 1.0,
                "Medio" => 1.5,
                "Difícil" => 2.0,
                _ => 1.0
            };

            // Cálculo del puntaje: cada respuesta correcta vale 20 puntos y al total se le resta el tiempo transcurrido
            int score = (int)Math.Max(0, (correctAnswers * 20 - elapsedTime) * multiplier);
            string username = UserSession.CurrentUser != null ? UserSession.CurrentUser.Username : "Invitado";
            lblScore.Text = $"Puntaje: {score}";
            lblTime.Text = $"Usuario: {username}\nTiempo: {elapsedTime} segundos\nRespuestas correctas: {correctAnswers}/10\nDificultad: {dificultadSeleccionada}";

            // Guardar la puntuación en la base de datos, si el usuario está registrado
            if (UserSession.CurrentUser != null)
            {
                int userId = dbService.GetOrCreateUser(username);
                dbService.AddScore(userId, score, dificultadSeleccionada);
            }

            PopulateRankingTable();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            PopulateRankingTable();
        }

        // Se consulta la base de datos y se actualiza la tabla de clasificación solo para la dificultad jugada
        private void PopulateRankingTable()
        {
            rankingList.Children.Clear();

            rankingList.Children.Add(new Label
            {
                Text = $"Tabla de Clasificación - {dificultadActual}",
                FontSize = 24,
                TextColor = Colors.Black,
                HorizontalOptions = LayoutOptions.Center
            });

            var rankingEntries = dbService.GetTopScores(dificultadActual);
            int rank = 1;
            foreach (var entry in rankingEntries)
            {
                rankingList.Children.Add(new Label
                {
                    Text = $"{rank}. {entry.Username} - {entry.Score} pts ({entry.Date.ToShortDateString()})",
                    FontSize = 18,
                    TextColor = Colors.Black
                });
                rank++;
            }
        }

        private async void OnReturnClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        protected override bool OnBackButtonPressed() => true;
    }
}
