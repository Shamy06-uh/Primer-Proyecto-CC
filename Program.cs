using System;
using System.Collections.Generic;   
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;  
using System.Threading.Tasks;

namespace Laberinto
{
    class Program
    {
        static void  Main(string[] args)
        {
            char[,] laberinto = {
                { ' ',' ',' ','#','X','X','X','X','X',' ','X'},
                { 'X','#',' ','#',' ',' ',' ',' ',' ',' ','X'},
                { 'X','X',' ','X','X','X','X','X',' ','X','X'},
                { 'X',' ',' ',' ','#',' ',' ',' ',' ',' ','X'},
                { 'X',' ','X','X','X','X',' ','X',' ','X','X'},
                { 'X',' ','X','X','X','X',' ','X',' ','X','X'},
                { 'X',' ',' ','X','X','X',' ','X',' ','X','X'},
                { 'X','X',' ','X','X','X',' ',' ','#','X','X'},
                { 'X','X','#','X',' ',' ',' ',' ',' ','X','X'},
                { 'X','X',' ',' ',' ','X','X','X',' ',' ',' '}
            };

            Jugador1 spiderman  = new Jugador1("spiderman", 0, 0);
            Jugador2 wolveraine = new Jugador2("wolveraine", 0, 0);
            Trampa trampa = new Trampa(1, 2); // Ejemplo de trampa

            while (true)
            {
                Console.Clear();
                MostrarLaberinto(laberinto, a ,wolveraine,trampa);
                Console.WriteLine("Turno de " + spiderman.Nombre);
                MoverJugador(Mara, laberinto);
                if (trampa.Activar() && spiderman.PosX == trampa.PosX && spiderman.PosY == trampa.PosY)
                {
                    Console.WriteLine("¡" + spiderman.Nombre + " ha caído en una trampa!");
                    break; // Fin del juego si cae en la trampa
                }

                Console.Clear();
                MostrarLaberinto(laberinto,wolveraine);
                Console.WriteLine("Turno de " +wolveraine.Nombre);
                MoverJugador(wolveraine, laberinto);
                if (trampa.Activar() && wolveraine.PosX == trampa.PosX && wolveraine.PosY == trampa.PosY)
                {
                    Console.WriteLine("¡" + wolveraine.Nombre + " ha caído en una trampa!");
                    break; // Fin del juego si cae en la trampa
                }
            }
        }

        static void MostrarLaberinto(char[,] laberinto, Jugador spiderman, Jugador wolveraine)
        {
            for (int i = 0; i < laberinto.GetLength(0); i++)
            {
                for (int j = 0; j < laberinto.GetLength(1); j++)
                {
                    if (i == spiderman.PosX && j == spiderman.PosY)
                        Console.Write("1 ");
                    else if (i == wolveraine.PosX && j == wolveraine.PosY)
                        Console.Write("2 ");
                    else
                        Console.Write(laberinto[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
                static void MoverJugador(Jugador jugador, char[,] laberinto)
        {
            Console.WriteLine("Ingresa movimiento (w/a/s/d): ");
            char movimiento = Console.ReadKey().KeyChar;

            int nuevoX = jugador.PosX;
            int nuevoY = jugador.PosY;

            switch (movimiento)
            {
                case 'w': nuevoX--; break; // Arriba
                case 's': nuevoX++; break; // Abajo
                case 'a': nuevoY--; break; // Izquierda
                case 'd': nuevoY++; break; // Derecha
            }

            // Verificar límites y obstáculos
            if (nuevoX >= 0 && nuevoX < laberinto.GetLength(0) &&
                nuevoY >= 0 && nuevoY < laberinto.GetLength(1) &&
                laberinto[nuevoX, nuevoY] != '#')
            {
                jugador.Mover(nuevoX, nuevoY);
            }
        }
    }
}


