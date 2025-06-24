namespace Library;
/// <summary>
/// representa una celda del tablero que tiene una coordenada una coordenada.
/// </summary>
public class Celda
{
    ///<summary>
    /// obtiene la coordenada asociada a la celda
    /// </summary>
    /// <param name="coordenada"></param>
    public Coordenada Coordenada { get; }
    
    /// <summary>
    /// crea una instancia de la clase <see cref="Celda"/> con una coordenada
    /// </summary>
    /// <param name="coordenada">
    /// La coordenada q identifica la ubicacion de la celda
    /// </param>
    public Celda(Coordenada coordenada)
    {
        this.Coordenada = coordenada;
    }
}
