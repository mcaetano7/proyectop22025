using Discord.Commands;
using Discord.WebSocket;
using Ucu.Poo.DiscordBot.Domain;
using Library;

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
            await ReplyAsync($"No se encontró información para el jugador {userName}", false, null);
            return;
        }

        // Ejemplo de armado de mensaje informativo
        var info = $"Información del jugador: **{player.Nombre}**\n" +
                   $"Civilización: {player.Civilizacion.Name}\n" +
                   $"Bonificaciones: {string.Join(", ", player.Civilizacion.Bonificaciones)}\n" +
                   $"Recursos: Alimento={player.GetRecurso(TipoRecurso.Alimento)}, " +
                   $"Madera={player.GetRecurso(TipoRecurso.Madera)}, " +
                   $"Oro={player.GetRecurso(TipoRecurso.Oro)}, " +
                   $"Piedra={player.GetRecurso(TipoRecurso.Piedra)}\n";

        // Estado: esperando, en batalla, etc. (puedes mejorar esto según tu lógica)
        string estado = Facade.Instance.PlayerIsWaiting(userName).Contains("esperando") ? "Esperando" : "No esperando";
        info += $"Estado: {estado}";

        await ReplyAsync(info, false, null);
    }

    // Este método obtiene el jugador por nombre desde la partida activa
    private Player? ObtenerPlayerPorNombre(string nombre)
    {
        try
        {
            var partida = Facade.Instance.GetPartidaActiva(nombre);
            if (partida != null)
            {
                // Determinar si es el jugador 1 o 2
                if (partida.Jugador1?.Nombre == nombre)
                {
                    return partida.Jugador1;
                }
                else if (partida.Jugador2?.Nombre == nombre)
                {
                    return partida.Jugador2;
                }
            }
            return null;
        }
        catch (Exception)
        {
            return null;
        }
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
            await ReplyAsync(info, false, null);
            return;
        }

        // Obtener el jugador
        var jugador = ObtenerPlayerPorNombre(displayName);
        if (jugador == null)
        {
            info += "**Error:** No se pudo encontrar tu jugador en la partida.";
            await ReplyAsync(info, false, null);
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
                $"🍖 Alimento: {jugador.GetRecurso(TipoRecurso.Alimento)}\n" +
                $"🪵 Madera: {jugador.GetRecurso(TipoRecurso.Madera)}\n" +
                $"💰 Oro: {jugador.GetRecurso(TipoRecurso.Oro)}\n" +
                $"🪨 Piedra: {jugador.GetRecurso(TipoRecurso.Piedra)}\n" +
                $"**Población:** {jugador.PoblacionActual}/{jugador.PoblacionMaxima}";

        await ReplyAsync(info, false, null);
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
            await ReplyAsync("**No hay partidas activas.**", false, null);
            return;
        }

        var info = "**Partidas Activas:**\n";
        foreach (var partida in partidas)
        {
            info += $"• **{partida.Jugador1.Nombre}** vs **{partida.Jugador2.Nombre}**\n";
        }

        await ReplyAsync(info, false, null);
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
                           "Usa `!join` para unirte a la lista de espera y luego `!battle` para iniciar una partida.", false, null);
            return;
        }

        try
        {
            // Obtener el mapa base desde la partida
            string? mapaAscii = Facade.Instance.VerMapaAscii(displayName);
            
            if (string.IsNullOrEmpty(mapaAscii))
            {
                await ReplyAsync("**Error:** No se pudo generar el mapa.", false, null);
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
                        await ReplyAsync(mensaje, false, null);
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
                    await ReplyAsync(mensaje, false, null);
                }
            }
            else
            {
                await ReplyAsync("**Mapa del Juego:**\n```\n" + mapaAscii + "\n```", false, null);
            }
        }
        catch (Exception ex)
        {
            await ReplyAsync($"**Error al mostrar el mapa:** {ex.Message}", false, null);
        }
    }

    /// <summary>
    /// Comando para diagnosticar problemas de construcción
    /// </summary>
    [Command("diagnostico-construccion")]
    [Summary("Diagnóstico específico para problemas de construcción")]
    public async Task DiagnosticoConstruccionAsync()
    {
        string displayName = CommandHelper.GetDisplayName(Context);
        
        // Verificar si está en una partida
        var partida = Facade.Instance.GetPartidaActiva(displayName);
        if (partida == null)
        {
            await ReplyAsync("**No estás en una partida activa.**\n" +
                           "Usa `!join` para unirte a la lista de espera y luego `!battle` para iniciar una partida.", false, null);
            return;
        }

        // Obtener el jugador
        var jugador = ObtenerPlayerPorNombre(displayName);
        if (jugador == null)
        {
            await ReplyAsync("**Error:** No se pudo encontrar tu jugador en la partida.", false, null);
            return;
        }

        // Verificar si es tu turno
        bool esMiTurno = partida.TieneTurno(displayName);
        string jugadorTurno = partida.ObtenerJugadorTurno();

        // Información de diagnóstico
        var info = $"🔧 **Diagnóstico de Construcción:**\n" +
                   $"**Jugador:** {displayName}\n" +
                   $"**¿Es mi turno?:** {(esMiTurno ? "Sí" : "No")}\n" +
                   $"**Turno actual:** {jugadorTurno}\n" +
                   $"**Recursos disponibles:**\n" +
                   $"🍖 Alimento: {jugador.GetRecurso(TipoRecurso.Alimento)}\n" +
                   $"🪵 Madera: {jugador.GetRecurso(TipoRecurso.Madera)}\n" +
                   $"💰 Oro: {jugador.GetRecurso(TipoRecurso.Oro)}\n" +
                   $"🪨 Piedra: {jugador.GetRecurso(TipoRecurso.Piedra)}\n" +
                   $"**Población:** {jugador.PoblacionActual}/{jugador.PoblacionMaxima}\n\n" +
                   $"**Costos de construcción:**\n" +
                   $"Casa: 25 Madera\n" +
                   $"Centro Cívico: 200 Madera\n" +
                   $"Almacén: 500 Madera\n\n" +
                   $"**Comandos útiles:**\n" +
                   $"• `!construir casa 5 5` - Construir una casa\n" +
                   $"• `!construir centrocivico 10 10` - Construir centro cívico\n" +
                   $"• `!construir almacen 15 15` - Construir almacén\n" +
                   $"• `!recolectar madera 5 5` - Recolectar madera";

        await ReplyAsync(info, false, null);
    }

    /// <summary>
    /// Comando para probar la construcción de una casa (solo para diagnóstico)
    /// </summary>
    [Command("probar-casa")]
    [Summary("Prueba la construcción de una casa en coordenadas específicas")]
    public async Task ProbarCasaAsync(int x = 5, int y = 5)
    {
        string displayName = CommandHelper.GetDisplayName(Context);
        
        // Verificar si está en una partida
        var partida = Facade.Instance.GetPartidaActiva(displayName);
        if (partida == null)
        {
            await ReplyAsync("**No estás en una partida activa.**\n" +
                           "Usa `!join` para unirte a la lista de espera y luego `!battle` para iniciar una partida.", false, null);
            return;
        }

        // Obtener el jugador
        var jugador = ObtenerPlayerPorNombre(displayName);
        if (jugador == null)
        {
            await ReplyAsync("**Error:** No se pudo encontrar tu jugador en la partida.", false, null);
            return;
        }

        // Verificar si es tu turno
        if (!partida.TieneTurno(displayName))
        {
            string jugadorTurno = partida.ObtenerJugadorTurno();
            await ReplyAsync($"**No es tu turno.** Juega {jugadorTurno}", false, null);
            return;
        }

        try
        {
            // Verificar que el jugador no sea null
            if (jugador == null)
            {
                await ReplyAsync("**Error:** No se pudo obtener información del jugador.", false, null);
                return;
            }

            // Crear una casa de prueba
            var ubicacion = new Coordenada(x, y);
            var casa = new Casa(ubicacion, 50, jugador, "Casa", 5);
            
            // Mostrar información antes de la construcción
            var maderaAntes = jugador.GetRecurso(TipoRecurso.Madera);
            var poblacionAntes = jugador.PoblacionMaxima;
            var tieneRecursos = jugador.TieneRecursos(casa.ObtenerCosto());
            
            await ReplyAsync($"**Antes de construir:**\n" +
                           $"• Madera: {maderaAntes}\n" +
                           $"• Población máxima: {poblacionAntes}\n" +
                           $"• Costo casa: 25 Madera\n" +
                           $"• ¿Tiene recursos?: {tieneRecursos}", false, null);

            // Intentar construir directamente
            bool construccionExitosa = jugador.Construir(casa, ubicacion);
            
            // Mostrar información después de la construcción
            var maderaDespues = jugador.GetRecurso(TipoRecurso.Madera);
            var poblacionDespues = jugador.PoblacionMaxima;
            
            await ReplyAsync($"**Después de construir:**\n" +
                           $"• Madera: {maderaDespues}\n" +
                           $"• Población máxima: {poblacionDespues}\n" +
                           $"• ¿Construcción exitosa?: {(construccionExitosa ? "Sí" : "No")}\n" +
                           $"• Madera gastada: {maderaAntes - maderaDespues}\n" +
                           $"• Población aumentada: {poblacionDespues - poblacionAntes}\n" +
                           $"• ¿Tiene recursos ahora?: {jugador.TieneRecursos(casa.ObtenerCosto())}", false, null);

            if (construccionExitosa)
            {
                await ReplyAsync($"**¡Casa construida exitosamente en ({x},{y})!**", false, null);
            }
            else
            {
                await ReplyAsync($"**No se pudo construir la casa.**\n" +
                               $"Verifica que tengas al menos 25 madera.", false, null);
            }
        }
        catch (Exception ex)
        {
            await ReplyAsync($"**Error en la prueba:** {ex.Message}\n" +
                           $"Stack trace: {ex.StackTrace}", false, null);
        }
    }
}
