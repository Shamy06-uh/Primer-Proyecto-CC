using Spectre.Console;
using System.Collections.Generic;

public class Informacion
{
    // Tabla para mostrar la información de las fichas
    private static Table tablaInformacion = new Table();

    // Inicializa la tabla con las columnas necesarias
    public static void Inicializar()
    {
        tablaInformacion = new Table()
            .Border(TableBorder.Rounded)
            .Title("Información de Fichas")
            .AddColumn("Nombre")
            .AddColumn("Vida (%)") // Cambiar el encabezado a "Vida (%)"
            .AddColumn("Habilidad")
            .AddColumn("Cooldown");
    }

    // Muestra la información actualizada de las fichas en la consola
    public static void MostrarInformacion(List<Ficha> fichas)
    {
        // Limpia las filas anteriores antes de agregar nueva información
        tablaInformacion.Rows.Clear();

        foreach (var ficha in fichas)
        {
            // Calcula el porcentaje de vida basado en los puntos actuales
            int porcentajeVida = ficha.Puntos * 10; // Suponiendo que 10 puntos = 100%

            // Agrega una nueva fila con la información de cada ficha
            tablaInformacion.AddRow(
                ficha.Nombre,
                $"{porcentajeVida}%", // Mostrar la vida como porcentaje
                ficha.Habilidad,
                ficha.Cooldown > 0 ? ficha.Cooldown.ToString() : "Listo");
        }

        // Refresca la tabla para mostrar la información actualizada en la consola
        AnsiConsole.Live(tablaInformacion).Start(ctx =>
        {
            ctx.Refresh();
        });
    }
}
