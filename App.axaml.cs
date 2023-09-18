using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Analog.ViewModels;
using Analog.Views;
using Microsoft.Extensions.DependencyInjection;
using Analog.Services;
using System;

namespace Analog;

public partial class App : Application
{
    public IServiceProvider? Services { get; private set; }

    public new static App? Current => Application.Current as App;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };

            var services = new ServiceCollection();

            services.AddSingleton<IFilesService>(x => new FilesService(desktop.MainWindow));

            Services = services.BuildServiceProvider();
        }

        base.OnFrameworkInitializationCompleted();
    }
}