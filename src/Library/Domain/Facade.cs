namespace Ucu.Poo.DiscordBot.Domain;


/// <summary>
/// Esta clase recibe las acciones y devuelve los resultados que permiten
/// implementar las historias de usuario. Otras clases que implementan el bot
/// usan esta <see cref="Facade"/> pero no conocen el resto de las clases del
/// dominio. Esta clase es un singleton.
/// </summary>
public class Facade
{
    private static Facade? _instance;

    // Este constructor privado impide que otras clases puedan crear instancias
    // de esta.
    private Facade()
    {
        this.WaitingList = new WaitingList();
        this.PartidasActivas = new Dictionary<string, Library.Facade>();
    }

    /// <summary>
    /// Obtiene la única instancia de la clase <see cref="Facade"/>.
    /// </summary>
    public static Facade Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Facade();
            }

            return _instance;
        }
    }

    /// <summary>
    /// Inicializa este singleton. Es necesario solo en los tests.
    /// </summary>
    public static void Reset()
    {
        _instance = null;
    }
    
    private WaitingList WaitingList { get; }

    /// <summary>
    /// Diccionario que almacena las partidas activas, donde la clave es el ID de la partida
    /// </summary>
    private Dictionary<string, Library.Facade> PartidasActivas { get; }

    /// <summary>
    /// Agrega un jugador a la lista de espera.
    /// </summary>
    /// <param name="displayName">El nombre del jugador.</param>
    /// <returns>Un mensaje con el resultado.</returns>
    public string AddPlayerToWaitingList(string displayName)
    {
        if (this.WaitingList.AddPlayer(displayName))
        {
            return $"{displayName} agregado a la lista de espera";
        }
        
        return $"{displayName} ya está en la lista de espera";
    }

    /// <summary>
    /// Remueve un jugador de la lista de espera.
    /// </summary>
    /// <param name="displayName">El jugador a remover.</param>
    /// <returns>Un mensaje con el resultado.</returns>
    public string RemovePlayerFromWaitingList(string displayName)
    {
        if (this.WaitingList.RemovePlayer(displayName))
        {
            return $"{displayName} removido de la lista de espera";
        }
        else
        {
            return $"{displayName} no está en la lista de espera";
        }
    }

    /// <summary>
    /// Obtiene la lista de jugadores esperando.
    /// </summary>
    /// <returns>Un mensaje con el resultado.</returns>
    public string GetAllPlayersWaiting()
    {
        if (this.WaitingList.Count == 0)
        {
            return "No hay nadie esperando";
        }

        string result = "Esperan: ";
        foreach (Player Player in this.WaitingList.GetAllWaiting())
        {
            result = result + Player.DisplayName + "; ";
        }
        
        return result;
    }

    /// <summary>
    /// Determina si un jugador está esperando para jugar.
    /// </summary>
    /// <param name="displayName">El jugador.</param>
    /// <returns>Un mensaje con el resultado.</returns>
    public string PlayerIsWaiting(string displayName)
    {
        Player? Player = this.WaitingList.FindPlayerByDisplayName(displayName);
        if (Player == null)
        {
            return $"{displayName} no está esperando";
        }
        
        return $"{displayName} está esperando";
    }

    /// <summary>
    /// Obtiene la partida activa de un jugador
    /// </summary>
    /// <param name="playerDisplayName">Nombre del jugador</param>
    /// <returns>La partida activa o null si no está en ninguna</returns>
    public Library.Facade? GetPartidaActiva(string playerDisplayName)
    {
        return PartidasActivas.Values.FirstOrDefault(p => 
            p.Jugador1.Nombre == playerDisplayName || p.Jugador2.Nombre == playerDisplayName);
    }

    private string CreateGame(string playerDisplayName, string opponentDisplayName)
    {
        // Remover jugadores de la lista de espera
        this.WaitingList.RemovePlayer(playerDisplayName);
        this.WaitingList.RemovePlayer(opponentDisplayName);
        
        // Crear una partida real del juego Age of Empires
        var partida = CrearPartidaReal(playerDisplayName, opponentDisplayName);
        
        // Almacenar la partida activa
        string gameId = $"{playerDisplayName}_{opponentDisplayName}";
        PartidasActivas[gameId] = partida;
        
        return $"¡Comienza la partida: {playerDisplayName} vs {opponentDisplayName}!";
    }

    /// <summary>
    /// Crea una partida real del juego Age of Empires
    /// </summary>
    /// <param name="player1Name">Nombre del primer jugador</param>
    /// <param name="player2Name">Nombre del segundo jugador</param>
    /// <returns>La partida creada</returns>
    private Library.Facade CrearPartidaReal(string player1Name, string player2Name)
    {
        // Obtener civilizaciones disponibles
        var civilizaciones = Library.Civilizacion.CivDisponibles(null);
        
        // Seleccionar civilizaciones aleatorias para cada jugador
        var random = new Random();
        var civ1 = civilizaciones[random.Next(civilizaciones.Count)];
        var civ2 = civilizaciones[random.Next(civilizaciones.Count)];
        
        // Crear la partida usando la Facade del dominio del juego
        var partida = new Library.Facade();
        partida.CrearPartida(civ1, civ2, player1Name, player2Name);
        partida.InicializarJugadores();
        
        return partida;
    }

    /// <summary>
    /// Inicia una partida entre dos jugadores.
    /// </summary>
    /// <param name="playerDisplayName">El primer jugador.</param>
    /// <param name="opponentDisplayName">El oponente.</param>
    /// <returns>Un mensaje con el resultado.</returns>
    public string StartBattle(string playerDisplayName, string? opponentDisplayName)
    {
        Player? opponent;
        
        if (!OpponentProvided() && !SomebodyIsWaiting())
        {
            return "No hay nadie esperando";
        }
        
        if (!OpponentProvided()) // && SomebodyIsWaiting
        {
            opponent = this.WaitingList.GetAnyoneWaiting();
            return this.CreateGame(playerDisplayName, opponent!.DisplayName);
        }

        // El símbolo ! luego de opponentDisplayName indica que sabemos que esa
        // variable no es null. Estamos seguros porque OpponentProvided hubiera
        // retorna false antes y no habríamos llegado hasta aquí.
        opponent = this.WaitingList.FindPlayerByDisplayName(opponentDisplayName!);
        
        if (!OpponentFound())
        {
            return $"{opponentDisplayName} no está esperando";
        }
        
        return this.CreateGame(playerDisplayName, opponent!.DisplayName);
        
        // Funciones locales a continuación para mejorar la legibilidad

        bool OpponentProvided()
        {
            return !string.IsNullOrEmpty(opponentDisplayName);
        }

        bool SomebodyIsWaiting()
        {
            return this.WaitingList.Count != 0;
        }

        bool OpponentFound()
        {
            return opponent != null;
        }
    }
}
