using System.Threading.Tasks;
using Avalonia.Platform.Storage;

namespace Analog.Services;

public interface IFilesService
{
    public Task<IStorageFile?> OpenFileAsync();
}