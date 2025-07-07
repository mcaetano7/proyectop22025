using Discord.Commands;
using Ucu.Poo.DiscordBot.Domain;
using Library;
namespace Ucu.Poo.DiscordBot.Commands

//TENEMOS FACADE EN EL BOT Y EN EL JUEGO POR ESO MARCA ERROR

/// <summary>
/// este comando permite construir un edificio en el mapa, en la ubicación que el usuario indique.
/// </summary>
public class ConstruirCommand : ModuleBase<SocketCommandContext>
{
    /// <summary>
    /// busca al jugador por su nombre (username).
    /// </summary>
    /// <param name="nombre">el nombre del jugador que buscamos.</param>
    /// <returns>el jugador si lo encuentra, si no null.</returns>
    private Player GetJugadorPorNombre(string nombre)
    {
        return Facade.Instance.GetJugadorPorNombre(nombre);
    }

    /// <summary>
    /// comando para construir un edificio.  
    /// el usuario pone el tipo de edificio y las coordenadas x e y.  
    /// ejemplo: !construir almacen 5 6  
    /// </summary>
    /// <param name="tipoEdificio">el nombre del edificio que querés construir.</param>
    /// <param name="x">eoordenada X en el mapa.</param>
    /// <param name="y">eoordenada Y en el mapa.</param>
    [Command("construir")]
    [Summary("Construye un edificio en la ubicación indicada. Ej: !construir almacen 5 6")]
    public async Task ConstruirAsync(string tipoEdificio, int x, int y)
    {
        // primero buscamos al jugador que hizo el comando
        var jugador = GetJugadorPorNombre(Context.User.Username);

        // si no lo encontramos, le decimos que no está en el juego
        if (jugador == null)
        {
            await ReplyAsync("No se encontró tu jugador en el juego.");
            return;
        }

        // armamos la coordenada con lo que puso el usuario
        var ubicacion = new Coordenada(x, y);

        // según el tipo de edificio, creamos el objeto correspondiente
        Edificio edificio = tipoEdificio.ToLower() switch
        {
            "almacen" => new Almacen(ubicacion, 100, jugador, TipoRecurso.Madera, 1000),
            "centrocivico" => new CentroCivico(ubicacion, 150, jugador, 10),
            "casa" => new Casa(ubicacion, 50, jugador, "Casa", 5),
            _ => null
        };

        // si el tipo que puso no es válido, avisamos
        if (edificio == null)
        {
            await ReplyAsync($"El tipo de edificio '{tipoEdificio}' no es válido.");
            return;
        }

        // chequeamos que el jugador tenga los recursos para construirlo
        if (!jugador.TieneRecursos(edificio.ObtenerCosto()))
        {
            await ReplyAsync("No tienes los recursos suficientes para construir ese edificio.");
            return;
        }

        // mandamos a construir el edificio (se encarga el Facade)
        Facade.Instance.Construir(jugador, edificio, ubicacion);

        // confirmamos que se construyó y mostramos los recursos que quedan
        await ReplyAsync($"¡{tipoEdificio} construido en ({x},{y})! Recursos restantes: Madera = {jugador.GetRecurso(TipoRecurso.Madera)}");
    }
}

