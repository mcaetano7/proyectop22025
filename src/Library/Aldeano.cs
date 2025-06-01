namespace Library;

public class Aldeano : Unidad
{
    public Aldeano(int id, Coordenada ubicacion, Player owner)
        : base (id, ubicacion, vida: 50, velocidad : 1, owner)
    {}

    public override void Mover(Coordenada nuevaUbicacion)
    {
        Ubicacion = nuevaUbicacion;
    }
    public void RecolectarRecurso(TipoRecurso tipo, Coordenada ubicacion)
    {
        var aldeano = GetAldeanoDisponible();
        if (aldeano != null)
        {
            aldeano.IniciarRecoleccion(tipo, ubicacion);
            
        }
    }
}