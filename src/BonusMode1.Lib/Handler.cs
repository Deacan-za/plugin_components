using System;
using Interfaces.Lib;

namespace BonusMode1.Lib
{
  public class Handler : IHandler
  {
    private readonly int _bonusMode;

    public Handler(int bonusMode)
    {
      _bonusMode = bonusMode;
    }

    public void ProcessRequest()
    {
      Console.WriteLine($"This is bonus mode {_bonusMode}'s handler\n" +
                        "This is in BonusMode1.Lib");
    }
  }
}