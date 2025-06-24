namespace Library;
/// <summary>
/// rrepresnta una coordenda en el mapa del juego con valores x e y
/// </summary>
public class Coordenada
{
    /// <summary>
    /// valor horizontal (x)
    /// </summary>
    public int X { get; set; }
    /// <summary>
    /// valor vertical (y)
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// inicializa una nueva instancia de la clase<see cref="Coordenada"/> con valores x e y que ya estan dados
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public Coordenada(int x, int y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// verifica si la coordenada se encuentra dentro de los limites del mapa
    /// </summary>
    /// <returns><c>true</c> si la coordenada está dentro del mapa y si no <c>false</c></returns>
    public bool EsValida()
    {
        return X >= 0 && X < Mapa.Size && Y >= 0 && Y < Mapa.Size;
    }
}