namespace Library
{
    public class Civilizacion
    {
        public string Name { get; set; }
        public List<string> Bonificaciones { get; set; } 
        public TipoUnidad UnidadEspecial { get; set; }

        public Civilizacion(string name, List<string> Bonificaciones, TipoUnidad unidadEspecial)
        {
        Name = name;
        Bonificaciones = Bonificaciones;
        UnidadEspecial = unidadEspecial;
        }
    }
    
}

