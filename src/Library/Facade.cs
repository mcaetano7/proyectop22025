namespace Library;

/* HISTORIAS DE USUARIO:
 
 1. poder crear una nueva partida especificando el tamaño del mapa y cantidad de jugadores.
 2. elegir civilización (aprovechar sus ventajas estratégicas)
 3. comenzar con un centro cívico y algunos aldeanos para iniciar la recolecci{on de recursos.
 4. ordenar a los aldeanos recolectar diferentes tipos de recursos.
 5. construir edificios para almacenar recursos.
 6. quiero visualizar la cantidad de recursos disponibles.
 7. construir edificios en ubis específicas para expandir la base.
 8. crear diferentes tipos de edificios con funciones específicas para desarrollar la civilización.
 9. entrrenar unidades militares para defender la base y atacar oponentes.
 10. mover las unidades ppor el mapa usando comandos simples.
 11. ordenar a las unidades atacar edeficios o unidades enemigas. 
 12. entrenar aldeanos parta mejorar la econom{ia y tener suficientes casas para mantener la población.
 13. destruir los centros cívicos para ganar la partida por dominación militar.
 14. usar comandos intuitivos para interactuar con el juego 
 15. quiero ver un mapa simplificado del juego en ASCII para visualizar la dispo del terreno y unidades. 
 16. como jugador quiero gardar la partida y continuarla más tarde. 
 */

public class Facade
{
    
    public Mapa Mapa { get; private set; }
    public Player Jugador1 { get; private set; }
    public Player Jugador2 { get; private set; }
    public void CrearPartida(Civilizacion civ1, Civilizacion civ2) //el user elige la civ desde program
    {
        Mapa = new Mapa(); 
        Jugador1 = new Player("Jugador 1", civ1); 
        Jugador2 = new Player("Jugador 2", civ2);
        
    }

    public void GuardarPartida()
    {
        
    }

    public void CargarPartida()
    {
        
    }
    //cargar partida, guardar partida, crear partida 
}