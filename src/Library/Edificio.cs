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

    }