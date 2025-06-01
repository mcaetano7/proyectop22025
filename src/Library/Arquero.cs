namespace Library;

public class Arquero : UnidadMilitar
{
    public Arquero(int id, Coordenada ubicacion, Player owner)
        : base(id, ubicacion, vida: 100, velocidad: 60, owner, ataque: 90, defensa : 10)
    {
    } 
}