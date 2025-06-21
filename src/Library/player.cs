using System.Collections.Generic;

using Library;

namespace Library
{

    public class Player
    {
        private string name; //nombre del jugador
        private Civilizacion civilizacion; //civilizacion que va a pertenecer
        private Dictionary<TipoRecurso, int> recursos; //diccionario con los recursos y cantidades
        private List<Aldeano> aldeanos; //lista de aldeanos
        private List<Edificio> edificios; //lista de los edificios del jugador
        private bool accesible; // vivo o muerto
        private int poblacionActual; //unidades actuales de poblacion
        private int poblacionMaxima; //limite maximo de poblacion
        

        public Player(string name, Civilizacion civilizacion) //inicializa un jugador con civilizacion
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

        public void InicializarJuego() //inicializa el juego con estructuras y unidades
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

        public bool Victoria() //verifica si el jugador perdió la partida
        {
            // se pierde la partida cuando no hay ningun centro urbano, entre otras condiciones
            return !edificios.Any(edificio => edificio is CentroCivico);
        }
        
        public bool PuedeCrearUnidad() //verifica si se puede crear más unidades
        {
            return poblacionActual < poblacionMaxima;
        }

        public void AgregarRecurso(TipoRecurso tipo, int cantidad) //añade recurso al inventario
        {
            if (recursos.ContainsKey(tipo)) //lo añade solo si existe
            {
                recursos[tipo] += cantidad;
            }
        }

        public bool TieneRecursos(Dictionary<TipoRecurso, int> costos) //verifica que el jugador tiene recursos para pagar costo especifico
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

        public void GastarRecursos(Dictionary<TipoRecurso, int> costos) //gasta recursos del inventario del jugador
        {
            if (TieneRecursos(costos)) //verifica que tiene recursos
            {
                foreach (var costo in costos)
                {
                    recursos[costo.Key] -= costo.Value; //si tiene recursos resta del inventario
                }
            }
        }

        public void Construir(Edificio edificio, Coordenada ubicación) //construye un edificio si se tiene los recursos necesarios
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
        

        public Aldeano GetAldeanoDisponible() //busca y retorna el primer aldeano para tareas
        {
            return aldeanos.FirstOrDefault(a => a.EstaDisponible());
        }
        
        public void RecolectarRecurso(TipoRecurso tipo, Coordenada ubicacion) //asigna a un aldeano la tarea de recolectar un recurso en una ubicación específica
        {
            var aldeano = GetAldeanoDisponible(); //busca aldeano disponible
            if (aldeano != null)
            {
                aldeano.IniciarRecoleccion(tipo, ubicacion); //le asigna tarea de recolectar
            
            }
        }
    }
}