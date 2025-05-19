using QuizAmbiental.Helpers;
using QuizAmbiental.Models;
using System;

namespace QuizAmbiental
{
    public partial class PuntuacionPage : ContentPage
    {
        DatabaseService dbService = new DatabaseService();
        private readonly string[] dificultades = new string[] { "Fácil", "Medio", "Difícil" };
        private int dificultadIndex = 0;

        public PuntuacionPage()
        {
            InitializeComponent();
            PopulateRankingTable(dificultades[dificultadIndex]);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Refrescar la tabla al aparecer con el filtro actual
            PopulateRankingTable(dificultades[dificultadIndex]);

            // Inicia el timer con el Dispatcher (se ejecuta cada 5 segundos)
            this.Dispatcher.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                // Rotar al siguiente filtro de dificultad
                dificultadIndex = (dificultadIndex + 1) % dificultades.Length;
                PopulateRankingTable(dificultades[dificultadIndex]);
                return true;
            });
        }

        private void PopulateRankingTable(string filtroDificultad)
        {
            rankingListPuntuacion.Children.Clear();
            rankingListPuntuacion.Children.Add(new Label
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
                rankingListPuntuacion.Children.Add(new Label
                {
                    Text = $"{rank}. {entry.Username} - {entry.Score} pts",
                    FontSize = 24,
                    TextColor = Colors.Black
                });
                rank++;
            }
        }

        private void OnVolverClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
