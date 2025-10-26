using System.Reflection;

namespace Sportik.Backend.Api.Helpers;

internal static class VersionHelper
{
    public static string GetVersion()
    {
        string version = Assembly
            .GetExecutingAssembly()
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion ?? "0.0.0";

        return version.Split('+')[0];
    }
}