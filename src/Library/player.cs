using System.Collections.Generic;

using Library;

namespace Library
{

    public class Player
    {
        private string name;
        private Civilizacion civilizacion;
        private Recurso recurso;
        private List<Aldeano> aldeanos;
        private List<Edificio> edificios;
        private List<Unidad> unidades;
        private bool accesible;

        public Player(string name, Civilizacion civilizacion)
        {
            this.name = name;
            this.civilizacion = civilizacion;
            this.recurso = new Recurso();
            this.aldeanos = new List<Aldeano>();
            this.edificios = new List<Edificio>();
            this.unidades = new List<Unidad>();
            this.accesible = true;
        }

        public void Info()
        {
            
        }

        public void RecolectarRecurso(TipoRecurso tipo, Coordenada ubicacion)
        {
            
        }

        public void Construir(Edificio edificio, Coordenada ubicacion)
        {
            
        }

        public void EntrenarUnidad(TipoUnidad tipo)
        {
            
        }






    }
}