namespace Library;

public interface EstadoPartida
{
    public string NombrePartida { get; set; }
    public string NombreJugador { get; set; }
    public int Level { get; set; }
    public int Health { get; set; }
    public List<string> Inventario { get; set; }
    
}