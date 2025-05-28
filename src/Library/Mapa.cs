namespace Library;

public class Mapa
{
    public const int Size = 100;
    public List<Celda> Celdas { get; private set; } 

    public Mapa()
    {
        Celdas = new List<Celda>();
        
        for (int x = 0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                Coordenada coordenada = new Coordenada(x, y);
                Celda celda = new Celda(coordenada);
                Celdas.Add(celda);
            }
        }
    }
    
    public Celda ObtenerCelda(Coordenada coordenada)
    {
        return Celdas.First(c =>
            c.Coordenada.X == coordenada.X &&
            c.Coordenada.Y == coordenada.Y);
    }
}