namespace Library;

/// <summary>
/// Clase que representa una unidad de infantería militar
/// </summary>
public class Infanteria : UnidadMilitar
{
    /// <summary>
    /// Constructor que inicializa una nueva infantería
    /// </summary>
    /// <param name="id">Identificación de la infantería</param>
    /// <param name="ubicacion">Coordenada donde está ubicada</param>
    /// <param name="owner">Propietario de la infantería</param>
    public Infanteria(int id, Coordenada ubicacion, Player owner) 
        : base(id, ubicacion, vida: 100, velocidad: 50, owner, ataque: 30, defensa: 50) 
    {
    } 
}