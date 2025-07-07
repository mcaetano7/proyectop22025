using Discord.Commands;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

/// <summary>
/// Esta clase implementa el comando 'recursos' del bot.
/// Este comando muestra los recursos actuales del jugador en su partida activa.
/// </summary>
// ReSharper disable once UnusedType.Global
public class RecursosCommand : ModuleBase<SocketCommandContext>
{
    /// <summary>
    /// Implementa el comando 'recursos'.
    /// </summary>
    [Command("recursos")]
    [Summary("Muestra los recursos actuales del jugador en su partida activa")]
    // ReSharper disable once UnusedMember.Global
    public async Task ExecuteAsync()
    {
        string displayName = CommandHelper.GetDisplayName(Context);
        
        // Obtener la partida activa del jugador
        var partida = Facade.Instance.GetPartidaActiva(displayName);
        
        if (partida == null)
        {
            await ReplyAsync("No tienes una partida activa. Usa `!battle` para iniciar una partida.");
            return;
        }

        // Determinar si es el jugador 1 o 2
        var jugador = partida.Jugador1.Nombre == displayName ? partida.Jugador1 : partida.Jugador2;
        
        // Obtener los recursos del jugador
        var alimento = jugador.GetRecurso(Library.TipoRecurso.Alimento);
        var madera = jugador.GetRecurso(Library.TipoRecurso.Madera);
        var oro = jugador.GetRecurso(Library.TipoRecurso.Oro);
        var piedra = jugador.GetRecurso(Library.TipoRecurso.Piedra);

        // Verificar que es el turno del jugador
        if (!partida.TieneTurno(displayName))
        {
            string jugadorTurno = partida.ObtenerJugadorTurno();
            await ReplyAsync($"No es tu turno. Juega {jugadorTurno}");
            return;
        }

        // Crear el mensaje con los recursos
        var mensaje = $"**Recursos de {displayName}:**\n" +
                     $"🍖 Alimento: {alimento}\n" +
                     $"🪵 Madera: {madera}\n" +
                     $"💰 Oro: {oro}\n" +
                     $"🪨 Piedra: {piedra}";

        await ReplyAsync(mensaje);
    }
} 