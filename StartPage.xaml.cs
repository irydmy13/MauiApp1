using Microsoft.Maui.Controls;
using MauiApp1;

namespace MauiApp1;

public partial class StartPage : ContentPage
{
    public List<ContentPage> lehed = new List<ContentPage>() { new TextPage(), new FigurePage(), new TimerPage(), new DateTimePage(), new Lumememm() };
    public List<string> tekstid = new List<string>() { "Tee lahti leht Tekst'ga", "Tee lahti Figure leht", "Käivita taimeri", "Kuupäevad ja kellaajad", "Lumememm" };
    ScrollView sv;
    VerticalStackLayout vsl;
    public StartPage()
    {
        //InitializeComponent();
        Title = "Avaleht";
    }

    private async void OpenTextPage(object sender, EventArgs e)
        => await Navigation.PushAsync(new TextPage());

    private async void OpenFigurePage(object sender, EventArgs e)
        => await Navigation.PushAsync(new FigurePage());

    private async void OpenTimerPage(object sender, EventArgs e)
        => await Navigation.PushAsync(new TimerPage());

    private async void OpenDateTimePage(object sender, EventArgs e)
        => await Navigation.PushAsync(new DateTimePage());

    private async void OpenLumememmPage(object sender, EventArgs e)
        => await Navigation.PushAsync(new Lumememm());
}
    