using System.Drawing;

namespace Library;

/// <summary>
/// Clase que representa un mapa con celdas 
/// </summary>
public class Mapa
{
    public const int Size = 100;
    public const int TileSize = 32;
    public List<Celda> Celdas { get; private set; } 

    /// <summary>
    /// Constructor que inicializa un mapa con celdas
    /// </summary>
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
    
    /// <summary>
    /// Obtiene celdas dependiendo de sus coordenadas
    /// </summary>
    /// <param name="coordenada">Coordenada de las celdas que se obtienen</param>
    /// <returns>Celda en la coordenada pedida</returns>
    public Celda ObtenerCelda(Coordenada coordenada) 
    {
        return Celdas.First(c =>
            c.Coordenada.X == coordenada.X &&
            c.Coordenada.Y == coordenada.Y); 
    }

    public void DibujarMapa(Graphics g)
    {
        for (int x = 0; x < Size; x++)
        {
            for (int y = 0; x < Size; y++)
            {
                int posX = x * TileSize;
                int posY = y * TileSize;
                
            }
        }
    }
}