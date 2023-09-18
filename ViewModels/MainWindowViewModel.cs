using ReactiveUI;

namespace Analog.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        ContentViewModel = new NormalizeViewModel();
    }

    private ViewModelBase contentViewModel;
    public ViewModelBase ContentViewModel
    {
        get => contentViewModel;
        protected set => this.RaiseAndSetIfChanged(ref contentViewModel, value);
    }

    public void Analyze()
    {
        ContentViewModel = new AnalyzeViewModel();
    }
}