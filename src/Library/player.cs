using System.Collections.Generic;

using Library;

namespace Library
{

    public class Player
    {
        private string name;
        private Civilizacion civilizacion;
        private Dictionary<TipoRecurso, int> recursos;
        private List<Aldeano> aldeanos;
        private List<Edificio> edificios;
        private List<Unidad> unidades;
        private bool accesible;
        private int poblacionActual;
        private int poblacionMaxima;
        

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
            this.unidades = new List<Unidad>();
            
            // seput de poblacion incial
            this.poblacionActual = 4;
            this.poblacionMaxima = 15;
            
            // crear unidades y edificios al comienzo de la partida
            
        }

        public void InicializarJuego()
        {
            var centroCivico = new CentroCivico(new Coordenada(50, 50), 100, this, 10);
            edificios.Add(centroCivico);
            
            
            // crear primeros 3 aldeanos 
            for (int i = 0; i < 3; i++)
            { // this es owner pero no se que iria
                var coordenada = new Coordenada(50 + i, 50);
                var aldeano = new Aldeano(i + 1, coordenada, this);
                aldeanos.Add(aldeano);
                unidades.Add(aldeano);
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

        public void AgregarRecurso(TipoRecurso tipo, int cantidad)
        {
            if (recursos.ContainsKey(tipo))
            {
                recursos[tipo] += cantidad;
            }
        }

        public bool TieneRecursos(Dictionary<TipoRecurso, int> costos) // ex info()
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
        
        public void EntrenarUnidad(Unidad tipo)
        {
            
        }
    }
}