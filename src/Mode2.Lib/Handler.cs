using System;
using Interfaces.Lib;

namespace Mode2.Lib;

internal class Handler : IHandler
{
    private readonly int _mode;

    public Handler(int mode)
    {
        _mode = mode;
    }

    public void ProcessRequest()
    {
        Console.WriteLine($"This is mode {_mode}'s handler\n" +
                          "This is in Mode2.Lib");
    }
}