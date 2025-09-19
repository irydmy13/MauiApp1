using System.Threading.Tasks;

namespace MauiApp1;

public partial class Valgusfoor : ContentPage
{
    bool isOn = false;
    bool isNight = false;
    bool loopRunning = false;   // чтобы не запускать цикл дважды

    const int RED_MS = 5000;       // 5 c
    const int YELLOW_MS = 2000;    // 2 c
    const int GREEN_MS = 5000;     // 5 c
    const int NIGHT_MS = 700;      // мигание ночью

    public Valgusfoor()
    {
        InitializeComponent();
        SetOff();
        DefaultTexts();
    }

    private void OnSisseClicked(object sender, EventArgs e)
    {
        if (isOn == true) return;
        isOn = true;

        if (loopRunning == false)
        {
            loopRunning = true;
            _ = RunLoop();
        }
    }

    private void OnValjaClicked(object sender, EventArgs e)
    {
        if (isOn == false) return;
        isOn = false;
        SetOff();
        DefaultTexts();
    }

    private void NightSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        isNight = e.Value;
        // при включённом светофоре переключение режима подхватится в цикле
    }

    private void RedCircle_Tapped(object s, TappedEventArgs e)
    {
        if (isOn == true) RedText.Text = "Peatu";
        else RedText.Text = "Kõigepealt lülita valgusfoor sisse";
    }

    private void YellowCircle_Tapped(object s, TappedEventArgs e)
    {
        if (isOn == true) YellowText.Text = "Oota";
        else YellowText.Text = "Kõigepealt lülita valgusfoor sisse";
    }

    private void GreenCircle_Tapped(object s, TappedEventArgs e)
    {
        if (isOn == true) GreenText.Text = "Mine";
        else GreenText.Text = "Kõigepealt lülita valgusfoor sisse";
    }

    async Task RunLoop()
    {
        while (true)
        {
            if (isOn == false)
            {
                loopRunning = false;
                break;
            }

            // Ночной режим: мигает только жёлтый
            if (isNight == true)
            {
                RedCircle.BackgroundColor = Colors.Gray;
                GreenCircle.BackgroundColor = Colors.Gray;

                YellowCircle.BackgroundColor = Colors.Yellow;
                YellowText.Text = "Oota";
                await Task.Delay(NIGHT_MS);

                if (isOn == false) continue;
                if (isNight == false) continue;

                YellowCircle.BackgroundColor = Colors.Gray;
                YellowText.Text = "Kollane";
                await Task.Delay(NIGHT_MS);

                continue;
            }

            // Дневной режим: обычный цикл
            SetStateRed();
            await Task.Delay(RED_MS);
            if (isOn == false) continue;
            if (isNight == true) continue;

            SetStateYellow();
            await Task.Delay(YELLOW_MS);
            if (isOn == false) continue;
            if (isNight == true) continue;

            SetStateGreen();
            await Task.Delay(GREEN_MS);
            if (isOn == false) continue;
            if (isNight == true) continue;

            SetStateYellow();
            await Task.Delay(YELLOW_MS);
            // дальше начнётся новый круг
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
