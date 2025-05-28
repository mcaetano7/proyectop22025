namespace Library;

public class UnidadMilitar : Unidad
{
   public int Ataque { get; set; }
   public int Defensa { get; set; }
   private int cantidad;
   public UnidadMilitar(int id, Coordenada ubicacion, int vida, int velocidad, Player owner, int ataque)
      : base(id, ubicacion, vida, velocidad, owner)
          
   {
       Ataque = ataque;
       Defensa = defensa;
       cantidad = 0;
   }

   public override void RecibirDamage(int damage)
   {
       cantidad = damage;
   }

   public override void Mover(Coordenada nuevaUbicacion)
   {
       if (Mapa.EsPosicionValida(nuevaUbicacion))
       {
           this.Ubicacion = nuevaUbicacion;
       }
       else
       {
           throw new InvalidOperationException("Ubicacion no valida");
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
       //l{ogica para ver el daño que recibio y actualizar la vida d la unidad
       this.Defensa += 5;
   }
   
}