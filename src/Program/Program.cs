namespace Program
{
    using Library;

    /// <summary>
    /// clase principal que arranca la aplicación
    /// </summary>
    class Program
    {
        /// <summary>
        /// método main que crea la partida y los jugadores
        /// </summary>
        /// <param name="args">argumentos de línea de comando</param>
        static void Main(string[] args)
        {
            // crear civilizaciones
            Civilizacion civ1 = new Civilizacion("bizantinos", new List<string>());
            Civilizacion civ2 = new Civilizacion("japanese", new List<string>());

            // crear jugadores con civilizaciones
            Player jugador1 = new Player("Mika", civ1);
            Player jugador2 = new Player("Victoria", civ2);

            // crear fachada del juego
            Facade juego = new Facade();

            // crear partida con las civilizaciones
            juego.CrearPartida(civ1, civ2);

            // inicializar juego para ambos jugadores
            juego.Jugador1.InicializarJuego();
            juego.Jugador2.InicializarJuego();
        }
    }
}