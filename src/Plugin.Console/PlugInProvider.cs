using System;
using System.Collections.Generic;
using Interfaces.Lib;
using Microsoft.Extensions.Options;
using Plugin.ConsoleApp.Options;

namespace Plugin.ConsoleApp;

public class PlugInProvider : IPlugInProvider
{
    private readonly ModeOption _modeConfiguration;
    private readonly IHandlerFactory _handlerFactory;

    private readonly Dictionary<int, IHandler> _handlers = new();

    public PlugInProvider(IOptions<ModeOption> option, IHandlerFactory handlerFactory)
    {
        _handlerFactory = handlerFactory;
        _modeConfiguration = option.Value;
    }

    public void RegisterPlugIns()
    {
        foreach (var mode in _modeConfiguration.SupportedModes)
        {
            if (_handlers.ContainsKey(mode))
            {
                break;
            }
            _handlers.Add(mode, _handlerFactory.CreateHandler(mode));
        }
    }

    public IHandler GetHandler(int mode)
    {
        _ = _handlers.TryGetValue(mode, out var handler);

        return handler ?? throw new NullReferenceException();
    }
}


internal interface IPlugInProvider
{
    void RegisterPlugIns();

    IHandler GetHandler(int mode);
}