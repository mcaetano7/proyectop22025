namespace Library;
//esta clase representa a los aldeanos
public class Aldeano : Unidad
{
    private bool ocupado = false; //indica si el aldeano está ocupado (recolectando recursos) o no
    public Aldeano(int id, Coordenada ubicacion, Player owner)
        : base (id, ubicacion, vida: 50, velocidad : 1, owner)
    {}

    public override void Mover(Coordenada nuevaUbicacion)
    {
        Ubicacion = nuevaUbicacion;  //m{etodo que cambia la ubi del aldeano por una nueva (mueve al aldeano)
    }

    public bool EstaDisponible()
    {
        return !ocupado; //devuelve true si el aldeano no está ocuado
    }

    public void IniciarRecoleccion(TipoRecurso tipo, Coordenada ubicacion) //método para que el aldeano recolecte recursos y los acumule con agregar.recurso
    {
        ocupado = true;
        Owner.AgregarRecurso(tipo, 10);
        ocupado = false;
    }
}