using Discord.Commands;
using Discord.WebSocket;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

/// <summary>
/// Esta clase implementa el comando 'playerinfo', alias 'jugador' o 'infojugador' del bot.
/// Este comando retorna información sobre el jugador que envía el mensaje o sobre
/// otro jugador si se incluye como parámetro.
/// </summary>
// ReSharper disable once UnusedType.Global
public class PlayerInfoCommand : ModuleBase<SocketCommandContext>
{
    /// <summary>
    /// Implementa el comando 'playerinfo', alias 'jugador' o 'infojugador' del bot.
    /// </summary>
    /// <param name="displayName">El nombre de usuario de Discord a buscar.</param>
    [Command("playerinfo")]
    [Alias("jugador", "infojugador")]
    [Summary(
        """
        Devuelve información sobre el jugador que se indica como parámetro o
        sobre el jugador que envía el mensaje si no se indica otro usuario.
        """)]
    // ReSharper disable once UnusedMember.Global
    public async Task ExecuteAsync(
        [Remainder][Summary("El usuario del que tener información, opcional")]
        string? displayName = null)
    {
        string userName = displayName ?? CommandHelper.GetDisplayName(Context);

        // Aquí deberías obtener el Player real de tu lógica de dominio.
        // Por simplicidad, se simula la obtención de datos.
        // TODO: Reemplazar por la obtención real del Player desde la lógica de dominio.
        var player = ObtenerPlayerPorNombre(userName); // Implementa este método según tu lógica
        if (player == null)
        {
            await ReplyAsync($"No se encontró información para el jugador {userName}");
            return;
        }

        // Ejemplo de armado de mensaje informativo
        var info = $"Información del jugador: **{player.Nombre}**\n" +
                   $"Civilización: {player.Civilizacion.Name}\n" +
                   $"Bonificaciones: {string.Join(", ", player.Civilizacion.Bonificaciones)}\n" +
                   $"Recursos: Alimento={player.GetRecurso(Library.TipoRecurso.Alimento)}, " +
                   $"Madera={player.GetRecurso(Library.TipoRecurso.Madera)}, " +
                   $"Oro={player.GetRecurso(Library.TipoRecurso.Oro)}, " +
                   $"Piedra={player.GetRecurso(Library.TipoRecurso.Piedra)}\n";

        // Estado: esperando, en batalla, etc. (puedes mejorar esto según tu lógica)
        string estado = Facade.Instance.PlayerIsWaiting(userName).Contains("esperando") ? "Esperando" : "No esperando";
        info += $"Estado: {estado}";

        await ReplyAsync(info);
    }

    // Este método es un placeholder. Debes implementarlo según tu lógica de dominio.
    private Library.Player? ObtenerPlayerPorNombre(string nombre)
    {
        var partida = Facade.Instance.GetPartidaActiva(nombre);
        if (partida != null)
        {
            // Determinar si es el jugador 1 o 2
            return partida.Jugador1.Nombre == nombre ? partida.Jugador1 : partida.Jugador2;
        }
        return null;
    }

    /// <summary>
    /// Comando para verificar el estado del jugador (útil para diagnóstico)
    /// </summary>
    [Command("estado")]
    [Summary("Muestra el estado actual del jugador para diagnóstico")]
    public async Task EstadoAsync()
    {
        string displayName = CommandHelper.GetDisplayName(Context);
        
        // Mostrar información de diagnóstico
        var info = $"**Diagnóstico de nombres:**\n" +
                   $"• Username: {Context.User.Username}\n" +
                   $"• DisplayName: {displayName}\n" +
                   $"• GlobalName: {Context.User.GlobalName}\n";
        
        // Verificar si está en una partida
        var partida = Facade.Instance.GetPartidaActiva(displayName);
        if (partida == null)
        {
            info += "**No estás en una partida activa.**\n" +
                   "Usa `!join` para unirte a la lista de espera y luego `!battle` para iniciar una partida.";
            await ReplyAsync(info);
            return;
        }

        // Obtener el jugador
        var jugador = ObtenerPlayerPorNombre(displayName);
        if (jugador == null)
        {
            info += "**Error:** No se pudo encontrar tu jugador en la partida.";
            await ReplyAsync(info);
            return;
        }

        // Verificar si es tu turno
        bool esMiTurno = partida.TieneTurno(displayName);
        string jugadorTurno = partida.ObtenerJugadorTurno();

        // Mostrar información detallada
        info += $"**Estado del Jugador:** {displayName}\n" +
                $"**Civilización:** {jugador.Civilizacion.Name}\n" +
                $"**Turno actual:** {jugadorTurno}\n" +
                $"**¿Es mi turno?:** {(esMiTurno ? " Sí" : " No")}\n" +
                $"**Recursos:**\n" +
                $"🍖 Alimento: {jugador.GetRecurso(Library.TipoRecurso.Alimento)}\n" +
                $"🪵 Madera: {jugador.GetRecurso(Library.TipoRecurso.Madera)}\n" +
                $"💰 Oro: {jugador.GetRecurso(Library.TipoRecurso.Oro)}\n" +
                $"🪨 Piedra: {jugador.GetRecurso(Library.TipoRecurso.Piedra)}\n" +
                $"**Población:** {jugador.PoblacionActual}/{jugador.PoblacionMaxima}";

        await ReplyAsync(info);
    }

    /// <summary>
    /// Comando para listar todas las partidas activas (solo para administradores)
    /// </summary>
    [Command("partidas")]
    [Summary("Lista todas las partidas activas (solo para diagnóstico)")]
    public async Task ListarPartidasAsync()
    {
        string displayName = CommandHelper.GetDisplayName(Context);
        
        // Obtener todas las partidas activas
        var partidas = Ucu.Poo.DiscordBot.Domain.Facade.Instance.GetTodasLasPartidas();
        
        if (partidas.Count == 0)
        {
            await ReplyAsync("**No hay partidas activas.**");
            return;
        }

        var info = "**Partidas Activas:**\n";
        foreach (var partida in partidas)
        {
            info += $"• **{partida.Jugador1.Nombre}** vs **{partida.Jugador2.Nombre}**\n";
        }

        await ReplyAsync(info);
    }

    /// <summary>
    /// Comando para mostrar el mapa del juego
    /// </summary>
    [Command("mapa")]
    [Summary("Muestra el mapa actual del juego")]
    public async Task MostrarMapaAsync()
    {
        string displayName = CommandHelper.GetDisplayName(Context);
        
        // Verificar si está en una partida
        var partida = Facade.Instance.GetPartidaActiva(displayName);
        if (partida == null)
        {
            await ReplyAsync("**No estás en una partida activa.**\n" +
                           "Usa `!join` para unirte a la lista de espera y luego `!battle` para iniciar una partida.");
            return;
        }

        try
        {
            // Obtener el mapa base desde la partida
            string? mapaAscii = Facade.Instance.VerMapaAscii(displayName);
            
            if (string.IsNullOrEmpty(mapaAscii))
            {
                await ReplyAsync("**Error:** No se pudo generar el mapa.");
                return;
            }

            // Discord tiene límites de caracteres, así que dividimos el mapa si es muy largo
            if (mapaAscii.Length > 1900) // Dejamos margen para el formato
            {
                var partes = mapaAscii.Split('\n');
                var mensaje = "**Mapa del Juego:**\n```\n";
                
                foreach (var linea in partes)
                {
                    if ((mensaje + linea + "\n").Length > 1900)
                    {
                        mensaje += "```";
                        await ReplyAsync(mensaje);
                        mensaje = "```\n" + linea + "\n";
                    }
                    else
                    {
                        mensaje += linea + "\n";
                    }
                }
                
                if (mensaje.Length > 3)
                {
                    mensaje += "```";
                    await ReplyAsync(mensaje);
                }
            }
            else
            {
                await ReplyAsync("**Mapa del Juego:**\n```\n" + mapaAscii + "\n```");
            }
        }
        catch (Exception ex)
        {
            await ReplyAsync($"**Error al mostrar el mapa:** {ex.Message}");
        }
    }
}
