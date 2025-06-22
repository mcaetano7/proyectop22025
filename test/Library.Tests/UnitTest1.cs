namespace Library.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        
    }

    //1. poder crear una nueva partida especificando el tamaño del mapa y cantidad de jugadores.
    [Test]
    public void NuevaPartidaTest()
    {
        var mapa = new Mapa();
        Assert.That(mapa.Celdas.Count, Is.EqualTo(10000), "El mapa no contiene la cantidad esperada de celdas(100x100)");
        
        var civ1 = new Civilizacion("bizantinos", new List<string>());
        var civ2 = new Civilizacion("constantinopolitanos", new List<string>());
        var jugador1 = new Player("Emperador Bizantino", civ1);
        var jugador2 = new Player("Constantino I", civ2);
        
        List<Player> jugadores = new List<Player> { jugador1, jugador2 };
        Assert.That(jugadores.Count, Is.EqualTo(2), "La cantidad de jugadores creados no es la esperada.");

        Assert.That(jugadores[0].Nombre, Is.EqualTo("Jugador1"));
        Assert.That(jugadores[1].Nombre, Is.EqualTo("Jugador2"));
        Assert.That(jugadores[0].Civilizacion.Name, Is.EqualTo("vikingos"));
        Assert.That(jugadores[1].Civilizacion.Name, Is.EqualTo("francos"));
    }
    
    //2. elegir civilización (aprovechar sus ventajas estratégicas)
    [Test]
    public void ElegirCivilizacion()
    {
        var ventajas = new List<string> { "Recolectan madera más rápido", "Unidades más baratas" };
        var civ = new Civilizacion("Mortífagos", ventajas);
        var jugador = new Player("Lucius Malfoy", civ);
        
        var civilizacionDelJugador = jugador.Civilizacion;
        
        Assert.That(civilizacionDelJugador.Name, Is.EqualTo("Chinos"), "La civilización asignada no es la esperada.");
        //Assert.AreEqual(ventajas, civilizacionDelJugador./NO SE QUE VA ACA/, "Las ventajas estratégicas no se asignaron correctamente.");
    }
    
    //3. comenzar con un centro cívico y algunos aldeanos para iniciar la recolecci{on de recursos.
    [Test]
    public void EdificiosIniciales()
    {
        var civ = new Civilizacion("Bizantinos", new List<string>());
        var jugador = new Player("Beto", civ);

        jugador.InicializarJuego();

        var centroCivicoExiste = jugador; //terminar

        var cantidadAldeanos = jugador; //terminar

        Assert.IsTrue(centroCivicoExiste, "El jugador no comenzó con un Centro Cívico.");
        Assert.That(cantidadAldeanos, Is.EqualTo(3), "El jugador no comenzó con 3 aldeanos.");
    }
    
    //4. ordenar a los aldeanos recolectar diferentes tipos de recursos.
    [Test]
    public void RecoleccionDeDistintosRecursos()
    {
        var player = new Player("TestPlayer", new Civilizacion());
        player.InicializarJuego();

        var recursosIniciales = new Dictionary<TipoRecurso, int>();
        {
            { TipoRecurso.Alimento, 100 },
            { TipoRecurso.Madera, 100 },
            { TipoRecurso.Oro, 0 },
            { TipoRecurso.Piedra, 0 }
        };
        var ubicacionAlimento = new Coordenada(45, 45);
        var ubicacionMadera = new Coordenada(55, 55);
        var ubicacionOro = new Coordenada(60, 40);
        
        player.RecolectarRecurso(TipoRecurso.Alimento, ubicacionAlimento);
        player.RecolectarRecurso(TipoRecurso.Madera,ubicacionMadera);
        player.RecolectarRecurso(TipoRecurso.Oro,ubicacionOro);

        var aldeanoDisponible = player.GetAldeanoDisponible();
        Assert.IsNotNull(aldeanoDisponible, "Tienen que haber aldeanos disponibles ");
        //no se cual poner
        Assert.That(player.GetRecurso, Is.EqualTo(110), "El alimento se debe incrementar de 100 a 110") ;
        Assert.AreEqual(110, player.GetRecurso(TipoRecurso.Madera), "La madera se debe incrementar de 100 a 110");
        Assert.AreEqual(10, player.GetRecurso(TipoRecurso.Oro), "El oro debe incrementar de 0 a 10");
    }
    
    //5. construir edificios para almacenar recursos.
    [Test]
    public void ConstruccionEdificios()
    {
        var coordenada = new Coordenada(5, 10);
        var player = new Player();
        string TipoAlmacen = "General";
        int capacidadAlmacen = 1000;
        int vidaInicial = 100;

        var almacen = new Almacen(coordenada, vidaInicial, player, TipoAlmacen, capacidadAlmacen);
    }
    
    // 6. quiero visualizar la cantidad de recursos disponibles.
    [Test]
    public void CantidadRecursos()
    {
        
    }
    
    // 7. construir edificios en ubis específicas para expandir la base.
    [Test]
    public void UbicacionEspecifica()
    {
        
    }
    
    //8. crear diferentes tipos de edificios con funciones específicas para desarrollar la civilización.
    [Test]
    public void DesarrollarCivilizacionEdificios()
    {
        
    }
    
    //9. entrrenar unidades militares para defender la base y atacar oponentes.
    [Test]
    public void EntrenarUnidades()
    {
        
    }
    
    //10. mover las unidades por el mapa usando comandos simples.
    [Test]
    public void MoverUnidades()
    {
        
    }
    
    //11. ordenar a las unidades atacar edeficios o unidades enemigas. 
    [Test]
    public void AtacarEnemigos()
    {
        
    }
    
    //12. entrenar aldeanos parta mejorar la econom{ia y tener suficientes casas para mantener la población.
    [Test]
    public void EntrenarAldeanos()
    {
        
    }
    
    //13. destruir los centros cívicos para ganar la partida por dominación militar.
    [Test]
    public void MilitaresGananPartida()
    {
        
    }
    
    //14. usar comandos intuitivos para interactuar con el juego 
    [Test]
    public void InteractuarJuego()
    {
        
    }
    
    //15. quiero ver un mapa simplificado del juego en ASCII para visualizar la dispo del terreno y unidades. 
    [Test]
    public void MapaEnAscii()
    {
        
    }

    //16. como jugador quiero gardar la partida y continuarla más tarde. 
    [Test]
    public void GuardarPartidaTest()
    {
        
    }
}