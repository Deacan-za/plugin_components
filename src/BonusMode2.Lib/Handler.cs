using System;
using Interfaces.Lib;

namespace BonusMode2.Lib
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
                        "This is in BonusMode2.Lib");
    }
  }
}