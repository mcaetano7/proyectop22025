using System.Collections.ObjectModel;
using System.Linq;

namespace Ucu.Poo.DiscordBot.Domain;

/// <summary>
/// Esta clase representa la lista de jugadores esperando para jugar.
/// </summary>
public class WaitingList
{
    private readonly List<Player> Players = new List<Player>();

    public int Count
    {
        get { return this.Players.Count; }
    }

    public ReadOnlyCollection<Player> GetAllWaiting()
    {
        return this.Players.AsReadOnly();
    }
    
    /// <summary>
    /// Agrega un jugador a la lista de espera.
    /// </summary>
    /// <param name="displayName">El nombre de usuario de Discord en el servidor
    /// del bot a agregar.
    /// </param>
    /// <returns><c>true</c> si se agrega el usuario; <c>false</c> en caso
    /// contrario.</returns>
    public bool AddPlayer(string displayName)
    {
        if (string.IsNullOrEmpty(displayName))
        {
            throw new ArgumentException(nameof(displayName));
        }
        
        if (this.FindPlayerByDisplayName(displayName) != null) return false;
        Players.Add(new Player(displayName));
        return true;

    }

    /// <summary>
    /// Remueve un jugador de la lista de espera.
    /// </summary>
    /// <param name="displayName">El nombre de usuario de Discord en el servidor
    /// del bot a remover.
    /// </param>
    /// <returns><c>true</c> si se remueve el usuario; <c>false</c> en caso
    /// contrario.</returns>
    public bool RemovePlayer(string displayName)
    {
        Player? Player = this.FindPlayerByDisplayName(displayName);
        if (Player == null) return false;
        Players.Remove(Player);
        return true;

    }

    /// <summary>
    /// Busca un jugador por el nombre de usuario de Discord en el servidor del
    /// bot.
    /// </summary>
    /// <param name="displayName">El nombre de usuario de Discord en el servidor
    /// del bot a buscar.
    /// </param>
    /// <returns>El jugador encontrado o <c>null</c> en caso contrario.
    /// </returns>
    public Player? FindPlayerByDisplayName(string displayName)
    {
        foreach (Player Player in this.Players)
        {
            if (Player.DisplayName == displayName)
            {
                return Player;
            }
        }

        return null;
    }

    /// <summary>
    /// Retorna un jugador cualquiera esperando para jugar. En esta
    /// implementación provista no es cualquiera, sino el primero. En la
    /// implementación definitiva, debería ser uno aleatorio.
    /// 
    /// </summary>
    /// <returns></returns>
    public Player? GetAnyoneWaiting()
    {
        if (this.Players.Count == 0)
        {
            return null;
        }

        return this.Players[0];
    }
}
