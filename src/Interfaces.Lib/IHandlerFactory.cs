namespace Interfaces.Lib
{
  public interface IHandlerFactory
  {
    IHandler CreateHandler(int bonusMode);
  }
}