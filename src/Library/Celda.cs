namespace Library;

public class Celda
{
    private Coordenada coordenada;
    private List<ElementoMapa> contenido;
    
    public Celda(Coordenada coordenada)
    {
        this.coordenada = coordenada;
        this.contenido = new List<ElementoMapa>();
    }
    
    public Coordenada Coordenada
    {
        get { return coordenada; }
        set { coordenada = value; }
    }

    public List<ElementoMapa> Contenido
    {
        get { return contenido; }
    }
}
