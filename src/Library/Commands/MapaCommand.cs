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
    [Summary("Muestra el mapa ASCII.")]
    public async Task MapaAsync()
    {
        string? mapa = Facade.Instance.VerMapaAscii();

        if (string.IsNullOrEmpty(mapa))
        {
            await ReplyAsync("No se pudo generar el mapa.");
        }
        else
        {
            if (mapa.Length > 1990)
            {
                await ReplyAsync("El mapa es demasiado grande para mostrar.");
            }
            else
            {
                await ReplyAsync("```" + mapa + "```"); 
            }
        }
    }
}