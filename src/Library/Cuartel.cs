namespace Library;
//recordar que en el cuartel se entrenan unidadesss
/// <summary>
/// entrena unidades militares, respresenta un edificio
/// </summary>
public class Cuartel : Edificio
{
    /// <summary>
    /// inicializa una instancia de la calse <see cref="Cuartel"/> con ubicación y propietario dados
    /// la vida empieza en 100
    /// </summary>
    /// <param name="ubicacion">ubicacion del cuartel en el mapa</param>
    /// <param name="owner">jugador dueño del cuartel</param>
    public Cuartel(Coordenada ubicacion, Player owner)
        : base(ubicacion, vida: 100, owner)
    {
    }

    /// <summary>
    /// entrena la unidad de infantería
    /// </summary>
    /// <param name="id">identificador de la unidad</param>
    /// <returns>una instancia de <see cref="Infanteria"/></returns>
    public Infanteria EntrenarInfanteria(int id)
    {
        return new Infanteria(id, this.Ubicacion, this.Owner);
    }
    //cada uno de los métodos crea una unidad
    
    /// <summary>
    /// entrena la unidad de caballería
    /// </summary>
    /// <param name="id">identificador dde la unidad</param>
    /// <returns>Una instancia de <see cref="Caballeria"/></returns>
    public Caballeria EntrenarCaballeria(int id)
    {
        return new Caballeria(id, this.Ubicacion, this.Owner);
    }

    /// <summary>
    /// Entrena una nueva unidad de arqueros.
    /// </summary>
    /// <param name="id">identificador de la unidad</param>
    /// <returns>Una instancia de <see cref="Arquero"/></returns>
    public Arquero EntrenarArquero(int id)
    {
        return new Arquero(id, this.Ubicacion, this.Owner);
    }

    public override void Almacenar(RecursoJugador recursosJugador)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// obtiene el costo del cuartel con respecto a los recursos
    /// </summary>
    /// <returns>un diccionario con el tipo de recurso y la cantidad necesaria</returns>

    public override Dictionary<TipoRecurso?, int> ObtenerCosto()
    {
        return new Dictionary<TipoRecurso?, int>()
        {
            { TipoRecurso.Madera, 125 }
        };
    }
}