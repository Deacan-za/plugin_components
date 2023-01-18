using Interfaces.Lib;

namespace Mode2.Lib;

public class SelfDescribingMode : ISelfDescribingMode
{
    public int SupportedMode() => 2;
}