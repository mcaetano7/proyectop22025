namespace Library;

/// <summary>
/// Define el estado de una partida del juego
/// </summary>
public interface IEstadoPartida
{
    
    /// <summary>
    /// Nombre de la partida que se está jugando
    /// </summary>
    public string NombrePartida { get; set; } 
    
    /// <summary>
    /// Nombre del jugador
    /// </summary>
    public string NombreJugador { get; set; }
    
    /// <summary>
    /// Nivel que está jugando el jugador
    /// </summary>
    public int Level { get; set; } 
    
    /// <summary>
    /// Salud que tiene el jugador actúal
    /// </summary>
    public int Health { get; set; } 
    
    /// <summary>
    /// Lista de objetos que se encuentran en el inventario
    /// </summary>
    public List<string> Inventario { get; set; }

    /// <summary>
    /// Constructor de la clase, inicializa el inventario
    /// </summary>
    public void EstadoPartida()
    {
        Inventario = new List<string>();
    }
    
}