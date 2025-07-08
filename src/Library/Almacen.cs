namespace Library;
/// <summary>
/// clase que representa un edificio almacén
/// </summary>
public class Almacen : Edificio
{
    /// <summary>
    /// tipo de recurso que almacena
    /// </summary>
    public TipoRecurso? Tipo { get; set; }
    
    /// <summary>
    /// capacidad máxima que almacena
    /// </summary>
    public int Capacidad { get; set; }

    /// <summary>
    /// construye el edificio almac{en 
    /// </summary>
    /// <param name="ubicacion"> ubi en el mapa</param>
    /// <param name="vida">puntos de vida</param>
    /// <param name="owner"> a que jugador pertenece</param>
    /// <param name="tipo"></param>
    /// <param name="capacidad"> capacidad del almacén </param>
    public Almacen(Coordenada ubicacion, int vida, Player owner,TipoRecurso? tipo, int capacidad)
        : base(ubicacion, vida, owner)
    {
        Tipo = tipo;
        Capacidad = capacidad;
    }

    /// <summary>
    /// almacena recursos del edificio
    /// </summary>
    /// <param name="recursosJugador">contiene los recursos disponibles del jugador</param>
    public override void Almacenar(RecursoJugador recursosJugador)
    {
        if (!recursosJugador.ContieneRecurso(Tipo))
            return;

        int cantidadDisponible = recursosJugador.ObtenerCantidad(Tipo);
        int cantidadAlmacenar = Math.Min(Capacidad, cantidadDisponible);
        
        recursosJugador.Descontar(Tipo, cantidadAlmacenar);
        Capacidad -= cantidadAlmacenar;
    }

    /// <summary>
    /// método que retorna cuanto cuesta construir un almacen (cuanto de recursos)
    /// </summary>
    /// <returns> un diccionario con los tipos de recuros y sus cantidades (usamos diccionario porque asi podemos buscar directamente por tipo de recurso y asociar más facil el tipo de recurso con la cantidad necesaria)</returns>
    public override Dictionary<TipoRecurso?, int> ObtenerCosto() 
    {
        return new Dictionary<TipoRecurso?, int>() 
        {
            { TipoRecurso.Madera, 500 }
        };
    }
}
