using Discord.Commands;
using Library;

namespace Ucu.Poo.DiscordBot.Commands
{
    public class RecolectarCommand : ModuleBase<SocketCommandContext>
    {
        [Command("recolectar")]
        [Summary("Permite recolectar un recurso en una coordenada. Sintaxis: !recolectar [tipo] [x] [y]")]
        public async Task Recolectar(string tipo, int x, int y)
        {
            // Buscar el tipo de recurso usando las instancias estáticas
            TipoRecurso? tipoRecurso = ObtenerTipoRecurso(tipo);

            var jugador = Facade.Instance.GetJugadorPorNombre(Context.User.Username);

            if (!Facade.Instance.TieneTurno(jugador.Nombre))
            {
                await ReplyAsync("No es tu turno.");
                return;
            }

            var coordenada = new Coordenada(x, y);

            try
            {
                Facade.Instance.Recolectar(jugador, tipoRecurso, coordenada);
                await ReplyAsync($"{jugador.Nombre} recolectó {tipoRecurso.Nombre} en ({x}, {y}).");
            }
            catch (Exception ex)
            {
                await ReplyAsync($"Error: {ex.Message}");
            }
        }

        // Método auxiliar para convertir el string a una de las instancias estáticas de TipoRecurso
        private TipoRecurso? ObtenerTipoRecurso(string tipo)
        {
            tipo = tipo.ToLower();
            return tipo switch
            {
                "alimento" => TipoRecurso.Alimento,
                "madera" => TipoRecurso.Madera,
                "piedra" => TipoRecurso.Piedra,
                "oro" => TipoRecurso.Oro,
                _ => null
            };
        }
    }
}
