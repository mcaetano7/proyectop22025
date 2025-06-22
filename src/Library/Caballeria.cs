namespace Library;
//representa los arqueros (una unidad militar)
public class Caballeria : UnidadMilitar
{
    public Caballeria(int id, Coordenada ubicacion, Player owner) //creamos caballer{ia heredado de UnidadMilitar (llamamos al contrusctor y le pasamos los parámetros)
        : base(id, ubicacion, vida: 100, velocidad: 90, owner, ataque: 40, defensa : 30)
    {
    }  
}