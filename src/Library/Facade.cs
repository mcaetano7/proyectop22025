using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Library
{
    /// <summary>
    /// fachada principal del juego para simplificar la interacción con el sistema
    /// </summary>
    public class Facade
    {
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
        public void Recolectar(Player jugador, TipoRecurso tipo, Coordenada ubicacion)
        {
            jugador.RecolectarRecurso(tipo, ubicacion);
        }

        /// <summary>
        /// construye un edificio en una coordenada
        /// </summary>
        public void Construir(Player jugador, Edificio edificio, Coordenada ubicacion)
        {
            jugador.Construir(edificio, ubicacion);
        }

        /// <summary>
        /// mueve una unidad a una nueva coordenada
        /// </summary>
        public void MoverUnidad(Unidad unidad, Coordenada destino)
        {
            unidad.Mover(destino);
        }
        

        /// <summary>
        /// devuelve una representación en texto del mapa
        /// </summary>
        public string? VerMapaAscii()
        {
            var generador = new GenerarMapa(20, 15);
            return generador.ToString();
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

        public void CrearPartida(Civilizacion civ1, Civilizacion civ2)
        {
            throw new NotImplementedException();
        }
    }
}
