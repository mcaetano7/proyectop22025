namespace Library
{
    public class Recurso
    {
        public string Tipo { get; set; }
        public int Cantidad { get; set; }
        
        public Recurso() { }

        public Recurso(string tipo, int cantidad)
        {
            Tipo = tipo;
            Cantidad = cantidad;
        }
    }
}
