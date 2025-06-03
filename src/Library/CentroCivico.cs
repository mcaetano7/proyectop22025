namespace Library;

public class CentroCivico : Edificio
{
    public int CapacidadAldeanos { get; private set; }
    private List<Aldeano> aldeanos;

    public CentroCivico(Coordenada ubicacion, int vida, Player owner, int capacidadAldeanos)
        : base(ubicacion, vida, owner)
    {
        CapacidadAldeanos = capacidadAldeanos;
        aldeanos = new List<Aldeano>();
    }

    public bool AlojarAldeano(Aldeano aldeano)
    {
        if (aldeanos.Count < CapacidadAldeanos)
        {
            aldeanos.Add(aldeano);
            return true;
        }

        return false; // si no hay capacidad
    }

    public bool SacarAldeano(Aldeano aldeano)
    {
        return aldeanos.Remove(aldeano);
    }

    public int CapacidadRestante()
    {
        return CapacidadAldeanos - aldeanos.Count;
    }

    public override Dictionary<TipoRecurso, int> obtenerCosto()
    {
        return new Dictionary<TipoRecurso, int>()
        {
            { TipoRecurso.Madera, 200 }
        };
    }
}