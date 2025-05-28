namespace Library;

public class Almacen : Edificio
{
    public string Tipo { get; set; }
    public int Capacidad { get; set; }

    public Almacen(Coordenada ubicacion, int vida, Player owner, string tipo, int capacidad)
        : base(ubicacion, vida, owner)
    {
        Tipo = tipo;
        Capacidad = capacidad;
    }
}
