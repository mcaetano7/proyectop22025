namespace Library;

public class Infanteria : UnidadMilitar
{
    public Infanteria(int id, Coordenada ubicacion, Player owner)
        : base(id, ubicacion, vida: 100, velocidad: 50, owner, ataque: 30)
    {
        Defensa = 50;  
    } 
}