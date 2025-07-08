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
        /// Uso: !mover [id_unidad] [x] [y]
        /// </summary>
        [Command("mover")]
        [Summary("Mueve una unidad a una nueva posición")]
        public async Task MoverAsync(int idUnidad, int x, int y)
        {
            try
            {
                string nombreJugador = Context.User.Username;
                
                // Verificar que hay una partida activa
                if (Facade.Instance.Jugador1 == null || Facade.Instance.Jugador2 == null)
                {
                    await ReplyAsync("❌ No hay una partida activa. Crea una partida primero.");
                    return;
                }
                
                // Obtener el jugador
                var jugador = Facade.Instance.GetJugadorPorNombre(nombreJugador);
                if (jugador == null)
                {
                    await ReplyAsync("❌ No estás participando en la partida actual.");
                    return;
                }
                
                // Verificar que es el turno del jugador
                if (!Facade.Instance.TieneTurno(nombreJugador))
                {
                    string jugadorTurno = Facade.Instance.ObtenerJugadorTurno();
                    await ReplyAsync($"❌ No es tu turno. Es el turno de {jugadorTurno}.");
                    return;
                }
                
                // Obtener la unidad por ID
                var unidad = Facade.Instance.ObtenerUnidadPorId(idUnidad, nombreJugador);
                if (unidad == null)
                {
                    await ReplyAsync($"❌ No se encontró una unidad con ID {idUnidad}.");
                    return;
                }
                
                // Crear la coordenada de destino
                var destino = new Coordenada(x, y);
                
                // 🎯 AQUÍ SE USA TU MÉTODO MoverUnidad de Facade
                bool exito = Facade.Instance.MoverUnidad(unidad, destino);
                
                if (exito)
                {
                    await ReplyAsync($"✅ Unidad {idUnidad} movida exitosamente a ({x}, {y})");
                    
                    // Pasar al siguiente turno
                    Facade.Instance.SiguienteTurno();
                    await ReplyAsync($"🔄 Turno pasado a {Facade.Instance.ObtenerJugadorTurno()}");
                }
                else
                {
                    await ReplyAsync($"❌ No se pudo mover la unidad {idUnidad} a ({x}, {y}). Verifica que el movimiento sea válido.");
                }
            }
            catch (Exception ex)
            {
                await ReplyAsync($"❌ Error al mover la unidad: {ex.Message}");
            }
        }
    }
}