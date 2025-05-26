namespace Library;

public abstract class Unidad
{
    public int Id { get; set; }
    public Coordenada Ubicacion { get; set; }
    public int Vida { get; set; }
    public int Velocidad { get; set; }
    public Player Owner { get; set; }

    public Unidad(int id, Coordenada ubicacion, int vida, int velocidad, Player owner)
    {
        Id = id;
        Ubicacion = ubicacion;
        Vida = vida;
        Velocidad = velocidad;
        Owner = owner;
    }

    public abstract void Mover(Coordenada nuevaUbicacion);
}