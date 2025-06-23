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
        private bool accesible;
        private int poblacionActual; 
        private int poblacionMaxima; 
        
        /// <summary>
        /// Constructor para inicializar al jugador con civilización
        /// </summary>
        /// <param name="name">Nombre del jugador</param>
        /// <param name="civilizacion">Civilización del jugador</param>
        public Player(string name, Civilizacion civilizacion) 
        {
            this.name = name;
            this.civilizacion = civilizacion;
            this.accesible = true; 

            // valores de incio de juego
            this.recursos = new Dictionary<TipoRecurso, int>()
            {
                { TipoRecurso.Alimento, 100 },
                { TipoRecurso.Madera, 100 },
                { TipoRecurso.Oro, 0 },
                { TipoRecurso.Piedra, 0 }
            };
            
            
            // inicilizar listas
            this.aldeanos = new List<Aldeano>();
            this.edificios = new List<Edificio>();
            
            // seput de poblacion incial
            this.poblacionActual = 4;
            this.poblacionMaxima = 15;
            
            // crear unidades y edificios al comienzo de la partida
            
        }
        public string Nombre => name;
        public Civilizacion Civilizacion => civilizacion;

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

        public bool Victoria() 
        {
            // se pierde la partida cuando no hay ningun centro urbano, entre otras condiciones
            return !edificios.Any(edificio => edificio is CentroCivico);
        }
        
        public bool PuedeCrearUnidad() 
        {
            return poblacionActual < poblacionMaxima;
        }

        public void AgregarRecurso(TipoRecurso tipo, int cantidad) 
        {
            if (recursos.ContainsKey(tipo)) //lo añade solo si existe
            {
                recursos[tipo] += cantidad;
            }
        }

        public bool TieneRecursos(Dictionary<TipoRecurso, int> costos) 
        {
            foreach (var costo in costos) //verifica el tipo de recurso requerido
            {
                if (!recursos.ContainsKey(costo.Key) || recursos[costo.Key] < costo.Value)
                {
                    return false; //si no tiene el recurso o la cantidad necesaria
                }
            }

            return true;
        }

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

        public void Construir(Edificio edificio, Coordenada ubicación) 
        {
            if (TieneRecursos(edificio.obtenerCosto())) //verifica si puede pagar la construcción
            {
                GastarRecursos(edificio.obtenerCosto());
                edificios.Add(edificio);

                if (edificio is Casa) //si es casa aumenta la población 
                {
                    poblacionMaxima += 5;
                }
            }
        }
        

        public Aldeano GetAldeanoDisponible() 
        {
            return aldeanos.FirstOrDefault(a => a.EstaDisponible());
        }
        
        public void RecolectarRecurso(TipoRecurso tipo, Coordenada ubicacion) 
        {
            var aldeano = GetAldeanoDisponible(); //busca aldeano disponible
            if (aldeano != null)
            {
                aldeano.IniciarRecoleccion(tipo, ubicacion); //le asigna tarea de recolectar
            
            }
        }
        public int GetRecurso(TipoRecurso tipo) 
        {
            return recursos.ContainsKey(tipo) ? recursos[tipo] : 0;
        }
    }
}