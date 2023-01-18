using Interfaces.Lib;

namespace Mode2.Lib;

public class HandlerFactory : IHandlerFactory
{
    public IHandler CreateHandler(int mode)
    {
        return new Handler(mode);
    }
}
