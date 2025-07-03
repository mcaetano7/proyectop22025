namespace Library;
/// <summary>
/// representa a los arqueros (una unidad militar)
/// </summary>
/// <param name="id"> identifica al arquero </param>
/// <param name="ubicacion"> donde se encuentra en el mapa </param>
/// <param name="owner"> jugador al que pertenece el arquero </param>
public class Arquero : UnidadMilitar
{
    public Arquero(int id, Coordenada ubicacion, Player owner) 
        : base(id, ubicacion, vida: 100, velocidad: 60, owner, ataque: 90, defensa : 10) //creamos arquero heredado de UNidadMIlitar (llamamos al contrusctor y le pasamos los parámetros )
    {
    } 
    
    public Celda ObtenerCeldaActual(Mapa mapa)
    {
        return mapa.ObtenerCelda(Ubicacion);
    }

}