using Spectre.Console;
using static GenerarTablero;
public class TableroDrawer
{
    // Método para dibujar el tablero en la consola usando Spectre.Console
    public static void DibujarTablero(Casilla[,] tablero, List<Ficha> fichasSeleccionadas)
    {
        // Limpiar toda la consola antes de redibujar
        Console.Clear();

        // Crear un nuevo canvas para dibujar el tablero
        var canvas = new Canvas(Program.Ancho, Program.Alto);

        // Dibujar casillas del tablero según su tipo
        for (int fila = 0; fila < tablero.GetLength(0); fila++)
        {
            for (int columna = 0; columna < tablero.GetLength(1); columna++)
            {
                Color color = tablero[fila, columna] switch
                {
                    Casilla.Camino => Color.Black,
                    Casilla.Obstaculo => Color.Grey,
                    Casilla.Trampa => Color.Red,
                    Casilla.Objeto => Color.Wheat1, // Los objetos se dibujan en color verde
                    _=> Color.Grey // Las trampas se dibujan en color rojo
                   
                };

                canvas.SetPixel(columna, fila, color); // Establecer el color del píxel en el canvas
            }
        }

        // Dibujar las fichas seleccionadas sobre el canvas
        foreach (var ficha in fichasSeleccionadas)
        {
            canvas.SetPixel(ficha.Columna, ficha.Fila, ficha.Color);
        }

        // Mostrar el canvas en la consola
        AnsiConsole.Write(canvas);

        // Opcional: mostrar mensajes adicionales
        Console.WriteLine("Tablero actualizado.");
    }
}