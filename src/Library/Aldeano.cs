namespace Library;
/// <summary>
/// esta clase representa a los aldeanos, quienes pueden recolectar recursos
/// </summary>
public class Aldeano : Unidad
{
    /// <summary>
    /// indica si el aldeano está ocupado (recolectando recursos) o no
    /// </summary>
    private bool ocupado = false; 
    
    /// <summary>
    /// construye al aldeano
    /// </summary>
    /// <param name="id"> identifica al aldeano </param>
    /// <param name="ubicacion"> donde se encuentra inicialmente en el mapa</param>
    /// <param name="owner"> a que jugador le pertenece </param>
    public Aldeano(int id, Coordenada ubicacion, Player owner)
        : base (id, ubicacion, vida: 50, velocidad : 1, owner)
    {}
    /// <summary>
    /// //m{etodo que cambia la ubi del aldeano por una nueva (mueve al aldeano)
    /// </summary>
    /// <param name="nuevaUbicacion"> ubicación a la que el aldeano es movido</param>
    public override void Mover(Coordenada nuevaUbicacion)
    {
        Ubicacion = nuevaUbicacion; 
    }
    
    /// <summary>
    /// obtiene la celda del mapa donde esta el aldeano
    /// </summary>
    /// <param name="mapa">instancia</param>
    /// <returns>devuelve la celda correspondiente al aledano</returns>
    public Celda ObtenerCeldaActual(Mapa mapa)
    {
        return mapa.ObtenerCelda(Ubicacion);
    }

    /// <summary>
    /// método para saber el estado de un aldeano
    /// </summary>
    /// <returns> devuelve true si el aldeano no está ocuado (si no está recolectando recursos) </returns>
    public bool EstaDisponible()
    {
        return !ocupado; 
    }

    
    /// <summary>
    /// //método para que el aldeano recolecte recursos y los acumule con agregar.recurso
    /// </summary>
    /// <param name="tipo"> indica de que recurso se trata</param>
    /// <param name="ubicacion"> indica la ubicación de la recolección </param>
    public void IniciarRecoleccion(TipoRecurso tipo, Coordenada ubicacion) 
    {
        ocupado = true;
        Owner.AgregarRecurso(tipo, 10);
        ocupado = false;
    }
}