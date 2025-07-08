namespace Library;
using Library;

/// <summary>
/// comando para mover unidad
/// </summary>
    public class MoverUnidadComando  
    {
        private Unidad unidad;
        private Coordenada destino;
        
        public MoverUnidadComando(Unidad unidad, Coordenada destino)
        {
            this.unidad = unidad;
            this.destino = destino;
        }
        /// <summary>
        /// Ejecuta el movimiento
        /// </summary>
        /// <returns>true si movió, false si no</returns>
        public bool Ejecutar()
        {
            try
            {
                unidad.Mover(destino);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
