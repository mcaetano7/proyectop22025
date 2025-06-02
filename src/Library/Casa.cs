namespace Library;

public class Casa : Edificio
{
  //capacidad poblacion
  public int CapacidadPoblacion { get; set; }
  public int CapacidadMaxima = 5;
  
  

  public Casa(Coordenada ubicacion, int vida, Player owner, string tipo, int capacidad)
      : base(ubicacion, 100, owner)
  {
      Resistencia = 500;
  }

  public override Dictionary<TipoRecurso, int> obtenerCosto()
  {
      return new Dictionary<TipoRecurso, int>
      {
          { TipoRecurso.Madera, 25 }
      };
  }
}