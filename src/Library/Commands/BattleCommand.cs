using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

/// <summary>
/// Esta clase implementa el comando 'battle' del bot. Este comando une al
/// jugador que envía el mensaje con el oponente que se recibe como parámetro,
/// si lo hubiera, en una batalla; si no se recibe un oponente, lo une con
/// cualquiera que esté esperando para jugar.
/// </summary>
// ReSharper disable once UnusedType.Global
public class BattleCommand : ModuleBase<SocketCommandContext>
{
    /// <summary>
    /// Implementa el comando 'battle'. Este comando une al jugador que envía el
    /// mensaje a la lista de jugadores esperando para jugar.
    /// </summary>
    [Command("battle")]
    [Summary(
        """
        Une al jugador que envía el mensaje con el oponente que se recibe
        como parámetro, si lo hubiera, en una batalla; si no se recibe un
        oponente, lo une con cualquiera que esté esperando para jugar.
        """)]
    // ReSharper disable once UnusedMember.Global
    public async Task ExecuteAsync(
        [Remainder]
        [Summary("Display name del oponente, opcional")]
        string? opponentDisplayName = null)
    {
        string displayName = CommandHelper.GetDisplayName(Context);
        
        SocketGuildUser? opponentUser = CommandHelper.GetUser(
            Context, opponentDisplayName);

        string result;
        if (opponentUser != null)
        {
            result = Facade.Instance.StartBattle(displayName, opponentUser.DisplayName);
            await Context.Message.Channel.SendMessageAsync(result);
            
            // Mostrar quién empieza la partida
            var partida = Facade.Instance.GetPartidaActiva(displayName);
            if (partida != null)
            {
                string primerTurno = partida.ObtenerJugadorTurno();
                await Context.Message.Channel.SendMessageAsync($"**Empieza la partida!** {primerTurno} empieza primero.");
            }
        }
        else
        {
            result = $"No hay un usuario {opponentDisplayName}";
            await ReplyAsync(result);
        }
    }

    /// <summary>
    /// Comando para pasar el turno al siguiente jugador
    /// </summary>
    [Command("pasar")]
    [Summary("Pasa el turno al siguiente jugador")]
    public async Task PasarTurnoAsync()
    {
        string displayName = CommandHelper.GetDisplayName(Context);
        
        // Obtener la partida activa
        var partida = Facade.Instance.GetPartidaActiva(displayName);
        if (partida == null)
        {
            await ReplyAsync("**No estás en una partida activa.**");
            return;
        }

        // Verificar que es tu turno
        if (!partida.TieneTurno(displayName))
        {
            string jugadorTurno = partida.ObtenerJugadorTurno();
            await ReplyAsync($"**No es tu turno.** Juega {jugadorTurno}");
            return;
        }

        // Pasar el turno
        partida.SiguienteTurno();
        string nuevoJugador = partida.ObtenerJugadorTurno();
        
                        await ReplyAsync($"**Turno pasado.** Ahora juega {nuevoJugador}");
    }
}
