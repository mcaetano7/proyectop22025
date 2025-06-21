namespace Library;

/* esta clase le da nombre a cada tipo de recurso */

public class TipoRecurso
{
    public string Nombre { get; }

    private TipoRecurso(string nombre)
    {
        Nombre = nombre;
    }
    
    public static readonly TipoRecurso Alimento = new TipoRecurso("Alimento");
    public static readonly TipoRecurso Madera = new TipoRecurso("Madera");
    public static readonly TipoRecurso Oro = new TipoRecurso("Oro");
    public static readonly TipoRecurso Piedra = new TipoRecurso("Piedra");

}