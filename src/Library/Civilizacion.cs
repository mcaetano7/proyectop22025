namespace Library
{
    public class Civilizacion
    {
        public string Name { get; set; }
        public List<string> Bonificaciones { get; set; }

        public Civilizacion(string name, List<string> bonificaciones)
        {
            Name = name;
            Bonificaciones = bonificaciones;

        }

        public static List<Civilizacion> CivDisponibles(Player owner)
        {
            return new List<Civilizacion>
            {
                new Civilizacion("Japoneses", new List<string> { "INSERTAR BONIFICACIONESSSSSSSS" }),                               //ADEM{AS CAMBIAR LOS NOMBRES DE LAS UNIDADES (para que tenga coherencia con las civilizaciones)
                new Civilizacion("Bizatinos", new List<string> { "BONIFICACIONES" }),
                new Civilizacion("Francos", new List<string> { "BONIFICACIONES" })
            };
        }
    }
}
