namespace Library;
using Library;


    public class MoverUnidadComando  
    {
        private Unidad unidad;
        private Coordenada destino;
        
        public MoverUnidadComando(Unidad unidad, Coordenada destino)
        {
            this.unidad = unidad;
            this.destino = destino;
        }
        
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
