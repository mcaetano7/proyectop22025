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
        string estado = Facade.Instance.TrainerIsWaiting(userName).Contains("esperando") ? "Esperando" : "No esperando";
        info += $"Estado: {estado}";

        await ReplyAsync(info);
    }

    // Este método es un placeholder. Debes implementarlo según tu lógica de dominio.
    private Library.Player? ObtenerPlayerPorNombre(string nombre)
    {
        // TODO: Implementar la búsqueda real del jugador en tu sistema
        return null;
    }
}
