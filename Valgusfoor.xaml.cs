using System.Threading.Tasks;

namespace MauiApp1;

public partial class Valgusfoor : ContentPage
{
    bool isOn = false;
    bool isNight = false;
    string mode = "off";

    public Valgusfoor()
    {
        InitializeComponent();
        SetOff();
        DefaultTexts();
    }

    private void OnSisseClicked(object sender, EventArgs e)
    {
        if (isOn) return;
        isOn = true;

        if (isNight) StartNight();
        else StartDay();
    }

    private void OnValjaClicked(object sender, EventArgs e)
    {
        if (!isOn) return;
        isOn = false;
        mode = "off";
        SetOff();
        DefaultTexts();
    }

    private void NightSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        isNight = e.Value;

        if (!isOn) return;

        if (isNight) StartNight();
        else StartDay();
    }

    private void RedCircle_Tapped(object s, TappedEventArgs e)
    {
        if (isOn) RedText.Text = "Peatu";
        else RedText.Text = "Pane käima valgusfoor";
    }

    private void YellowCircle_Tapped(object s, TappedEventArgs e)
    {
        if (isOn) YellowText.Text = "Oota";
        else YellowText.Text = "Pane käima valgusfoor";
    }

    private void GreenCircle_Tapped(object s, TappedEventArgs e)
    {
        if (isOn) GreenText.Text = "Mine";
        else GreenText.Text = "Kõigepealt lülita valgusfoor sisse";
    }

    void StartDay()
    {
        mode = "day";
        _ = DayCycle();
    }

    void StartNight()
    {
        mode = "night";
        // перед миганием гасим красный и зелёный
        RedCircle.BackgroundColor = Colors.Gray;
        GreenCircle.BackgroundColor = Colors.Gray;
        DefaultTexts();
        _ = NightBlink();
    }

    async Task DayCycle()
    {
        while (mode == "day")
        {
            SetStateRed();
            await Task.Delay(1500);
            if (mode != "day") break;

            SetStateYellow();
            await Task.Delay(800);
            if (mode != "day") break;

            SetStateGreen();
            await Task.Delay(1500);
            if (mode != "day") break;

            SetStateYellow();
            await Task.Delay(800);
        }
    }

    async Task NightBlink()
    {
        while (mode == "night")
        {
            YellowCircle.BackgroundColor = Colors.Yellow;
            YellowText.Text = "Oota";
            await Task.Delay(600);
            if (mode != "night") break;

            YellowCircle.BackgroundColor = Colors.Gray;
            YellowText.Text = "Kollane";
            await Task.Delay(500);
        }
    }

    void SetOff()
    {
        RedCircle.BackgroundColor = Colors.Gray;
        YellowCircle.BackgroundColor = Colors.Gray;
        GreenCircle.BackgroundColor = Colors.Gray;
    }

    void DefaultTexts()
    {
        RedText.Text = "Punane";
        YellowText.Text = "Kollane";
        GreenText.Text = "Roheline";
    }

    void SetStateRed()
    {
        RedCircle.BackgroundColor = Colors.Red;
        YellowCircle.BackgroundColor = Colors.Gray;
        GreenCircle.BackgroundColor = Colors.Gray;

        RedText.Text = "Peatu";
        YellowText.Text = "Kollane";
        GreenText.Text = "Roheline";
    }

    void SetStateYellow()
    {
        RedCircle.BackgroundColor = Colors.Gray;
        YellowCircle.BackgroundColor = Colors.Yellow;
        GreenCircle.BackgroundColor = Colors.Gray;

        RedText.Text = "Punane";
        YellowText.Text = "Oota";
        GreenText.Text = "Roheline";
    }

    void SetStateGreen()
    {
        RedCircle.BackgroundColor = Colors.Gray;
        YellowCircle.BackgroundColor = Colors.Gray;
        GreenCircle.BackgroundColor = Colors.Green;

        RedText.Text = "Punane";
        YellowText.Text = "Kollane";
        GreenText.Text = "Mine";
    }
}
