namespace Library;

public class Aldeano : Unidad
{

    private bool ocupado = false;
    public Aldeano(int id, Coordenada ubicacion, Player owner)
        : base (id, ubicacion, vida: 50, velocidad : 1, owner)
    {}

    public override void Mover(Coordenada nuevaUbicacion)
    {
        Ubicacion = nuevaUbicacion;
    }

    public bool EstaDisponible()
    {
        return !ocupado;
    }

    public void IniciarRecoleccion(TipoRecurso tipo, Coordenada ubicacion)
    {
        ocupado = true;
        Owner.AgregarRecurso(tipo, 10);
        ocupado = false;
    }
}