using Ucu.Poo.DiscordBot.Domain;
using Ucu.Poo.DiscordBot.Services;

namespace Program;

/// <summary>
/// Un programa que implementa un bot de Discord.
/// </summary>
internal static class Program
{
    /// <summary>
    /// Punto de entrada al programa.
    /// </summary>
   
    private static void Main()
    {
        //DemoFacade();
        DemoBot();
    }
    
    private static void DemoFacade()
    {
        Console.WriteLine(Facade.Instance.AddPlayerToWaitingList("player"));
        Console.WriteLine(Facade.Instance.AddPlayerToWaitingList("opponent"));
        Console.WriteLine(Facade.Instance.GetAllPlayersWaiting());
        Console.WriteLine(Facade.Instance.StartBattle("player", "opponent"));
        Console.WriteLine(Facade.Instance.GetAllPlayersWaiting());
    }

    private static void DemoBot()
    {
        BotLoader.LoadAsync().GetAwaiter().GetResult();
    }
    
}
