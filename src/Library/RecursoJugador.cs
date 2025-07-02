namespace Library
{
    /// <summary>
    /// esta clase define el tipo y la cantidad de un recurso que tiene un jugador.
    /// ejemplo: madera, 100
    /// </summary>
    public class RecursoJugador
    {
        /// <summary>
        /// tipo de recurso (madera, comida, etc.)
        /// </summary>
        public string Tipo { get; set; }

        /// <summary>
        /// cuánto hay de ese recurso
        /// </summary>
        public int Cantidad { get; set; }

        /// <summary>
        /// constructor vacío
        /// </summary>
        public RecursoJugador()
        {
            
        }

        private Dictionary<TipoRecurso, int> recursos = new();

        public void Almacenar(TipoRecurso tipo, int cantidad)
        {
            if (!recursos.ContainsKey(tipo))
            {
                recursos[tipo] = 0;
            }
            recursos[tipo] += cantidad;
        }

        public int Obtener(TipoRecurso tipo)
        {
            return recursos.TryGetValue(tipo, out int cantidad) ? cantidad : 0;
        }
        
        /// <summary>
        /// constructor con tipo y cantidad
        /// </summary>
        /// <param name="tipo">el tipo de recurso</param>
        /// <param name="cantidad">la cantidad que tiene el jugador</param>
        public RecursoJugador(string tipo, int cantidad)
        {
            Tipo = tipo;
            Cantidad = cantidad;
        }
    }
}