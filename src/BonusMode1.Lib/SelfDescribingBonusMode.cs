using Interfaces.Lib;

namespace BonusMode1.Lib
{
  public class SelfDescribingBonusMode : ISelfDescribingBonusMode
  {
    public int GetSupportedBonusMode()
    {
      return 103;
    }
  }
}