namespace Library;

public class Infanteria : UnidadMilitar
{
    public Infanteria(int id, Coordenada ubicacion, Player owner) //inicializa una unidad infanteria ya definida
        : base(id, ubicacion, vida: 100, velocidad: 50, owner, ataque: 30, defensa: 50) //ubicacion, puntos de vida, velocidad, propietario, ataque, defensa
    {
    } 
}