using QuizAmbiental.Helpers;
using QuizAmbiental.Models;

namespace QuizAmbiental
{
    public partial class PuntuacionPage : ContentPage
    {
        public PuntuacionPage()
        {
            InitializeComponent();
        }
        // Tabla de puntuaciones
        protected override void OnAppearing()
        {
            base.OnAppearing();
            PopulateRankingTable();
        }

        private void PopulateRankingTable()
        {
            rankingListPuntuacion.Children.Clear();

            // Agrega el encabezado
            rankingListPuntuacion.Children.Add(new Label
            {
                Text = "Top Jugadores",
                FontSize = 20,
                TextColor = Color.FromArgb("#3E8C62"),
                HorizontalOptions = LayoutOptions.Center
            });

            var rankingEntries = ScoreManager.GetScores();
            int rank = 1;
            foreach (var entry in rankingEntries)
            {
                var lblEntry = new Label
                {
                    Text = $"{rank}. {entry.Username} - {entry.Score} Pts",
                    FontSize = 24,
                    TextColor = Colors.Black
                };
                rankingListPuntuacion.Children.Add(lblEntry);
                rank++;
            }
        }

        private void OnVolverClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}