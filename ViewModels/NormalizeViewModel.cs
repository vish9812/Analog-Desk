using System;
using System.IO;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Analog.Models;
using Analog.Services;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;

namespace Analog.ViewModels;

public class NormalizeViewModel : ViewModelBase
{
    public NormalizeViewModel()
    {
        OpenFileAsyncCommand = ReactiveCommand.CreateFromTask(OpenFileAsync);
        // AnalyzeCommand = ReactiveCommand.Create(() =>
        // {
        //     ContentViewModel = new AnalyzeViewModel();
        //     var a = 2;
        // });
    }

    private string fileName;
    public string FileName
    {
        get => fileName;
        set => this.RaiseAndSetIfChanged(ref fileName, value);
    }

    public ICommand OpenFileAsyncCommand { get; }
    // public ICommand AnalyzeCommand { get; }

    private async Task OpenFileAsync(CancellationToken token)
    {
        var filesService = (App.Current?.Services?.GetService<IFilesService>()) ?? throw new NullReferenceException("Missing File Service instance.");

        var file = await filesService.OpenFileAsync();
        if (file is null) return;

        // Limit the file to 150MB
        if ((await file.GetBasicPropertiesAsync()).Size > 1024 * 1024 * 150)
        {
            throw new Exception("File exceeded 150MB limit.");
        }

        await using var readStream = await file.OpenReadAsync();
        using var reader = new StreamReader(readStream);
        string? line;
        while ((line = await reader.ReadLineAsync(token)) != null)
        {
            FileModel.TextLines.Add(line);
        }
    }
}