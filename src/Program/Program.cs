namespace Program;
using Library;

class Program
{
    static void Main(string[] args)
    {
        
        Civilizacion civ1 = new Civilizacion("bizantinos", new List<string>());
        Civilizacion civ2 = new Civilizacion("japanese", new List<string>());
        Player jugador1 = new Player("Mika", civ1);
        Player jugador2 = new Player("Victoria", civ2);
        
        
        Facade juego = new Facade();
        
        juego.CrearPartida(civ1, civ2);
        
        juego.Jugador1.InicializarJuego();
        juego.Jugador2.InicializarJuego();
        
        
    }
  
}
