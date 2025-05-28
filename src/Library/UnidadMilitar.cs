namespace Library;

public class UnidadMilitar : Unidad
{
   public int Ataque { get; set; }
   
   public UnidadMilitar(int id, Coordenada ubicacion, int vida, int velocidad, Player owner, int ataque)
      : base(id, ubicacion, vida, velocidad, owner)
          
   {
       Ataque = ataque; 
   }
   
   public void Mover(Coordenada nuevaUbicacion)
   {
       //implementar codigo if mapa tiene posicion válida blah blah 
       //para eso crear clase mapa para ver los métodos 
   }
   
   public void Atacar(Unidad objetivo)
   {
       // idem lo de arriba, ver como puede atacar la unidad militar 
   }
   
   public int Defensa { get; set; } 

   public void Defender()
   {
       //l{ogica para ver el daño que recibio y actualizar la vida d la unidad
   }
   
   
}