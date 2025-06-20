using System;
using System.Collections.Generic;

namespace Library
{
    
}
public class GenerarMapa //genera el mapa dependiendo del terreno y unidades
{
    private char[,] mapa; //matriz que genera el mapa
    private Dictionary<(int x, int y), char> unidades; //diccionario que contiene unidades y posiciones 
    private Dictionary<char, string> terrenos; //diccionario que contiene los terrenos
    private Dictionary<char, string> simbolosUnidades; //diccionario que contiene los simbolos de unidades con sus tipos
    
    public int Ancho { get; private set; } //ancho del mapa
    public int Alto { get; private set; } //alto del mapa

    public GenerarMapa(int ancho = 20, int alto = 15) //constructor inicializando un mapa especifico
    {
        Ancho = ancho;
        Alto = alto;
        mapa = new char[alto, ancho];
        unidades = new Dictionary<(int x, int y), char>();

        InicializarDiccionarios();
        InicializarMapa();
    }

    private void InicializarDiccionarios() //iniciliza diccionarios con terrenos y unidades
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
    
    private void InicializarMapa() //inicia el mapa con terrenos
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
