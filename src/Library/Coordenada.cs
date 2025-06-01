namespace Library;

public class Coordenada
{
    public int X { get; set; }
    public int Y { get; set; }

    public Coordenada(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool EsValida()
    {
        return X >= 0 && X < Mapa.Size && Y >= 0 && Y < Mapa.Size;
    }
}