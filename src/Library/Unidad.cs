namespace Library;

/// <summary>
/// representa una unidad del juego con ubicación, vida, velocidad y dueño(jugador 1 o 2)
/// </summary>
public abstract class Unidad
{
    /// <summary>
    /// identificador único de la unidad
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// coordenada actual de la unidad
    /// </summary>
    public Coordenada Ubicacion { get; set; }

    /// <summary>
    /// puntos de vida de la unidad
    /// </summary>
    public int Vida { get; set; }

    /// <summary>
    /// tiempo de creación de la unidad
    /// </summary>
    public int Velocidad { get; set; }

    /// <summary>
    /// jugador que posee esta unidad
    /// </summary>
    public Player Owner { get; set; }

    /// <summary>
    /// constructor de la clase Unidad
    /// </summary>
    /// <param name="id">identificador de la unidad</param>
    /// <param name="ubicacion">ubicación inicial de la unidad</param>
    /// <param name="vida">puntos de vida iniciales</param>
    /// <param name="velocidad">velocidad de la unidad</param>
    /// <param name="owner">jugador dueño de la unidad</param>
    public Unidad(int id, Coordenada ubicacion, int vida, int velocidad, Player owner)
    {
        Id = id;
        Ubicacion = ubicacion;
        Vida = vida;
        Velocidad = velocidad;
        Owner = owner;
    }

    /// <summary>
    /// devuelve si la unidad está muerta (vida menor o igual a cero)
    /// </summary>
    /// <returns>true si la unidad está muerta</returns>
    public bool EstaMuerto() => Vida <= 0;

    /// <summary>
    /// devuelve el valor de defensa de la unidad (por defecto 0)
    /// </summary>
    /// <returns>valor de defensa</returns>
    public virtual int RecibirDefensa() => 0;

    /// <summary>
    /// resta puntos de vida a la unidad al recibir daño
    /// </summary>
    /// <param name="damage">cantidad de daño recibido</param>
    public virtual void RecibirDamage(int damage)
    {
        Vida = Math.Max(0, Vida - damage);
    }

    /// <summary>
    /// mueve la unidad a una nueva ubicación
    /// </summary>
    /// <param name="nuevaUbicacion">coordenada de destino</param>
    public abstract void Mover(Coordenada nuevaUbicacion);
}
