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