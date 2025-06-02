namespace Library;
//recordar que en el cuartel se entrenan unidadesss
public class Cuartel : Edificio
{
    public Cuartel(Coordenada ubicacion, Player owner)
        : base(ubicacion, vida: 100, owner)
    {
    }

    public Infanteria EntrenarInfanteria(int id)
    {
        return new Infanteria(id, this.Ubicacion, this.Owner);
    }
                                                               //cada uno de los métodos crea una unidad
    public Caballeria EntrenarCaballeria(int id)
    {
        return new Caballeria(id, this.Ubicacion, this.Owner);
    }

    public Arquero EntrenarArquero(int id)
    {
        return new Arquero(id, this.Ubicacion, this.Owner);
    }

    public override Dictionary<TipoRecurso, int> obtenerCosto()
    {
        return new Dictionary<TipoRecurso, int>()
        {
            { TipoRecurso.Madera, 125 }
        };
    }
}