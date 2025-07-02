namespace Library;
/// <summary>
/// clase que representa una casa, permite alojar cierta cantidad de población
/// </summary>
public class Casa : Edificio
{
    /// <summary>
    /// capacidad poblacion (cuantos individuos está alojando esta casa)
    /// </summary>
    public int CapacidadPoblacion { get; set; }
   
    /// <summary>
    /// capacidad maxima que puede alojar la casa
    /// </summary>
    public int CapacidadMaxima = 5; 
    
    
    /// <summary>
    /// construye la casa
    /// </summary>
    /// <param name="ubicacion"> donde se encuentra ubicada en el mapa</param>
    /// <param name="vida">puntos de vida </param>
    /// <param name="owner"> a que jugador le pertenece</param>
    /// <param name="tipo"></param>
    /// <param name="capacidad"> capacidad de la casa</param>
  public Casa(Coordenada ubicacion, int vida, Player owner, string tipo, int capacidad) //crea una casa
      : base(ubicacion, 100, owner)
  {
      CapacidadPoblacion = 0;
      Resistencia = 500;
  }

    /// <summary>
    /// devuelve el costo de contruir una casa (materiales)
    /// </summary>
    /// <returns> diccionario con  el tipo de recurso y la cantidad necesaria</returns>
  public override Dictionary<TipoRecurso, int> ObtenerCosto() 
  {
      return new Dictionary<TipoRecurso, int>
      {
          { TipoRecurso.Madera, 25 }
      };
  }
}