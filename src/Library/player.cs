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
        
        private List<Aldeano> aldeanosDisponibles = new();
        public void RecolectarRecurso(string tipo, Coordenada ubicacion, string nombre)
        {
            if (aldeanosDisponibles.Count == 0)
            {
                Console.WriteLine("No hay aldeanos disponibles para recolectar recursos.");
                return;
            }
            var aldeano = aldeanosDisponibles[0];
            aldeanosDisponibles.Remove(aldeano);
            
            var recurso1 = new Recurso(tipo, 10);
            var almacen = new Edificio(tipo);
            
            aldeano.Recolectar(recurso1, almacen);
            Console.WriteLine($"{nombre} recolectó {recurso1.Cantidad} de {tipo.Length}.");
        }

        public void Construir(Edificio edificio, Coordenada ubicacion)
        {
            
        }

        public void EntrenarUnidad(Unidad tipo)
        {
            
        }
    }
}