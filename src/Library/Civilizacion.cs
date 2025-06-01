namespace Library
{
    public class Civilizacion
    {
        public string Name { get; set; }
        public List<string> Bonificaciones { get; set; }
        public Unidad UnidadEspecial { get; set; }

        public Civilizacion(string name, List<string> bonificaciones, Unidad unidadEspecial)
        {
            Name = name;
            Bonificaciones = bonificaciones;
            UnidadEspecial = unidadEspecial;
        }

        public static List<Civilizacion> CivDisponibles(Player owner)
        {
            return new List<Civilizacion>
            {
                new Civilizacion("Japoneses", new List<string> { "INSERTAR BONIFICACIONESSSSSSSS" },  //COMPLETAR CON LAS BONIFICACIONES, 
                    new Arquero(1, new Coordenada(0, 0), owner)),                               //ADEM{AS CAMBIAR LOS NOMBRES DE LAS UNIDADES (para que tenga coherencia con las civilizaciones)
                new Civilizacion("Bizatinos", new List<string> { "BONIFICACIONES" },
                    new Infanteria(1, new Coordenada(0, 0), owner)),
                new Civilizacion("Francos", new List<string> { "BONIFICACIONES" },
                    new Caballeria(1, new Coordenada(0, 0), owner))
            };
        }
    }
}
