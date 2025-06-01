using Microsoft.Maui.Controls;

namespace QuizAmbiental;

public partial class SobreNosotrosPage : ContentPage
{
    public SobreNosotrosPage()
    {
        InitializeComponent();
    }

    private void OnImageTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is string imageSource)
        {
            PopupImage.Source = imageSource;
            ImagePopup.IsVisible = true;
        }
    }

    private void OnPopupTapped(object sender, TappedEventArgs e)
    {
        ImagePopup.IsVisible = false;
        PopupImage.Source = null;
    }
}