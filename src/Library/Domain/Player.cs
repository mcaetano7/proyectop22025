namespace Ucu.Poo.DiscordBot.Domain;

/// <summary>
/// Esta clase representa un jugador en el juego Pokémon.
/// </summary>
public class Player
{
    /// <summary>
    /// El nombre de usuario de Discord en el servidor del bot del jugador.
    /// </summary>
    public string DisplayName { get; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="Player"/> con el
    /// nombre de usuario de Discord que se recibe como argumento.
    /// </summary>
    /// <param name="displayName">El nombre de usuario de Discord.</param>
    public Player(string displayName)
    {
        this.DisplayName = displayName;
    }
}
