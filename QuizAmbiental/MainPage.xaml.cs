namespace QuizAmbiental
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnStartClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new QuizPage());
        }

        private void OnRegistroClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegistroPage());
        }

        private void OnPuntuacionClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PuntuacionPage());
        }
    }
}
