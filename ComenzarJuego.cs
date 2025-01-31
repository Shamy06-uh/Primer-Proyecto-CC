using Spectre.Console;

public class StartGame
{
    public static void MostrarPresentacion()
    {
        // Título del juego con estilo
        var title = new FigletText("WELCOME TO MY GAME")
            .LeftJustified()
            .Color(Color.Cyan1);
        
        AnsiConsole.Write(title);
        
        // Mensaje inicial
        AnsiConsole.MarkupLine("[bold yellow]El primer jugador en alcanzar 3 Objetos amarillos gana la partida.[/]");
        AnsiConsole.MarkupLine("[bold red]Evite las trampas (Casillas Rojas) así evitará perder vida o que aumente el cooldown de su habilidad.[/]");
        AnsiConsole.MarkupLine("[green]¡Disfruta del juego![/]");
        
        // Opciones interactivas
        var menu = new SelectionPrompt<string>()
            .Title("¿Qué deseas hacer?")
            .AddChoices(new[] { "Jugar", "Instrucciones", "Salir" });

        var opcion = AnsiConsole.Prompt(menu);

        switch (opcion)
        {
            case "Jugar":
                // Lógica para iniciar el juego
                break; // Aquí puedes dejarlo vacío, ya que se iniciará en el Main
            case "Instrucciones":
                MostrarInstrucciones();
                break;
            case "Salir":
                Environment.Exit(0);
                break;
        }
    }

    private static void MostrarInstrucciones()
    {
        // Mostrar instrucciones aquí
        AnsiConsole.MarkupLine("[bold blue]Instrucciones del juego:[/]");
        AnsiConsole.MarkupLine("- Presiona ENTER y usa las teclas de dirección para moverte.");
        AnsiConsole.MarkupLine("- Recoge objetos para ganar.");
        AnsiConsole.MarkupLine("- Evita los obstáculos y trampas.");
        AnsiConsole.MarkupLine("Presiona cualquier tecla para volver al menú.");
        
        Console.ReadKey();
    }
}
