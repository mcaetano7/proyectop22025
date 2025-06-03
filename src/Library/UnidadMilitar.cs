namespace Library;

public class UnidadMilitar : Unidad
{
   public int Ataque { get; set; }
   public int Defensa { get; set; }
   private int cantidad;
   public UnidadMilitar(int id, Coordenada ubicacion, int vida, int velocidad, Player owner, int ataque, int defensa)
      : base(id, ubicacion, vida, velocidad, owner)
   {
       Ataque = ataque;
       Defensa = defensa;
       cantidad = 0;
   }

   public override void RecibirDamage(int damage)
   {
       int damageFinal = Math.Max(0, damage - Defensa);
       Vida = Math.Max(0, Vida - damageFinal);
   }

   public override void Mover(Coordenada nuevaUbicacion)
   {
       if (nuevaUbicacion.EsValida())
       {
           this.Ubicacion = nuevaUbicacion;
       }
       else
       {
           throw new InvalidOperationException("Ubicacion no valida"); // chagpt ????
       }
   } 
   
   public void Atacar(Unidad objetivo)
   {
       // idem lo de arriba, ver como puede atacar la unidad militar 
       if (objetivo == null || objetivo.EstaMuerto())
           return;

       int damage = Math.Max(0, this.Ataque - objetivo.RecibirDefensa());
       objetivo.RecibirDamage(damage);
   }

   public void Defender()
   {
       //logica para ver el daño que recibio y actualizar la vida de la unidad
       this.Defensa += 5;
   }
   
}