namespace Library;

public class Caballeria : UnidadMilitar
{
    public Caballeria(int id, Coordenada ubicacion, Player owner)
        : base(id, ubicacion, vida: 100, velocidad: 90, owner, ataque: 40)
    {
        Defensa = 20;  // Definís valores propios para cada tipo
    }  
}