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
<<<<<<< HEAD:src/Program/Program.cs
   
    private static void Main()
=======
    class Program1
>>>>>>> cefd40a909befe5ef240c37fbb68d281da25074f:src/Program/Program1.cs
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
