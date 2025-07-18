using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ucu.Poo.DiscordBot.Commands;

namespace Library
{
    /// <summary>
    /// fachada principal del juego para simplificar la interacción con el sistema
    /// </summary>
    public class Facade
    {
        /// <summary>
        /// implementacion singleton (esto es para que ...)
        /// </summary>
        private static Facade? _instance;
        public static Facade Instance => _instance ??= new Facade();

        /// <summary>
        ///  constructor privado para evitar instanciación externa
        /// </summary>
        public Facade()
        {
            // el metodo es el constructor privado del patron singleton y aunquew está vacío sirve para impedir crear la clase desde afuera
        }
        
        /// <summary>
        /// mapa del juego
        /// </summary>
        public Mapa Mapa { get; private set; }

        /// <summary>
        /// primer jugador
        /// </summary>
        public Player Jugador1 { get; private set; }

        /// <summary>
        /// segundo jugador
        /// </summary>
        public Player Jugador2 { get; private set; }
        
        /// <summary>
        /// obtiene una instancia del jugador correspondiente al nombre ingresado
        /// </summary>
        /// <param name="nombre">Nombre del jugador que se desea buscar</param>
        /// <returns>
        /// retorna el jugador cuyo nombre coincida con el parámetro ingresado 
        /// si no se encuentra un jugador con ese nombre, retorna null
        /// </returns>
        public Player GetJugadorPorNombre(string nombre)
        {
            if (Jugador1.Nombre == nombre)
                return Jugador1;
            else if (Jugador2.Nombre == nombre)
                return Jugador2;
            else
                return null;
        }
        
        /// <summary>
        /// jugador que tiene el turno actual
        /// </summary>
        public Player TurnoActual { get; private set; }

        /// <summary>
        /// crea una nueva partida con dos civilizaciones
        /// </summary>
        public void CrearPartida(Civilizacion civ1, Civilizacion civ2, string nombreJugador1, string nombreJugador2)
        {
            Mapa = new Mapa();
            Jugador1 = new Player(nombreJugador1, civ1);
            Jugador2 = new Player(nombreJugador2, civ2);
            
            // Determinar aleatoriamente quién empieza
            DeterminarPrimerTurno();
        }

        /// <summary>
        /// Determina aleatoriamente qué jugador empieza la partida
        /// </summary>
        private void DeterminarPrimerTurno()
        {
            var random = new Random();
            TurnoActual = random.Next(2) == 0 ? Jugador1 : Jugador2;
        }

        /// <summary>
        /// Pasa el turno al siguiente jugador
        /// </summary>
        public void SiguienteTurno()
        {
            TurnoActual = TurnoActual == Jugador1 ? Jugador2 : Jugador1;
        }

        /// <summary>
        /// Obtiene el nombre del jugador que tiene el turno actual
        /// </summary>
        /// <returns>Nombre del jugador con el turno</returns>
        public string ObtenerJugadorTurno()
        {
            return TurnoActual.Nombre;
        }

        /// <summary>
        /// Verifica si un jugador específico tiene el turno
        /// </summary>
        /// <param name="nombreJugador">Nombre del jugador a verificar</param>
        /// <returns>True si tiene el turno, False en caso contrario</returns>
        public bool TieneTurno(string nombreJugador)
        {
            return TurnoActual.Nombre == nombreJugador;
        }

        /// <summary>
        /// inicializa el juego para ambos jugadores
        /// </summary>
        public void InicializarJugadores()
        {
            Jugador1.InicializarJuego();
            Jugador2.InicializarJuego();
        }

        /// <summary>
        /// ordena a un jugador recolectar un recurso en una coordenada
        /// </summary>
        public void Recolectar(Player jugador, TipoRecurso? tipo, Coordenada ubicacion)
        {
            jugador.RecolectarRecurso(tipo, ubicacion);
        }

        /// <summary>
        /// construye un edificio en una coordenada
        /// </summary>
        /// <returns>True si la construcción fue exitosa, False en caso contrario</returns>
        public bool Construir(Player jugador, Edificio edificio, Coordenada ubicacion)
        {
            return jugador.Construir(edificio, ubicacion);
        }

        /// <summary>
        /// mueve una unidad a una nueva coordenada
        /// </summary>
        public bool MoverUnidad(Unidad unidad, Coordenada destino)
        {

            try
            {
                unidad.Mover(destino);
                return true;
            }
            catch
            {
                return false;
            }
        
        }

        public Unidad ObtenerUnidadPorId(int id, string nombreJugador)
        {
            var jugador = GetJugadorPorNombre(nombreJugador);
            if (jugador == null) return null;
    
            return jugador.Unidades?.Cast<Unidad>()?.FirstOrDefault(u => u?.Id == id);
        }
        

        /// <summary>
        /// devuelve una representación en texto del mapa
        /// </summary>
        public string VerMapaAscii()
        {
            var generador = new GenerarMapa(20, 15);
            return generador.MostrarMapa();
        }

        /// <summary>
        /// guarda la partida en un archivo
        /// </summary>
        public void GuardarPartida(string ruta = "partida.txt")
        {
            File.WriteAllText(ruta, "simulando partida guardada...");
        }

        /// <summary>
        /// carga una partida desde un archivo
        /// </summary>
        public void CargarPartida(string ruta = "partida.txt")
        {
            if (File.Exists(ruta))
            {
                string contenido = File.ReadAllText(ruta);
                // simulación de carga
            }
        }


    }
    
    
}
