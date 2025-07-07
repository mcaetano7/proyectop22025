using Discord.Commands;
using Ucu.Poo.DiscordBot.Domain;
using Ucu.Poo.DiscordBot.Commands;
namespace Library;

/// <summary>
/// Comando que muestra el mapa ASCII en el bot.
/// </summary>
public class MapaCommand : ModuleBase<SocketCommandContext>
{
    [Command("mapa")]
    public async Task MostrarMapaAscii()
    {
        // Obtener el mapa en texto desde la fachada
        string mapa = Facade.Instance.VerMapaAscii();

        // Enviar el mapa como respuesta en Discord
        await ReplyAsync(mapa);
    }
}