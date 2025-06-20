namespace Library;

public class Mapa
{
    public const int Size = 100; //tamaño del mapa
    public List<Celda> Celdas { get; private set; } //lista con las celdas

    public Mapa() //inicializa el mapa con las celdas necesarias
    {
        Celdas = new List<Celda>(); //inicializa la lista de celdas
        
        for (int x = 0; x < Size; x++) //genera las celdas
        {
            for (int y = 0; y < Size; y++)
            {
                Coordenada coordenada = new Coordenada(x, y); //coordenada para la ubicacion
                Celda celda = new Celda(coordenada); //crea la celda en la coordenada
                Celdas.Add(celda); //agrega la celda en la lista de mapa
            }
        }
    }
    
    public Celda ObtenerCelda(Coordenada coordenada) //obtiene la celda en las coordenadas indicadas
    {
        return Celdas.First(c =>
            c.Coordenada.X == coordenada.X &&
            c.Coordenada.Y == coordenada.Y); //busca y retorna la celda que coincida con las coordenadas
    }
}