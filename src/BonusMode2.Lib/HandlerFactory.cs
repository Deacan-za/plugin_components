using Interfaces.Lib;

namespace BonusMode2.Lib
{
  public class HandlerFactory : IHandlerFactory
  {
    public IHandler CreateHandler(int bonusMode)
    {
      return new Handler(bonusMode);
    }
  }
}