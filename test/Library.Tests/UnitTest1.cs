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

        Assert.That(jugadores[0].Nombre, Is.EqualTo("Emperador Bizantino"));
        Assert.That(jugadores[1].Nombre, Is.EqualTo("Constantino I"));
        Assert.That(jugadores[0].Civilizacion.Name, Is.EqualTo("bizantinos"));
        Assert.That(jugadores[1].Civilizacion.Name, Is.EqualTo("constantinopolitanos"));
    }
    
    //2. elegir civilización (aprovechar sus ventajas estratégicas)
    [Test]
    public void ElegirCivilizacion()
    {
        var ventajas = new List<string> { "Recolectan madera más rápido", "Unidades más baratas" };
        var civ = new Civilizacion("Chinos", ventajas);
        var jugador = new Player("Lucius Malfoy", civ);
        
        var civilizacionDelJugador = jugador.Civilizacion;
        
        Assert.That(civilizacionDelJugador.Name, Is.EqualTo("Chinos"), "La civilización asignada no es la esperada.");
        //Assert.AreEqual(ventajas, civilizaciónDelJugador./NO SE QUE VA ACA/, "Las ventajas estratégicas no se asignaron correctamente.");
    }
    
    //3. comenzar con un centro cívico y algunos aldeanos para iniciar la recolección de recursos.
    [Test]
    public void EdificiosIniciales()
    {
        var civ = new Civilizacion("Bizantinos", new List<string>());
        var jugador = new Player("Beto", civ);

        jugador.InicializarJuego();

        bool centroCivicoExiste = !jugador.Victoria();

        var aldeanosField = typeof(Player).GetField("aldeanos", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var listaAldeanos = (List<Aldeano>)aldeanosField.GetValue(jugador);
        int cantidadAldeanos = listaAldeanos.Count;

        Assert.IsTrue(centroCivicoExiste);
        Assert.That(cantidadAldeanos, Is.EqualTo(3));
    }
    

    //4. ordenar a los aldeanos recolectar diferentes tipos de recursos.
    [Test]
    public void RecoleccionDeDistintosRecursos()
    {
        var civilizacion = new Civilizacion("Bizantinos", new List<string> { "Bonificación de prueba" });
        var player = new Player("Juan",  civilizacion);
        player.InicializarJuego();

        var recursosIniciales = new Dictionary<TipoRecurso?, int>()
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
        Assert.That(player.GetRecurso(TipoRecurso.Alimento), Is.EqualTo(110), "El alimento se debe incrementar de 100 a 110") ;
        Assert.That(player.GetRecurso(TipoRecurso.Madera), Is.EqualTo(110), "La madera se debe incrementar de 100 a 110") ;
        Assert.That(player.GetRecurso(TipoRecurso.Oro), Is.EqualTo(10), "El oro se debe incrementar de 0 a 10") ;
    }
    
    //5. construir edificios para almacenar recursos.
    [Test]
    public void ConstruccionEdificios()
    {
        var coordenada = new Coordenada(5, 10);
        var civilizacion = new Civilizacion("Bizantinos", new List<string>());
        var player = new Player("Juan", civilizacion);
        TipoRecurso? tipoAlmacen = TipoRecurso.Madera;
        int capacidadAlmacen = 1000;
        int vidaInicial = 100;

        var almacen = new Almacen(coordenada, vidaInicial, player, tipoAlmacen, capacidadAlmacen); 
        
        Assert.IsNotNull(almacen);
        Assert.That(almacen.Ubicacion, Is.EqualTo(coordenada));
        Assert.That(almacen.Vida, Is.EqualTo(vidaInicial));
        Assert.That(almacen.Owner, Is.EqualTo(player));
        Assert.That(almacen.Tipo, Is.EqualTo(tipoAlmacen));
        Assert.That(almacen.Capacidad, Is.EqualTo(capacidadAlmacen));
        Assert.IsTrue(almacen.Capacidad > 0, "El almacén debe poder tener capacidad");
        
        var costo = almacen.ObtenerCosto();
        Assert.IsNotNull(costo);
        Assert.That(costo.Count, Is.EqualTo(1), "El almacén debe tener un tipo de recurso como costo");
        Assert.IsTrue(costo.ContainsKey(TipoRecurso.Madera));
        Assert.That(costo[TipoRecurso.Madera], Is.EqualTo(500), "El costo de madera debería ser 500");
        
        var recursoMadera = new RecursoJugador(TipoRecurso.Madera.Nombre, 100);
        var recursoOro = new RecursoJugador(TipoRecurso.Oro.Nombre, 50);
        var recursoAlimento = new RecursoJugador(TipoRecurso.Alimento.Nombre, 200);
        var recursoPiedra = new RecursoJugador(TipoRecurso.Piedra.Nombre, 75);
        
        Assert.DoesNotThrow(() => almacen.Almacenar(recursoMadera));
        Assert.DoesNotThrow(() => almacen.Almacenar(recursoOro));
        Assert.DoesNotThrow(() => almacen.Almacenar(recursoAlimento));
        Assert.DoesNotThrow(() => almacen.Almacenar(recursoPiedra));
        
        Assert.That(almacen.Capacidad, Is.EqualTo(capacidadAlmacen), "La capacidad no debería cambiar al almacenar");
        Assert.That(almacen.Vida, Is.EqualTo(vidaInicial), "La vida no debería cambiar al almacenar");
    }
    
    // 6. quiero visualizar la cantidad de recursos disponibles.
    [Test]
    public void CantidadRecursos()
    {
        var civilizacion = new Civilizacion("Bizantinos", new List<string>());
        var player = new Player("Juan", civilizacion);
        
        Assert.That(player.GetRecurso(TipoRecurso.Alimento), Is.EqualTo(100));
        Assert.That(player.GetRecurso(TipoRecurso.Madera), Is.EqualTo(100));
        Assert.That(player.GetRecurso(TipoRecurso.Oro), Is.EqualTo(0));
        Assert.That(player.GetRecurso(TipoRecurso.Piedra), Is.EqualTo(0));
    }
    
    // 7. construir edificios en ubicaciones específicas para expandir la base.
    [Test]
    public void UbicacionEspecifica()
    {
        var bonificaciones = new List<string>();
        var civilizacion = new Civilizacion("Bizantinos", bonificaciones);
        var player = new Player("Juan", civilizacion);
        var ubicacionEspecifica = new Coordenada(99, 99);

        int maderaInicial = player.GetRecurso(TipoRecurso.Madera);
        var casa = new Casa(ubicacionEspecifica, 100, player, "Residencial", 5);
        
        player.Construir(casa, ubicacionEspecifica);

        Assert.That(player.GetRecurso(TipoRecurso.Madera), Is.EqualTo(maderaInicial - 25));
        Assert.That(casa.Ubicacion.X, Is.EqualTo(99));
        Assert.That(casa.Ubicacion.Y, Is.EqualTo(99));
        Assert.IsTrue(ubicacionEspecifica.EsValida());
        Assert.That(casa.Owner, Is.EqualTo(player));
    }
    
    //8. crear diferentes tipos de edificios con funciones específicas para desarrollar la civilización.
    [Test]
    public void DesarrollarCivilizacionEdificios()
    {
        var jugador = new Player("Maria", new Civilizacion("Bizantinos", new List<string>()));
        var ubicacionCasa = new Coordenada(0, 0);
        var ubicacionAlmacen = new Coordenada(2, 1);

        var casa = new Casa(ubicacionCasa, 100, jugador, "Casa", 5);
        var tipoAlmacen = TipoRecurso.Madera;
        var almacen = new Almacen(ubicacionAlmacen, 150, jugador, tipoAlmacen, 10);
        
        // simulamos que en la casa se alojan personas (incrementamos población actual)
        casa.CapacidadPoblacion = 3;

        // verificamos que el edificio fue creado 
        var costoCasa = casa.ObtenerCosto();
        var costoAlmacen = almacen.ObtenerCosto();
        
        Assert.IsNotNull(casa, "La casa debería haberse creado correctamente");
        Assert.IsNotNull(almacen, "El almacén debería haberse creado correctamente");

        Assert.That(casa.CapacidadPoblacion, Is.EqualTo(3), "La casa debería tener capacidad de población usada");
        Assert.That(casa.CapacidadMaxima, Is.EqualTo(5), "La casa debería tener una capacidad máxima de 5");

        Assert.That(almacen.Tipo, Is.EqualTo(tipoAlmacen), "El almacén debería almacenar madera");
        Assert.That(almacen.Capacidad, Is.EqualTo(10), "El almacén debería tener capacidad 10");

        Assert.IsTrue(costoCasa.ContainsKey(TipoRecurso.Madera), "La casa debería costar madera");
        Assert.IsTrue(costoAlmacen.ContainsKey(TipoRecurso.Madera), "El almacén debería costar madera"); 
    }
    
    //9. entrenar unidades militares para defender la base y atacar oponentes.
   [Test]
    public void EntrenarUnidades()
    {
        var jugador = new Player("Mika", new Civilizacion("Bizantinos", new List<string>()));
        var cuartel = new Cuartel(new Coordenada(5, 5), jugador);

        var infante = cuartel.EntrenarInfanteria(1);
        var arquero = cuartel.EntrenarArquero(2);
        var caballeria = cuartel.EntrenarCaballeria(3);

        Assert.IsNotNull(infante);
        Assert.IsNotNull(arquero);
        Assert.IsNotNull(caballeria);

        Assert.That(infante.Owner, Is.EqualTo(jugador));
        Assert.That(arquero.Owner, Is.EqualTo(jugador));
        Assert.That(caballeria.Owner, Is.EqualTo(jugador));
    }

    
    //10. mover las unidades por el mapa usando comandos simples.
    [Test]
    public void MoverUnidades()
    {
        var jugador = new Player("Mar", new Civilizacion("Japoneses", new List<string>()));
        var ubicacionInicial = new Coordenada(1, 1);
        var unidad = new Caballeria(1, ubicacionInicial, jugador);

        var nuevaUbicacion = new Coordenada(2, 2);
        unidad.Mover(nuevaUbicacion);

        Assert.That(unidad.Ubicacion, Is.EqualTo(nuevaUbicacion));
    }

    
    //11. una unidad puede atacar a otra y se reduce su vida 
    [Test]
    public void AtacarEnemigos()
    {
        var jugador1 = new Player("Pepe", new Civilizacion("Bizantinos", new List<string>()));
        var jugador2 = new Player("Lucia", new Civilizacion("Japoneses", new List<string>()));

        var ubicacionAtacante = new Coordenada(0, 0);
        var ubicacionObjetivo = new Coordenada(1, 0);

        var atacante = new Caballeria(1, ubicacionAtacante, jugador1);
        var objetivo = new Caballeria(2, ubicacionObjetivo, jugador2); // enemigo

        int vidaInicial = objetivo.Vida;
        
        atacante.Atacar(objetivo);
        
        Assert.Less(objetivo.Vida, vidaInicial, "La vida del objetivo debería disminuir después de ser atacado");
    }
    
    //12. entrenar aldeanos para mejorar la economía y tener suficientes casas para mantener la población.
    [Test]
    public void EntrenarAldeanos()
    {
        var jugador = new Player("Vicky", new Civilizacion("Chinos", new List<string>()));
        var centro = new CentroCivico(new Coordenada(0, 0), 300, jugador, 5);

        var aldeano = centro.EntrenarAldeano();

        Assert.IsNotNull(aldeano);
        Assert.That(aldeano.Owner, Is.EqualTo(jugador));
    }
    
    //13. destruir los centros cívicos para ganar la partida por dominación militar.
    [Test]
    public void Dictadura()
    {
        var jugador1 = new Player("Pepe", new Civilizacion("Francos", new List<string>()));
        var jugador2 = new Player("Lucía", new Civilizacion("Japoneses", new List<string>()));

        var centroEnemigo = new CentroCivico(new Coordenada(5, 5), 100, jugador2, 5);

        var atacante = new Infanteria(1, new Coordenada(5, 4), jugador1);

        centroEnemigo.RecibirDamage(atacante.Ataque);

        Assert.IsTrue(centroEnemigo.Vida < 100, "El centro cívico debería recibir daño");

        centroEnemigo.RecibirDamage(100);
        Assert.IsTrue(centroEnemigo.EstaMuerto(), "El centro cívico debería haber sido destruido");
    }

    
    //14. usar comandos intuitivos para interactuar con el juego 
    [Test]
    public void InteractuarJuego()
    {
        var facade = new Facade();
        var civ1 = new Civilizacion("Bizantinos", new List<string>());
        var civ2 = new Civilizacion("Francos", new List<string>());

        facade.CrearPartida(civ1, civ2);
       // string respuesta = facade.InterpretarComando("mover unidad 1 a 3 4"); CREAR MÉTODO EN FACADE PARA INTERPRETAR COMANDOS

       // Assert.IsNotNull(respuesta);
      //  Assert.IsTrue(respuesta.Contains("mover"), "El comando debería haber sido procesado");
    }

    
    //15. quiero ver un mapa simplificado del juego en ASCII para visualizar la disponibilidad del terreno y unidades. 
    [Test]
    public void MapaEnAscii()
    {
        var mapa = new Mapa();
        int cantidadCeldas = mapa.Celdas.Count;

        Assert.That(cantidadCeldas, Is.EqualTo(10000), "El mapa debería tener 100x100 celdas");

        // Simulación de una vista simplificada usando un fragmento de celdas
        var vistaParcial = string.Join("\n",
            mapa.Celdas
                .Where(c => c.Coordenada.X < 5 && c.Coordenada.Y < 5)
                .GroupBy(c => c.Coordenada.Y)
                .Select(grupo => string.Join(" ", grupo.Select(_ => "."))));

        Assert.IsTrue(vistaParcial.Contains("."), "La vista simplificada debería contener caracteres de representación");
    }


    //16. como jugador quiero guardar la partida y continuarla más tarde. 
    [Test]
    public void GuardarPartida()
    {
        var juego = new Facade();
        string ruta = "partida_test.txt";

        if (File.Exists(ruta))
            File.Delete(ruta);
        
        juego.GuardarPartida(ruta);
        
        Assert.IsTrue(File.Exists(ruta), "El archivo no fue creado al guardar la partida.");
        string contenido = File.ReadAllText(ruta);
        Assert.That(contenido, Is.EqualTo("simulando partida guardada..."), "El contenido del archivo guardado no es el esperado.");
        File.Delete(ruta);
    }

    //17. si el edificio recibe la misma cantidad de daño que tiene de vida debería destruirse
    [Test]
    public void DestruirEdificio()
    {
        var jugador = new Player("Carlos V", new Civilizacion("España", new List<string>()));
        var edificio = new Casa(new Coordenada(10, 10), 100, jugador, "Residencial", 5);

        edificio.RecibirDamage(100);
    
        Assert.IsTrue(edificio.EstaMuerto(), "El edificio debería estar destruido si recibe una cantidad de daño igual a su vida.");
    }
    
    //18. si no hay suficientes recursos no se construye el edificio
    [Test]
    public void NoConstruyeSinRecursos()
    {
        var jugador = new Player("Pizarro", new Civilizacion("Incas", new List<string>()));
        var coordenada = new Coordenada(0, 0);
        var casa = new Casa(coordenada, 100, jugador, "Casa", 5);

        jugador.GastarRecursos(new Dictionary<TipoRecurso?, int>
        {
            { TipoRecurso.Madera, 100 } // deja al jugador sin madera
        });

        jugador.Construir(casa, coordenada);

        Assert.That(jugador.PoblacionMaxima, Is.EqualTo(15), "No debería haber aumentado la población máxima al no construirse la casa.");
    }

    //19. si las coordenadas son invalidas no se puede mover para ahi
    [Test]
    public void CoordenadasInvalidas()
    {
        var jugador = new Player("Moctezuma", new Civilizacion("Mayas", new List<string>()));
        var unidad = new Infanteria(1, new Coordenada(1, 1), jugador);
        var nuevaUbicacion = new Coordenada(-5, 200);
        unidad.Mover(nuevaUbicacion);

        Assert.That(nuevaUbicacion, Is.Not.EqualTo(unidad.Ubicacion), "No debería moverse a coordenadas que no son validas.");
    }
    
    //20. no se debe atacar a unidades aliadas
    [Test]
    public void NoAtacarAliados()
    {
        var jugador = new Player("Hernán Cortés", new Civilizacion("Aztecas", new List<string>()));
        var atacante = new Infanteria(1, new Coordenada(0, 0), jugador);
        var aliado = new Infanteria(2, new Coordenada(1, 0), jugador);

        int vidaInicial = aliado.Vida;
        atacante.Atacar(aliado);

        Assert.That(aliado.Vida, Is.EqualTo(vidaInicial), "No se debería dañar a unidades aliadas.");
    }
    
    [Test]
    public void Constructor_CreaMapaConDimensionesCorrectas()
    {
        var mapa = new GenerarMapa(10, 5);
        Assert.AreEqual(10, mapa.Ancho);
        Assert.AreEqual(5, mapa.Alto);
    }
    
}