using System.Collections.Generic;

namespace Library
{

    public class Player
    {
        private string name;
        private Civilizacion civilizacion;
        private Recursos recursos;
        private List<Aldeano> aldeanos;
        private List<Edificio> edificios;
        private List<UnidadMilitar> unidades;
        private bool accesible;

        public Player(string name, Civilizacion civilizacion)
        {
            this.name = name;
            this.civilizacion = civilizacion;
            this.recursos = new Recursos();
            this.aldeanos = new List<Aldeano>();
            this.edificios = new List<Edificio>();
            this.unidades = new List<UnidadMilitar>();
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

        public void MoverUnidad(int idUnidad, Coordenada destino)
        {
            
        }

        public void Atacar(int idAtacante, Coordenada objetivo)
        {
            
        }

        public bool Accesible => accesible;






    }
}