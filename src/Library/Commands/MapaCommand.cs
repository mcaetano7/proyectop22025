using Discord.Commands;
using Ucu.Poo.DiscordBot.Domain;
using Ucu.Poo.DiscordBot.Commands;
namespace Library;

/// <summary>
/// Comando que muestra el mapa ASCII en el bot.
/// </summary>
public class MapaCommand : ModuleBase<SocketCommandContext>
{
    public string Name => "mapa";

    public string Execute(string[] args, string user)
    {
        return Facade.Instance.VerMapaAscii();
    }
}