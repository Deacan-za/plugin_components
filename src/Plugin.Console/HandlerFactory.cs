using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Interfaces.Lib;

namespace Plugin.ConsoleApp;

public class HandlerFactory : IHandlerFactory
{
    public IHandler CreateHandler(int mode)
    {
        var factory = GetModeHandlerFactory(mode);

        if (factory != null)
        {
            return factory.CreateHandler(mode);
        }

        throw new NotImplementedException($"Mode {mode} is not supported");
    }

    private static IHandlerFactory GetModeHandlerFactory(int mode)
    {
        var assemblies = GetAssemblies();

        foreach (var assembly in assemblies)
        {
            var modeType = GetRequestedType(assembly, typeof(ISelfDescribingMode));

            if (modeType?.FullName == null) continue;
            if (assembly.CreateInstance(modeType.FullName) is not ISelfDescribingMode describedMode) continue;

            var factory = ResolveModeHandlerFactory(describedMode, mode, assembly);
            if (factory == null) continue;
            return factory;
        }

        return null;
    }

    private static IHandlerFactory ResolveModeHandlerFactory(ISelfDescribingMode mode, int requestedMode, Assembly assembly)
    {
        var supportedMode = mode.SupportedMode();

        if (!IsModeSupported(supportedMode, requestedMode)) return null;

        var factoryType = GetRequestedType(assembly, typeof(IHandlerFactory));

        if (factoryType?.FullName == null) return null;

        if (assembly.CreateInstance(factoryType.FullName) is IHandlerFactory handlerFactory)
        {
            return handlerFactory;
        }

        return null;
    }

    private static bool IsModeSupported(int supportedMode, int requestedMode)
    {
        return supportedMode == requestedMode;
    }

    private static Type GetRequestedType(Assembly assembly, Type type)
    {
        return assembly
            .GetTypes().FirstOrDefault(x => type
                .IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);
    }

    private static IEnumerable<Assembly> GetAssemblies()
    {
        return Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
            .Select(assemblyPath => AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyPath))
            .ToArray();
    }
}