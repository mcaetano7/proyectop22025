namespace Library
{
    /// <summary>
    /// esta clase le da nombre a cada tipo de recurso
    /// </summary>
    public class TipoRecurso
    {
        /// <summary>
        /// nombre del tipo de recurso
        /// </summary>
        public string Nombre { get; }

        /// <summary>
        /// constructor privado que inicializa el nombre
        /// </summary>
        /// <param name="nombre">el nombre del recurso</param>
        private TipoRecurso(string nombre)
        {
            Nombre = nombre;
        }
        
        public static readonly TipoRecurso? Alimento = new TipoRecurso("Alimento");
        public static readonly TipoRecurso? Madera = new TipoRecurso("Madera");
        public static readonly TipoRecurso? Oro = new TipoRecurso("Oro");
        public static readonly TipoRecurso? Piedra = new TipoRecurso("Piedra");
    }
}