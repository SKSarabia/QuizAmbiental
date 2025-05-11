using QuizAmbiental.Helpers;
using QuizAmbiental.Models;

namespace QuizAmbiental
{
    public partial class ScorePage : ContentPage
    {
        public ScorePage(int correctAnswers, int elapsedTime)
        {
            InitializeComponent();

            // Cálculo del puntaje: cada respuesta correcta vale 20 puntos y al total se le resta el tiempo transcurrido.
            int score = Math.Max(0, correctAnswers * 20 - elapsedTime);
            string username = UserSession.CurrentUser != null ? UserSession.CurrentUser.Username : "Invitado";

            lblScore.Text = $"Puntaje: {score}";
            lblTime.Text = $"Usuario: {username}\nTiempo: {elapsedTime} segundos\nRespuestas correctas: {correctAnswers}/10";

            // Agregar el puntaje a la lista de puntuaciones
            var entry = new ScoreEntry
            {
                Username = UserSession.CurrentUser?.Username ?? "Invitado",
                Score = score,
                Date = DateTime.Now
            };
            ScoreManager.AddScore(entry);
        }

        private void OnReturnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new QuizPage());
        }
        protected override bool OnBackButtonPressed()
        {
            // Deshabilitar el botón de retroceso.
            return true;
        }
    }
}