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

        private Dictionary<TipoRecurso, int> recursos = new();

        public void Almacenar(TipoRecurso tipo, int cantidad)
        {
            if (!recursos.ContainsKey(tipo))
            {
                recursos[tipo] = 0;
            }
            recursos[tipo] += cantidad;
        }
        
        public bool ContieneRecurso(TipoRecurso tipo)
        {
            return recursos.ContainsKey(tipo) && recursos[tipo] > 0;
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
        
        /// <summary>
        /// obtiene la cantidad de recursos
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public int ObtenerCantidad(TipoRecurso tipo)
        {
            return recursos.ContainsKey(tipo) ? recursos[tipo] : 0;
        }
        
        /// <summary>
        /// elimina
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="cantidad"></param>
        public void Descontar(TipoRecurso tipo, int cantidad)
        {
            if (recursos.ContainsKey(tipo))
            {
                recursos[tipo] = Math.Max(0, recursos[tipo] - cantidad);
            }
        }
        
        /// <summary>
        /// agrega
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="cantidad"></param>
        public void Agregar(TipoRecurso tipo, int cantidad) //falta implementar este metodo
        {
            if (!recursos.ContainsKey(tipo))
                recursos[tipo] = 0;

            recursos[tipo] += cantidad;
        }
    }
}