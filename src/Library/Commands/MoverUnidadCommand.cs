using Library;

namespace Ucu.Poo.DiscordBot.Commands
{
    public class MoverUnidadCommand
    {
        private readonly Unidad unidad;
        private readonly Coordenada destino;

        public MoverUnidadCommand(Unidad unidad, Coordenada destino)
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
}