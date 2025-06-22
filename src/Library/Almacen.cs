namespace Library;
//clase que representa un edificio almacén
public class Almacen : Edificio
{
    public string Tipo { get; set; }
    public int Capacidad { get; set; }

    public Almacen(Coordenada ubicacion, int vida, Player owner, string tipo, int capacidad)
        : base(ubicacion, vida, owner)
    {
        Tipo = tipo;
        Capacidad = capacidad;
    }

    public override Dictionary<TipoRecurso, int> obtenerCosto() //método que retorna cuanto cuesta construir un almacen (cuanto de recursos)
    {
        return new Dictionary<TipoRecurso, int>() //usamos diccionario porque as{i podemos buscar directamente por tipo de recurso y asociar m{as facil el tipo de recurso con la cantidad necesaria
        {
            { TipoRecurso.Madera, 500 }
        };
    }
}
