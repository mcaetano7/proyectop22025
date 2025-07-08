namespace Library;
/// <summary>
/// representa un edificio generico
///
///es una clase abstracta que debe ser heredada por ciertos tipos de edificio
/// </summary>
public abstract class Edificio
{
    /// <summary>
    /// coordenada que indica la ubicación del edificio en el mapa
    /// </summary>
    public Coordenada Ubicacion { get; set; }
    
    /// <summary>
    /// Puntos de vida del edificio
    /// </summary>
    public int Vida { get; set; }
    /// <summary>
    ///dueño del edificio
    /// </summary>
    public Player Owner { get; set; }
    
    /// <summary>
    /// Resistencia extra que puede ser usada para reducir daño
    /// </summary>
    public int Resistencia { get; set; } 

    /// <summary>
    /// crea una nueva instancia del edificio con ubicación, vida inicial y dueño
    /// </summary>
    /// <param name="ubicacion">ubicacion en el mapa</param>
    /// <param name="vida">puntos de vida iniciales</param>
    /// <param name="owner">Jugador dueño del edificio</param>
    public Edificio(Coordenada ubicacion, int vida, Player owner)
    {
        Ubicacion = ubicacion;
        Vida = vida;  
        Owner = owner;
    }
    
    /// <summary>
    /// almacena recursos dentro del edificio
    /// </summary>
    /// <param name="recursosJugador"></param>
    public abstract void Almacenar(RecursoJugador recursosJugador);

    /// <summary>
    /// retorna el costo de construcción del edificio con respecto a los recursos
    /// Debe ser implementado por cada subclase
    /// </summary>
    /// <returns>un diccionario que asocia tipo de recurso y cantidad necesaria</returns>
    public abstract Dictionary<TipoRecurso?, int> ObtenerCosto();
    
    /// <summary>
    /// daña al edificio reduciendo su vida
    /// </summary>
    /// <param name="damage">cantidad de daño recibido</param>
    public virtual void RecibirDamage(int damage)
    {
        Vida = Math.Max(0, Vida - damage);
    }

    /// <summary>
    /// indica si el edificio tiene vida igual o menor a 0
    /// </summary>
    /// <returns><c>true</c> si el edificio está destruido, si no <c>false</c>.</returns>
    // Método para saber si el edificio está destruido
    public bool EstaMuerto()
    {
        return Vida <= 0;
    }

}
    
