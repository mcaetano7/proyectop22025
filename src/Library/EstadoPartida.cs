namespace Library;

public interface EstadoPartida
{
    public string NombrePartida { get; set; } //nombre de la partida
    public string NombreJugador { get; set; } //nombre del jugador
    public int Level { get; set; } //nivel en el que se encuentra el jugador
    public int Health { get; set; } //salud del jugador
    public List<string> Inventario { get; set; } //lista de objetos que tiene el inventario
    
}