using System;
using System.Collections.Generic;
using System.Linq;

namespace Balance.ConsoleApp
{
  internal class Program
  {
    private static void Main()
    {
      var bonusModes = new List<int> {1, 103};

      var factory = new HandlerFactory();

      foreach (var handler in bonusModes.Select(bonusMode => factory.CreateHandler(bonusMode)))
      {
        handler.ProcessRequest();
        Console.WriteLine("");
      }

      Console.ReadKey(true);
    }
  }
}
