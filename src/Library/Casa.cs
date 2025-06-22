namespace Library;

public class Casa : Edificio
{
  //capacidad poblacion (cuantos individuos puede alojar esta casa)
  public int CapacidadPoblacion { get; set; }
  public int CapacidadMaxima = 5; 
  
  

  public Casa(Coordenada ubicacion, int vida, Player owner, string tipo, int capacidad) //crea una casa
      : base(ubicacion, 100, owner)
  {
      Resistencia = 500;
  }

  public override Dictionary<TipoRecurso, int> obtenerCosto() //método que devuelve el costo de construir una casa (25 unidades de madera)
  {
      return new Dictionary<TipoRecurso, int>
      {
          { TipoRecurso.Madera, 25 }
      };
  }
}