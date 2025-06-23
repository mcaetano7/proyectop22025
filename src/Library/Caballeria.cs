namespace Library;
/// <summary>
/// clase que representa a la caballería (una unidad militar)
/// </summary>
public class Caballeria : UnidadMilitar
{
    /// <param name="id"> identifica a cada caballería</param>
    /// <param name="ubicacion"> donde se encuentra en el mapa</param>
    /// <param name="owner"> a que jugador le pertenece</param>
    public Caballeria(int id, Coordenada ubicacion, Player owner) //creamos caballer{ia heredado de UnidadMilitar (llamamos al contrusctor y le pasamos los parámetros)
        : base(id, ubicacion, vida: 100, velocidad: 90, owner, ataque: 40, defensa : 30)
    {
    }  
}