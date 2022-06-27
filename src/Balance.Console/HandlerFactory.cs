using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Interfaces.Lib;

namespace Balance.ConsoleApp
{
  public class HandlerFactory 
  {
    public IHandler CreateHandler(int bonusMode)
    {
      var factory = GetBonusModeHandlerFactory(bonusMode);

      if (factory != null)
      {
        return factory.CreateHandler(bonusMode);
      }

      throw new NotImplementedException($"Bonus mode {bonusMode} is not supported");
    }

    private static IHandlerFactory GetBonusModeHandlerFactory(int bonusMode)
    {
      var assemblies = GetAssemblies();

      foreach (var assembly in assemblies)
      {
        var bonusModeType = GetRequestedType(assembly, typeof(ISelfDescribingBonusMode));

        if (bonusModeType?.FullName == null) continue;
        if (!(assembly.CreateInstance(bonusModeType.FullName) is ISelfDescribingBonusMode describedBonus)) continue;

        var factory = ResolveBonusModeHandlerFactory(describedBonus, bonusMode, assembly);
        if (factory == null) continue;
        return factory;
      }

      return null;
    }

    private static IHandlerFactory ResolveBonusModeHandlerFactory(ISelfDescribingBonusMode describedBonus, int bonusMode, Assembly assembly)
    {
      var supportedBonus = describedBonus.GetSupportedBonusMode();

      if (!IsBonusModeSupported(supportedBonus, bonusMode)) return null;

      var factoryType = GetRequestedType(assembly, typeof(IHandlerFactory));

      if (factoryType?.FullName == null) return null;

      if (assembly.CreateInstance(factoryType.FullName) is IHandlerFactory handlerFactory)
      {
        return handlerFactory;
      }

      return null;
    }

    private static bool IsBonusModeSupported(int supportedBonus, int requestedBonus)
    {
      return supportedBonus == requestedBonus;
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
}