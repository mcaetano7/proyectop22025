namespace Library
{
    /// <summary>
    /// reepresenta una civilizacion disponible en el juego que tiene nombre y bonificaciones
    /// </summary>
    public class Civilizacion
    {
        /// <summary>
        /// nombre de la civilización
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// lista de bonificaiocnes que otorga la civilización
        /// </summary>
        public List<string> Bonificaciones { get; set; }

        /// <summary>
        /// inicializa una nueva instancia de la clase <see cref="Civilizacion"/>
        /// </summary>
        /// <param name="name">Nombre de la civilización</param>
        /// <param name="bonificaciones">lista de bonificaciones que otorga</param>

        public Civilizacion(string name, List<string> bonificaciones)
        {
            Name = name;
            Bonificaciones = bonificaciones;

        }

        /// <summary>
        /// retorna una lista de civilizaciones disponibles para elegir
        /// </summary>
        /// <param name="owner">Jugador consulta las civilizaciones disponibles</param>

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
