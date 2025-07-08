using System;
using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Ucu.Poo.DiscordBot.Commands
{
    /// <summary>
    /// Comando de Discord para mover unidades
    /// </summary>
    public class MoverUnidadCommand : ModuleBase<SocketCommandContext>
    {
        /// <summary>
        /// Comando para mover una unidad
        /// </summary>
        [Command("mover")]
        [Summary("Mueve una unidad a una nueva posición")]
        public async Task MoverAsync(int idUnidad, int x, int y)
        {
            try
            {
                string nombreJugador = Context.User.Username;
                
                if (Facade.Instance.Jugador1 == null || Facade.Instance.Jugador2 == null)
                {
                    await ReplyAsync("No hay una partida activa.");
                    return;
                }
                
                var jugador = Facade.Instance.GetJugadorPorNombre(nombreJugador);
                if (jugador == null)
                {
                    await ReplyAsync("No estás participando en la partida actual.");
                    return;
                }
                
                if (!Facade.Instance.TieneTurno(nombreJugador))
                {
                    string jugadorTurno = Facade.Instance.ObtenerJugadorTurno();
                    await ReplyAsync($"No es tu turno. Es el turno de {jugadorTurno}.");
                    return;
                }
                
                var unidad = Facade.Instance.ObtenerUnidadPorId(idUnidad, nombreJugador);
                if (unidad == null)
                {
                    await ReplyAsync($"No se encontró una unidad con ID {idUnidad}.");
                    return;
                }
                
                var destino = new Coordenada(x, y);
                
                bool exito = Facade.Instance.MoverUnidad(unidad, destino);
                
                if (exito)
                {
                    await ReplyAsync($"Unidad {idUnidad} movida a ({x}, {y})");
                    
                }
                else
                {
                    await ReplyAsync($"No se pudo mover la unidad {idUnidad} a ({x}, {y}).");
                }
            }
            catch (Exception ex)
            {
                await ReplyAsync($"Error al mover la unidad: {ex.Message}");
            }
        }
    }
}