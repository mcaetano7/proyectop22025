using System.Collections;
using System.Collections.Generic;

using Library;

namespace Library
{
    /// <summary>
    /// Clase para representar un jugador en el juego
    /// </summary>
    public class Player
    {
        private string name; 
        private Civilizacion civilizacion; 
        private Dictionary<TipoRecurso, int> recursos; 
        private List<Aldeano> aldeanos; 
        private List<Edificio> edificios;
        // private bool accesible; // Campo no utilizado, comentado para evitar warning
        private int poblacionActual; 
        private int poblacionMaxima; 
        
        /// <summary>
        /// Constructor para inicializar al jugador con civilización
        /// </summary>
        /// <param name="name">Nombre del jugador</param>
        /// <param name="civilizacion">Civilización del jugador</param>
        public Player(string name, Civilizacion civilizacion, IEnumerable unidades) 
        {
            this.name = name;
            this.civilizacion = civilizacion;
            Unidades = unidades;
            // this.accesible = true; 

            // valores de inicio de juego
            this.recursos = new Dictionary<TipoRecurso, int>()
            {
                { TipoRecurso.Alimento, 100 },
                { TipoRecurso.Madera, 100 },
                { TipoRecurso.Oro, 0 },
                { TipoRecurso.Piedra, 0 }
            };
            
            
            // inicializar listas
            this.aldeanos = new List<Aldeano>();
            this.edificios = new List<Edificio>();
            
            // seput de población inicial
            this.poblacionActual = 4;
            this.poblacionMaxima = 15;
            
            // crear unidades y edificios al comienzo de la partida
            
        }

        public Player(string nombreJugador1, Civilizacion civ1)
        {
            this.name = nombreJugador1;
            this.civilizacion = civ1;
            // this.accesible = true;

            // valores de inicio de juego
            this.recursos = new Dictionary<TipoRecurso, int>()
            {
                { TipoRecurso.Alimento, 100 },
                { TipoRecurso.Madera, 100 },
                { TipoRecurso.Oro, 0 },
                { TipoRecurso.Piedra, 0 }
            };
            
            // inicializar listas
            this.aldeanos = new List<Aldeano>();
            this.edificios = new List<Edificio>();
            
            // setup de población inicial
            this.poblacionActual = 4;
            this.poblacionMaxima = 15;
            
            // inicializar unidades vacías
            this.Unidades = new List<Unidad>();
        }

        /// <summary>
        /// Nombre del jugador
        /// </summary>
        public string Nombre => name;
        
        /// <summary>
        /// Civilización del jugador
        /// </summary>
        public Civilizacion Civilizacion => civilizacion;

        /// <summary>
        /// Población actual del jugador
        /// </summary>
        public int PoblacionActual => poblacionActual;

        /// <summary>
        /// Población máxima del jugador
        /// </summary>
        public int PoblacionMaxima => poblacionMaxima;

        /// <summary>
        /// Inicializa el juego siguiendo especificaciones
        /// </summary>
        public void InicializarJuego() 
        {
            var centroCivico = new CentroCivico(new Coordenada(50, 50), 100, this, 10);
            edificios.Add(centroCivico);
            
            
            // crear primeros 3 aldeanos 
            for (int i = 0; i < 3; i++)
            { 
                var coordenada = new Coordenada(50 + i, 50); 
                var aldeano = new Aldeano(i + 1, coordenada, this);
                aldeanos.Add(aldeano); //coloca los aldeanos en coordenadas
            }

            poblacionActual += 3; //actualiza la posición
        }

        /// <summary>
        /// Verifica si el jugador ganó
        /// </summary>
        /// <returns>Retorna true si ganó, false si no</returns>
        public bool Victoria()
        {
            return edificios.Any(edificio => edificio is CentroCivico);
        }
        
        /// <summary>
        /// Determina si puede o no crear más unidades
        /// </summary>
        /// <returns>True si puede, False si no</returns>
        public bool PuedeCrearUnidad() //IMPLEMENTADO
        {
            return poblacionActual < poblacionMaxima;
        }

        /// <summary>
        /// Agrega recursos al inventario
        /// </summary>
        /// <param name="tipo">El tipo de recurso a agregar</param>
        /// <param name="cantidad">La cantidad que se agrega</param>
        public void AgregarRecurso(TipoRecurso tipo, int cantidad) 
        {
            if (recursos.ContainsKey(tipo)) //lo añade solo si existe
            {
                recursos[tipo] += cantidad;
            }
        }

        /// <summary>
        /// Determina si el jugador tiene recursos para poder pagar los costos específicos
        /// </summary>
        /// <param name="costos">Diccionario con los costos</param>
        /// <returns>True si tiene los recursos, False si no</returns>
        public bool TieneRecursos(Dictionary<TipoRecurso, int> costos) 
        {
            foreach (var costo in costos) //verifica el tipo de recurso requerido
            {
                if (!recursos.ContainsKey(costo.Key) || recursos[costo.Key] < costo.Value)
                {
                    return false; 
                }
            }

            return true;
        }

        /// <summary>
        /// Gasta recursos del inventario
        /// </summary>
        /// <param name="costos">Diccionario con los costos</param>
        public void GastarRecursos(Dictionary<TipoRecurso, int> costos) 
        {
            if (TieneRecursos(costos)) //verifica que tiene recursos
            {
                foreach (var costo in costos)
                {
                    recursos[costo.Key] -= costo.Value; //si tiene recursos resta del inventario
                }
            }
        }
        
        /// <summary>
        /// Construye un edificio si tiene los recursos
        /// </summary>
        /// <param name="edificio">Edificio que se va a construir</param>
        /// <param name="ubicación">Ubicación en donde se va a construir</param>
        public void Construir(Edificio edificio, Coordenada ubicación) 
        {
            if (TieneRecursos(edificio.ObtenerCosto())) //verifica si puede pagar la construcción
            {
                GastarRecursos(edificio.ObtenerCosto());
                edificios.Add(edificio);

                if (edificio is Casa) //si es casa aumenta la población 
                {
                    poblacionMaxima += 5;
                }
            }
        }
        
        /// <summary>
        /// Busca un aldeano disponible
        /// </summary>
        /// <returns>Aldeano o null si no encuentra ninguno</returns>
        public Aldeano GetAldeanoDisponible() 
        {
            return aldeanos.FirstOrDefault(a => a.EstaDisponible());
        }
        
        /// <summary>
        /// Le da la tarea a un aldeano de recolectar recursos en una ubicacion específica
        /// </summary>
        /// <param name="tipo">Tipo de recurso</param>
        /// <param name="ubicacion">Ubicación para recolectarlo</param>
        public void RecolectarRecurso(TipoRecurso tipo, Coordenada ubicacion) 
        {
            var aldeano = GetAldeanoDisponible(); //busca aldeano disponible
            if (aldeano != null)
            {
                aldeano.IniciarRecoleccion(tipo, ubicacion); //le asigna tarea de recolectar
            
            }
        }
        
        /// <summary>
        /// Obtiene la cantidad de un tipo de recurso
        /// </summary>
        /// <param name="tipo">Tipo de recurso que se va a obtener la cantidad</param>
        /// <returns>Cantidad del recurso</returns>
        public int GetRecurso(TipoRecurso tipo) 
        {
            return recursos.ContainsKey(tipo) ? recursos[tipo] : 0;
        }

        public IEnumerable Unidades { get; set; }
    }
}