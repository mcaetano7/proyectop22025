namespace Library;

public abstract class Edificio
{
    public Coordenada Ubicacion { get; set; }
    public int Vida { get; set; }
    public Player Owner { get; set; }
    
    public int Resistencia { get; set; } 

    public Edificio(Coordenada ubicacion, int vida, Player owner)
    {
        Ubicacion = ubicacion;
        Vida = vida;
        Owner = owner;
    }
    
    public void Almacenar(RecursoJugador recursosJugador)
    {
        
    }

    public abstract Dictionary<TipoRecurso, int> obtenerCosto();
    
    public virtual void RecibirDamage(int damage)
    {
        Vida = Math.Max(0, Vida - damage);
    }

    // Método para saber si el edificio está destruido
    public bool EstaMuerto()
    {
        return Vida <= 0;
    }

}
    
