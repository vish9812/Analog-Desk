using System.Collections.Generic;
using System.Text.Json;

namespace Analog.Models;

public static class FileModel
{
    public static List<string> TextLines { get; set; } = new();
    public static List<JsonElement> JsonLines { get; set; } = new();
}