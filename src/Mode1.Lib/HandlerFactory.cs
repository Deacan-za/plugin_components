using Interfaces.Lib;

namespace Mode1.Lib
{
  public class HandlerFactory : IHandlerFactory
  {
    public IHandler CreateHandler(int mode)
    {
      return new Handler(mode);
    }
  }
}