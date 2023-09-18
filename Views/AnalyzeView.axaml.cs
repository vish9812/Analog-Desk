using System.Threading.Tasks;
using Analog.ViewModels;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace Analog.Views;

public partial class AnalyzeView : ReactiveUserControl<AnalyzeViewModel>
{
    public AnalyzeView()
    {
        InitializeComponent();

        this.WhenActivated(action =>
         action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
    }

    private async Task DoShowDialogAsync(InteractionContext<FullDataViewModel,
                                        bool> interaction)
    {
        var dialog = new FullDataWindow
        {
            DataContext = interaction.Input
        };

        var win = TopLevel.GetTopLevel(this) as Window;
        if (win is null)
        {
            throw new System.Exception("Did not get right window");
        }

        var result = await dialog.ShowDialog<bool>(win);
        interaction.SetOutput(result);
    }
}