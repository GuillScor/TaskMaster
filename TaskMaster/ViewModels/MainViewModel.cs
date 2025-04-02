using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TaskMaster.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private int count;

    private string counterText = "Click me";
    public string CounterText
    {
        get => counterText;
        set => SetProperty(ref counterText, value);
    }

    public MainViewModel()
    {
        CounterText = "Click me"; // Valeur initiale
    }

    [RelayCommand]
    private void IncrementCounter()
    {
        Count++;
        CounterText = Count == 1 ? $"Clicked {Count} time" : $"Clicked {Count} times";
        SemanticScreenReader.Announce(CounterText); // Accessibilité
    }
}
