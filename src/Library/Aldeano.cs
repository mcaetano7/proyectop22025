namespace Library;

public class Aldeano
{
    public void RecolectarRecurso(TipoRecurso tipo, Coordenada ubicacion)
    {
        var aldeano = GetAldeanoDisponible();
        if (aldeano != null)
        {
            aldeano.IniciarRecoleccion(tipo, ubicacion);
            
        }
    }
}