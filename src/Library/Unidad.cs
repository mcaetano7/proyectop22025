namespace Library;

public abstract class Unidad
{
    public int Id { get; set; }
    public Coordenada Ubicacion { get; set; }
    public static int Vida { get; set; }
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
    
    public bool EstaMuerto() => Vida <= 0;
    public virtual int RecibirDefensa() => 0;
    private int cantidad;
    public abstract void RecibirDamage(int damage);
    {
        Vida = Math.Max(0, Vida - cantidad);
    }
    public abstract void Mover(Coordenada nuevaUbicacion);
}