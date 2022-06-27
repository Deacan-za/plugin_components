using Interfaces.Lib;

namespace BonusMode2.Lib
{
  public class SelfDescribingBonusMode : ISelfDescribingBonusMode
  {
    public int GetSupportedBonusMode()
    {
      return 1;
    }
  }
}