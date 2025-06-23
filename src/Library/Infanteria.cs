namespace Library;

/// <summary>
/// Clase que representa una unidad de infantería militar
/// </summary>
public class Infanteria : UnidadMilitar
{
    /// <summary>
    /// Constructor que inicializa una nueva infantería
    /// </summary>
    /// <param name="id"></param>
    /// <param name="ubicacion"></param>
    /// <param name="owner"></param>
    public Infanteria(int id, Coordenada ubicacion, Player owner) 
        : base(id, ubicacion, vida: 100, velocidad: 50, owner, ataque: 30, defensa: 50) 
    {
    } 
}