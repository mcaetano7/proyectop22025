namespace Library;

public class Celda
{
    private Coordenada coordenada;
    
    public Celda(Coordenada coordenada)
    {
        this.coordenada = coordenada;
    }
    
    public Coordenada Coordenada
    {
        get { return coordenada; }
        set { coordenada = value; }
    }
}
