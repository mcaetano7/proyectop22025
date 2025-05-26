namespace Library
{
    public class Civilizacion
    {
        public string Name { get; set; }
        public List<string> Bonificaciones { get; set; } 
        public Unidad UnidadEspecial { get; set; }

        public Civilizacion(string name, List<string> Bonificaciones, Unidad unidadEspecial)
        {
        Name = name;
        Bonificaciones = Bonificaciones;
        UnidadEspecial = unidadEspecial;
        }
    }
    
}

