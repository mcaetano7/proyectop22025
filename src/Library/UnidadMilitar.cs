namespace Library
{
    /// <summary>
    /// clase que representa una unidad militar con ataque y defensa
    /// </summary>
    public class UnidadMilitar : Unidad
    {
        /// <summary>
        /// valor de ataque de la unidad
        /// </summary>
        public int Ataque { get; set; }

        /// <summary>
        /// valor de defensa de la unidad
        /// </summary>
        public int Defensa { get; set; }

        private int cantidad;

        /// <summary>
        /// constructor que inicializa la unidad militar con sus atributos
        /// </summary>
        /// <param name="id">identificador único</param>
        /// <param name="ubicacion">posición actual en el mapa</param>
        /// <param name="vida">vida actual</param>
        /// <param name="velocidad">velocidad de movimiento</param>
        /// <param name="owner">jugador dueño de la unidad</param>
        /// <param name="ataque">valor de ataque</param>
        /// <param name="defensa">valor de defensa</param>
        public UnidadMilitar(int id, Coordenada ubicacion, int vida, int velocidad, Player owner, int ataque, int defensa)
            : base(id, ubicacion, vida, velocidad, owner)
        {
            Ataque = ataque;
            Defensa = defensa;
            cantidad = 0;
        }

        /// <summary>
        /// recibe daño y lo aplica descontando la defensa
        /// </summary>
        /// <param name="damage">daño recibido</param>
        public override void RecibirDamage(int damage)
        {
            int damageFinal = Math.Max(0, damage - Defensa);
            Vida = Math.Max(0, Vida - damageFinal);
        }

        /// <summary>
        /// mueve la unidad a una nueva ubicación si es válida
        /// </summary>
        /// <param name="nuevaUbicacion">nueva posición</param>
        /// <exception cref="InvalidOperationException">si la ubicación no es válida</exception>
        public override void Mover(Coordenada nuevaUbicacion)
        {
            if (nuevaUbicacion.EsValida())
            {
                this.Ubicacion = nuevaUbicacion;
            }
            else
            {
                throw new InvalidOperationException("ubicacion no valida");
            }
        }

        /// <summary>
        /// ataca a otra unidad si no está muerta
        /// </summary>
        /// <param name="objetivo">unidad objetivo del ataque</param>
        public void Atacar(Unidad objetivo)
        {
            if (objetivo == null || objetivo.EstaMuerto())
                return;

            int damage = Math.Max(0, this.Ataque - objetivo.RecibirDefensa());
            objetivo.RecibirDamage(damage);
        }

        /// <summary>
        /// aumenta la defensa de la unidad
        /// </summary>
        public void Defender()
        {
            this.Defensa += 5;
        }
    }
}
