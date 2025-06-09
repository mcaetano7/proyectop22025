using System;
using System.Collections.Generic;

namespace Library
{
    
}
public class GenerarMapa
{
    private char[,] mapa;
    private Dictionary<(int x, int y), char> unidades;
    private Dictionary<char, string> terrenos;
    private Dictionary<char, string> simbolosUnidades;
    
    public int Ancho { get; private set; }
    public int Alto { get; private set; }

    public GenerarMapa(int ancho = 20, int alto = 15)
    {
        Ancho = ancho;
        Alto = alto;
        mapa = new char[alto, ancho];
        unidades = new Dictionary<(int x, int y), char>();

        InicializarDiccionarios();
        InicializarMapa();
    }

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

        simbolosUnidades = new Dictionary<char, string>()
        {
            { 'P', "Player" },
            { 'E', "Enemigo" },
            { 'A', "Aliado" },
            { 'B', "Edificio" }
        };
    }
    
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
                    mapa[y, x] = '^'; // monta;as
                }
                else if (y > Alto - 4)
                {
                    mapa[y, x] = '~'; // agua
                }
                else if (x % 4 == 0 && y % 3 == 0)
                {
                    mapa[y, x] = 'T'; //arboles por ahgi
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
