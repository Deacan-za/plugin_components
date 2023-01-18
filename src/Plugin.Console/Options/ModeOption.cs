using System.Collections.Generic;

namespace Plugin.ConsoleApp.Options;

public sealed class ModeOption
{
    public IEnumerable<int> SupportedModes { get; set; }
}