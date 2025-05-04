namespace QuizAmbiental
{
    public partial class PuntuacionPage : ContentPage
    {
        public PuntuacionPage()
        {
            InitializeComponent();
        }

        private void OnVolverClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}