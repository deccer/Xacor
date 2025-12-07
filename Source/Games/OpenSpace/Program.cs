using Microsoft.Extensions.DependencyInjection;
using Xacor;
using Xacor.Game;

namespace OpenSpace;

internal static class Program
{
    public static void Main(string[] args)
    {
        ApplicationEntryPoint.Run(args,
            (services, configuration) =>
            {
                services.AddSingleton<IGame, Game>();
            });
    }
}