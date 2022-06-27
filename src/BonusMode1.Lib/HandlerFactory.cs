using Interfaces.Lib;

namespace BonusMode1.Lib
{
  public class HandlerFactory : IHandlerFactory
  {
    public IHandler CreateHandler(int bonusMode)
    {
      return new Handler(bonusMode);
    }
  }
}