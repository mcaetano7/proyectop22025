namespace Library;
//representa los arqueros (una unidad militar)
public class Arquero : UnidadMilitar
{
    public Arquero(int id, Coordenada ubicacion, Player owner) 
        : base(id, ubicacion, vida: 100, velocidad: 60, owner, ataque: 90, defensa : 10) //creamos arquero heredado de UNidadMIlitar (llamamos al contrusctor y le pasamos los parámetros )
    {
    } 
}