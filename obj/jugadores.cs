using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto_de_prueba.obj
{
    public class jugadores
    {
        
    public string Nombre { get; set; }
    public int PosX { get; set; }
    public int PosY { get; set; }

    public Jugador(string nombre, int x, int y)
    {
        Nombre = ;
        PosX = x;
        PosY = y;
    }
    public jugadores(string nombre, int x, int y)
    {
        Nombre = nombre ;
        
    }

    public void Mover(int nuevoX, int nuevoY)
    {
        PosX = nuevoX;
        PosY = nuevoY;
    }
}
    
}
