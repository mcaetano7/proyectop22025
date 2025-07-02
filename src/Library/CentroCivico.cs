namespace Library;
/// <summary>
/// representa un centro civico que puede alojar aldeanos y es propiedad del jugador
/// hereda de <see cref="Edificio"/>.
/// </summary>
public class CentroCivico : Edificio
{
    /// <summary>
    /// obtiene la capacidad maxima de aldeanos que el centro civico puede tener
    /// </summary>
    public int CapacidadAldeanos { get; private set; }
    private List<Aldeano> aldeanos;
    
    /// <summary>
    /// crea una nueva instancia de la clase <see cref="CentroCivico"/>.
    /// </summary>
    /// <param name="ubicacion">ubicación del centro cívico en el mapa</param>
    /// <param name="vida">puntos de vida del edificio</param>
    /// <param name="owner">jugador propietario del centro civico</param>
    /// <param name="capacidadAldeanos">cantidad max de aldeanos que se pueden tener</param>

    public CentroCivico(Coordenada ubicacion, int vida, Player owner, int capacidadAldeanos)
        : base(ubicacion, vida, owner)
    {
        CapacidadAldeanos = capacidadAldeanos;
        aldeanos = new List<Aldeano>();
    }

    /// <summary>
    /// intenta alojar un aldeano en el centro civico
    /// </summary>
    /// <param name="aldeano">aldeano a alojar</param>
    public bool AlojarAldeano(Aldeano aldeano)
    {
        if (aldeanos.Count < CapacidadAldeanos)
        {
            aldeanos.Add(aldeano);
            return true;
        }

        return false; // si no hay capacidad
    }

    /// <summary>
    /// elimina un aldeano alojado del centro civico
    /// </summary>
    /// <param name="aldeano">Aldeano a retirar.</param>
    public bool SacarAldeano(Aldeano aldeano)
    {
        return aldeanos.Remove(aldeano);
    }

    /// <summary>
    /// retorna la cantidad de espacio restante disponible para aldeanos
    /// </summary>
    public int CapacidadRestante()
    {
        return CapacidadAldeanos - aldeanos.Count;
    }

    /// <summary>
    /// devuelve el costo del centro civico con respecto a los recursos
    /// </summary>
    public override Dictionary<TipoRecurso, int> ObtenerCosto()
    {
        return new Dictionary<TipoRecurso, int>()
        {
            { TipoRecurso.Madera, 200 }
        };
    }
    
    public Aldeano EntrenarAldeano()
    {
        if (aldeanos.Count >= CapacidadAldeanos)
            throw new InvalidOperationException("No hay capacidad para entrenar más aldeanos");
        
        int nuevoId = aldeanos.Count + 1;
        Aldeano nuevoAldeano = new Aldeano(nuevoId, this.Ubicacion, this.Owner);
        aldeanos.Add(nuevoAldeano);
    
        return nuevoAldeano;
    }

}