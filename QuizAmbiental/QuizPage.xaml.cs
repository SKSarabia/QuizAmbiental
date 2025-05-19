using QuizAmbiental.Helpers;
using QuizAmbiental.Models;
using System;

namespace QuizAmbiental
{
    public partial class QuizPage : ContentPage
    {
        private string dificultadSeleccionada = "";
        DatabaseService dbService = new DatabaseService();
        private readonly string[] dificultades = new string[] { "Fácil", "Medio", "Difícil" };
        private int dificultadIndex = 0;

        public QuizPage()
        {
            InitializeComponent();
            PopulateRankingTable(dificultades[dificultadIndex]);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Refrescar la tabla al aparecer con el filtro actual
            PopulateRankingTable(dificultades[dificultadIndex]);

            // Inicia el timer con el Dispatcher (se ejecuta cada 10 segundos)
            this.Dispatcher.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                // Rotar al siguiente filtro de dificultad
                dificultadIndex = (dificultadIndex + 1) % dificultades.Length;
                PopulateRankingTable(dificultades[dificultadIndex]);
                return true;
            });
        }

        // Actualiza la tabla de ranking filtrado por la dificultad pasada
        private void PopulateRankingTable(string filtroDificultad)
        {
            rankingList.Children.Clear();
            rankingList.Children.Add(new Label
            {
                Text = $"Top Jugadores - {filtroDificultad}",
                FontSize = 20,
                TextColor = Color.FromArgb("#3E8C62"),
                HorizontalOptions = LayoutOptions.Center
            });

            var rankingEntries = dbService.GetTopScores(filtroDificultad);
            int rank = 1;
            foreach (var entry in rankingEntries)
            {
                rankingList.Children.Add(new Label
                {
                    Text = $"{rank}. {entry.Username} - {entry.Score} pts",
                    FontSize = 16,
                    TextColor = Colors.Black,
                    Margin = new Thickness(0, 2)
                });
                rank++;
            }
        }

        // Se ejecuta al pulsar alguno de los botones de dificultad
        private void OnDificultadSeleccionada(object sender, EventArgs e)
        {
            // Quitar el borde de los tres botones
            btnFacil.BorderWidth = 0;
            btnMedio.BorderWidth = 0;
            btnDificil.BorderWidth = 0;

            if (sender is Button botonSeleccionado)
            {
                // Actualizamos la dificultad seleccionada
                dificultadSeleccionada = botonSeleccionado.Text;
                UserSession.Dificultad = dificultadSeleccionada;

                // Resaltamos visualmente el botón seleccionado
                botonSeleccionado.BorderWidth = 3;
                botonSeleccionado.BorderColor = Color.FromArgb("#FFFF00");
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
        private async void OnMenuClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}