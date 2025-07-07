using System;
using System.Collections.Generic;

namespace Library
{
    
}
/// <summary>
/// Esta clase genera un mapa con terreno y unidades 
/// </summary>
public class GenerarMapa 
{
    private char[,] mapa;
    private Dictionary<(int x, int y), char> unidades; 
    private Dictionary<char, string> terrenos; 
    private Dictionary<char, string> simbolosUnidades; 
    
    public int Ancho { get; private set; } 
    public int Alto { get; private set; } 

    /// <summary>
    /// Constructor que inicializa el mapa con medidas específicas
    /// </summary>
    /// <param name="ancho"></param>
    /// <param name="alto"></param>
    public GenerarMapa(int ancho = 20, int alto = 15) 
    {
        Ancho = ancho;
        Alto = alto;
        mapa = new char[alto, ancho];
        unidades = new Dictionary<(int x, int y), char>();

        InicializarDiccionarios();
        InicializarMapa();
    }

    /// <summary>
    /// Inicializa diccionarios con terrenos y unidades
    /// </summary>
    private void InicializarDiccionarios() 
    {
        terrenos = new Dictionary<char, string>
        {
            { '.', "Tierra" },
            { '~', "Agua" },
            { '^', "Mountain" },
            { 'T', "Tree" },
            { '#', "Muro" },
            { ' ', "Empty" }
        };

        simbolosUnidades = new Dictionary<char, string>() //tipo de unidades y simbolos
        {
            { 'P', "Player" },
            { 'E', "Enemigo" },
            { 'A', "Aliado" },
            { 'B', "Edificio" }
        };
    }
    
    /// <summary>
    /// Devuelve el mapa como un string listo para mostrar.
    /// Incluye terreno y unidades.
    /// </summary>
    public string MostrarMapa()
    {
        string resultado = "";

        for (int y = 0; y < Alto; y++)
        {
            for (int x = 0; x < Ancho; x++)
            {
                if (unidades.ContainsKey((x, y)))
                {
                    resultado += unidades[(x, y)];
                }
                else
                {
                    resultado += mapa[y, x];
                }
            }
            resultado += "\n";
        }

        return resultado;
    }

    
    /// <summary>
    /// Inicia el mapa con terrenos
    /// </summary>
    private void InicializarMapa() 
    {
        for (int y = 0; y < Alto; y++)
        {
            for (int x = 0; x < Ancho; x++)
            {
                // patron basico
                if (y == 0 || y == Alto - 1 || x == 0 || x == Ancho - 1)
                {
                    mapa[y, x] = '#'; // muros en los bordes
                }
                else if (y < 3)
                {
                    mapa[y, x] = '^'; // montañas
                }
                else if (y > Alto - 4)
                {
                    mapa[y, x] = '~'; // agua
                }
                else if (x % 4 == 0 && y % 3 == 0)
                {
                    mapa[y, x] = 'T'; //arboles 
                }
                else
                {
                    mapa[y, x] = '.'; // tierra
                }
                    
            }
        }
    }
//public bool ColocarUnidad(int x, int y, char tipoUnidad)

}
