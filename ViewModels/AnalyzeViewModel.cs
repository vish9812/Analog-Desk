using System.Linq;
using System.Reactive.Linq;
using System.Collections.ObjectModel;
using System.Text.Json;
using Analog.Models;
using System.Collections.Generic;
using ReactiveUI;
using System.Reactive;
using System.Windows.Input;

namespace Analog.ViewModels;

public class AnalyzeViewModel : ViewModelBase
{
    public AnalyzeViewModel()
    {
        var fieldsSet = new HashSet<Field>();
        foreach (string line in FileModel.TextLines)
        {
            using var document = JsonDocument.Parse(line);
            var root = document.RootElement.Clone();
            FileModel.JsonLines.Add(root);

            foreach (var prop in root.EnumerateObject())
            {
                fieldsSet.Add(new Field
                {
                    Name = prop.Name,
                    IsChecked = prop.NameEquals("timestamp") || prop.NameEquals("msg") || prop.NameEquals("level"),
                });
            }
        }

        Fields = new(fieldsSet);

        Logs = new(
            FileModel.JsonLines.Select(entry => new LogData
            {
                Data = entry.GetRawText(),
                Level = entry.GetProperty("level").GetString(),
                Message = entry.GetProperty("msg").GetString(),
                Timestamp = entry.GetProperty("timestamp").GetString(),
            })
        );

        ShowDialog = new();

        ShowFullDataCommand = ReactiveCommand.CreateFromTask(async (string log) =>
        {
            var fullData = new FullDataViewModel { Text = log };
            var result = await ShowDialog.Handle(fullData);
            var a = 1;
        });
    }

    public ObservableCollection<LogData> Logs { get; }
    public ObservableCollection<Field> Fields { get; }

    public ICommand ShowFullDataCommand { get; }

    public Interaction<FullDataViewModel, bool> ShowDialog { get; }
}