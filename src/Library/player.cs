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
                aldeanos.Add(aldeano);
            }

            poblacionActual += 3;
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

        public void AgregarRecurso(TipoRecurso tipo, int cantidad) // recolcetar recurso uml 
        {
            if (recursos.ContainsKey(tipo))
            {
                recursos[tipo] += cantidad;
            }
        }

        public bool TieneRecursos(Dictionary<TipoRecurso, int> costos) 
        {
            foreach (var costo in costos)
            {
                if (!recursos.ContainsKey(costo.Key) || recursos[costo.Key] < costo.Value)
                {
                    return false;
                }
            }

            return true;
        }

        public void GastarRecursos(Dictionary<TipoRecurso, int> costos)
        {
            if (TieneRecursos(costos))
            {
                foreach (var costo in costos)
                {
                    recursos[costo.Key] -= costo.Value;
                }
            }
        }

        public void Construir(Edificio edificio, Coordenada ubicacion)
        {
            if (TieneRecursos(edificio.obtenerCosto()))
            {
                GastarRecursos(edificio.obtenerCosto());
                edificios.Add(edificio);

                if (edificio is Casa)
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
            var aldeano = GetAldeanoDisponible();
            if (aldeano != null)
            {
                aldeano.IniciarRecoleccion(tipo, ubicacion);
            
            }
        }
    }
}