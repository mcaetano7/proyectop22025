namespace Library
{
    public class RecursoJugador
    {
        public string Tipo { get; set; }
        public int Cantidad { get; set; }
        
        public RecursoJugador() { }

        public RecursoJugador(string tipo, int cantidad)
        {
            Tipo = tipo;
            Cantidad = cantidad;
        }
        
        
    }
}
