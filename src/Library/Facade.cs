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
        /// crea una nueva partida con dos civilizaciones
        /// </summary>
        public void CrearPartida(Civilizacion civ1, Civilizacion civ2)
        {
            Mapa = new Mapa();
            Jugador1 = new Player("jugador1", civ1);
            Jugador2 = new Player("jugador2", civ2);
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
        public string VerMapaAscii()
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
    }
}
