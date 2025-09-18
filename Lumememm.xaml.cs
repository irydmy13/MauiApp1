using Microsoft.Maui.Layouts;

namespace MauiApp1;

public partial class Lumememm : ContentPage
{
    // Подпись выбранного действия
    Label mis_on_valitud;

    // Управление
    Picker picker;       // выбор действия
    Button btn;          // кнопка "выполнить"
    Slider slider;       // прозрачность
    Stepper stepper;     // скорость (мс)

    // Снеговик (контейнер + части)
    AbsoluteLayout al;
    AbsoluteLayout snowman; // на нём меняем opacity/анимации
    Frame keha;   // тело
    Frame pea;    // голова
    Frame aamber; // ведро

    Random rnd = new Random();

    public Lumememm()
    {
        Title = "Lumememm";

        mis_on_valitud = new Label
        {
            Text = "Vali tegevus ja vajuta nuppu",
            FontSize = 18,
            TextColor = Colors.Black
        };

        picker = new Picker
        {
            Title = "Vali tegevus",
            FontSize = 16,
            BackgroundColor = Color.FromRgb(200, 200, 100),
            TextColor = Colors.Black,
            ItemsSource = new List<string>
            {
                "Peida lumememm",
                "Näita lumememm",
                "Muuda värvi",
                "Sulata",
                "Tantsi"
            }
        };
        picker.SelectedIndexChanged += (s, e) =>
        {
            if (picker.SelectedIndex != -1)
            {
                mis_on_valitud.Text = $"Valitud: {picker.SelectedItem}";
            }
        };

        btn = new Button
        {
            Text = "Käivita",
            BackgroundColor = Color.FromRgb(200, 200, 100),
            TextColor = Colors.Black
        };
        btn.Clicked += Btn_Clicked;

        slider = new Slider
        {
            Minimum = 0.0,
            Maximum = 1.0,
            Value = 1.0,
            BackgroundColor = Color.FromRgb(200, 200, 100),
            ThumbColor = Colors.Black,
            MinimumTrackColor = Colors.Black,
            MaximumTrackColor = Colors.Black
        };
        slider.ValueChanged += (s, e) =>
        {
            // Прозрачность всего снеговика
            snowman.Opacity = e.NewValue;
        };

        stepper = new Stepper
        {
            Minimum = 100,
            Maximum = 2000,
            Increment = 100,
            Value = 600,
            BackgroundColor = Color.FromRgb(200, 200, 100),
            HorizontalOptions = LayoutOptions.Center
        };
        stepper.ValueChanged += (s, e) =>
        {
            mis_on_valitud.Text = $"Kiirus (ms): {e.NewValue}";
        };

        snowman = new AbsoluteLayout();

        keha = new Frame
        {
            BackgroundColor = Colors.White,
            CornerRadius = 999,
            HasShadow = false,
            BorderColor = Colors.LightGray
        };
        pea = new Frame
        {
            BackgroundColor = Colors.White,
            CornerRadius = 999,
            HasShadow = false,
            BorderColor = Colors.LightGray
        };
        aamber = new Frame
        {
            BackgroundColor = Colors.Sienna,
            CornerRadius = 6,
            HasShadow = false,
            Rotation = -10
        };

        snowman.Children.Add(keha);
        snowman.Children.Add(pea);
        snowman.Children.Add(aamber);

        // Позиции и размеры (доли) через AbsoluteLayoutFlags.All
        AbsoluteLayout.SetLayoutFlags(keha, AbsoluteLayoutFlags.All);
        AbsoluteLayout.SetLayoutBounds(keha, new Rect(0.5, 0.75, 0.5, 0.4)); // тело

        AbsoluteLayout.SetLayoutFlags(pea, AbsoluteLayoutFlags.All);
        AbsoluteLayout.SetLayoutBounds(pea, new Rect(0.5, 0.45, 0.32, 0.26)); // голова

        AbsoluteLayout.SetLayoutFlags(aamber, AbsoluteLayoutFlags.All);
        AbsoluteLayout.SetLayoutBounds(aamber, new Rect(0.43, 0.28, 0.18, 0.09)); // ведро

        al = new AbsoluteLayout { };

        // Добавляем элементы управления и снеговик
        al.Children.Add(mis_on_valitud);
        al.Children.Add(picker);
        al.Children.Add(btn);
        al.Children.Add(slider);
        al.Children.Add(stepper);
        al.Children.Add(snowman);

        // Раскладываем по экрану ровно и просто
        var elementid = new View[]
        {
            mis_on_valitud, picker, btn, slider, stepper, snowman
        };

        for (int i = 0; i < elementid.Length; i++)
        {
            // Верхние 5 элементов — по очереди; снеговику даём побольше места внизу
            if (elementid[i] == snowman)
            {
                AbsoluteLayout.SetLayoutBounds(snowman, new Rect(0.5, 0.78, 0.9, 0.6));
                AbsoluteLayout.SetLayoutFlags(snowman, AbsoluteLayoutFlags.All);
            }
            else
            {
                AbsoluteLayout.SetLayoutBounds(elementid[i], new Rect(0.5, 0.06 + i * 0.12, 0.9, 0.1));
                AbsoluteLayout.SetLayoutFlags(elementid[i], AbsoluteLayoutFlags.All);
            }
        }

        Content = al;
    }

    async void Btn_Clicked(object? sender, EventArgs e)
    {
        if (picker.SelectedIndex == -1)
        {
            await DisplayAlert("Tegevus", "Palun vali tegevus.", "OK");
            return;
        }

        string tegevus = picker.SelectedItem?.ToString() ?? "";
        uint ms = (uint)stepper.Value;

        switch (tegevus)
        {
            case "Peida lumememm":
                // можно скрыть полностью
                snowman.IsVisible = false;
                break;

            case "Näita lumememm":
                snowman.IsVisible = true;
                await snowman.FadeTo(1, ms);
                await snowman.ScaleTo(1, ms / 2);
                await snowman.TranslateTo(0, 0, ms / 2);
                break;

            case "Muuda värvi":
                {
                    bool ok = await DisplayAlert("Kinnita", "Kas muuta värvi?", "Jah", "Ei");
                    if (ok)
                    {
                        // случайный светлый цвет для «снега»
                        var c = Color.FromRgb(rnd.Next(220, 256), rnd.Next(220, 256), rnd.Next(220, 256));
                        pea.BackgroundColor = c;
                        keha.BackgroundColor = c;
                    }
                }
                break;

            case "Sulata":
                // «тает»: уменьшается и исчезает
                snowman.IsVisible = true;
                await snowman.FadeTo(0, ms);
                await snowman.ScaleTo(0.8, ms);
                break;

            case "Tantsi":
                // лёгкое движение влево-вправо и обратно
                snowman.IsVisible = true;
                await snowman.TranslateTo(-20, 0, ms / 2);
                await snowman.TranslateTo(20, 0, ms / 2);
                await snowman.TranslateTo(0, 0, ms / 2);
                break;

            default:
                await DisplayAlert("Tegevus", "Tundmatu tegevus.", "OK");
                break;
        }
    }
}