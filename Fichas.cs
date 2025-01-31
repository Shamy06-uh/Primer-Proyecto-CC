using Casilla = GenerarTablero.Casilla;

public class Ficha
{
    public string Nombre { get; set; }  // Nombre de la ficha
    public int Fila { get; set; }       // Posición de la ficha en la fila
    public int Columna { get; set; }    // Posición de la ficha en la columna
    public ConsoleColor Color { get; set; }  // Color de la ficha
    public string Habilidad { get; set; }    // Habilidad especial de la ficha
    public int Cooldown { get;  set; }  // Turnos restantes para reutilizar la habilidad
    public int Puntos { get;private set; }    // Puntos actuales de la ficha
    public int TurnosInmunidad { get;  set; } = 0;  // Turnos restantes de inmunidad
    public int turnosPasoFantasma;  // Contador para la habilidad Paso Fantasma

    public int ObjetosRecogidos { get; private set; }  // Nueva propiedad para contar objetos recogidos

    // Constructor para inicializar una ficha
    public Ficha(string nombre, int fila, int columna, ConsoleColor color, string habilidad, int cooldown = 0, int puntosIniciales = 5)
    {
        Nombre = nombre;
        Fila = fila;
        Columna = columna;
        Color = color;
        Habilidad = habilidad;

        Cooldown = cooldown;
        Puntos = puntosIniciales;
        ObjetosRecogidos = 0; // Inicializar en 0
    }
    public void RecogerObjeto()
    {
    ObjetosRecogidos++;
    Console.WriteLine($"{Nombre} ha recogido un objeto! Total objetos recogidos: {ObjetosRecogidos}");
    //Condicion de ganar
    if (ObjetosRecogidos >= 3)
    {
        Console.WriteLine($"{Nombre} ha ganado al recoger 3 objetos!");
        // Aquí puedes agregar la lógica para terminar el juego
        Environment.Exit(0); // Termina el programa
    }

    }

    // Mueve la ficha a una nueva posición, permitiendo atravesar obstáculos con "Paso Fantasma"
  public bool Mover(int nuevaFila, int nuevaColumna, Casilla[,] tablero)
{
    // Verificar si la casilla es un obstáculo
    Casilla casillaDestino = tablero[nuevaFila, nuevaColumna];

    // Si la ficha tiene la habilidad "Paso Fantasma", podrá atravesar obstáculos
    if (turnosPasoFantasma > 0 && casillaDestino == Casilla.Obstaculo)
    {
        // Verificar si la casilla no está en el borde
        bool esBorde = nuevaFila == 0 || nuevaFila == tablero.GetLength(0) - 1 || nuevaColumna == 0 || nuevaColumna == tablero.GetLength(1) - 1;

        if (!esBorde) 
        {
            Console.WriteLine($"{Nombre} atraviesa un obstáculo debido a la habilidad Paso Fantasma.");
            Fila = nuevaFila;
            Columna = nuevaColumna;
            return true;
        }
        else
        {
            Console.WriteLine($"{Nombre} no puede atravesar un obstáculo en el borde.");
            return false;
        }
    }
    // Si no es obstáculo, moverse normalmente
    else if (casillaDestino != Casilla.Obstaculo) 
    {
        Fila = nuevaFila;
        Columna = nuevaColumna;
        return true;
    }
    else
    {
        Console.WriteLine($"{Nombre} no puede moverse a una casilla con obstáculo.");
        return false; // Movimiento no permitido
    }
}


    public void FinalizarTurno(Ficha ficha)
{
    if (ficha.Cooldown > 0)
    {
        ficha.Cooldown--;
    }

    if (ficha.TurnosInmunidad > 0)
    {
        ficha.TurnosInmunidad--;
        if (ficha.TurnosInmunidad == 0)
        {
            Console.WriteLine($"{ficha.Nombre} ya no tiene inmunidad temporal.");
        }
    }

    // Otras lógicas de finalización de turno pueden ir aquí
}



   
  public bool PerderPuntos(int puntos)
{
    if (TurnosInmunidad > 0)
    {
        Console.WriteLine($"{Nombre} es inmune a la pérdida de puntos este turno.");
    }
    else
    {
        Puntos -= puntos;
        if (Puntos < 0) Puntos = 0; // Asegurarse de que los puntos no sean negativos
        MostrarVida();

        if (Puntos == 0)
        {
            // Notificación cuando la ficha pierde todos sus puntos
            Console.WriteLine($"¡{Nombre} ha perdido todos sus puntos! El juego ha terminado.");
            Thread.Sleep(3000); // Pausa de 3 segundos
            Environment.Exit(0); // Termina el programa
    

        

            return true; // Indica que el juego debe detenerse
        }
    }

    return false; // El juego continúa si no se ha perdido
}


    public void MostrarVida()
    {
        int porcentajeVida = Puntos * 10; // Escala a porcentaje (10 puntos = 100%)
        Console.WriteLine($"[{Nombre}] Vida actual: {porcentajeVida}%");
    }

        public void AumentarCooldown(int turnos)
    {
        Cooldown += turnos;
    }

    // Finaliza el turno de la ficha, gestionando la inmunidad, cooldown y Paso Fantasma
    public void FinalizarTurno()
    {
        if (TurnosInmunidad > 0)
        {
            TurnosInmunidad--;  // Reducir el contador de inmunidad
            if (TurnosInmunidad == 0)
            {
                Console.WriteLine($"{Nombre} ya no es inmune a la pérdida de puntos.");
            }
        }

        if (Cooldown > 0)
        {
            Cooldown--;  // Reducir el contador de cooldown de habilidad
        }

        // Decrementar los turnos de la habilidad Paso Fantasma si está activa
        if (turnosPasoFantasma > 0)
        {
            turnosPasoFantasma--;  // Reducir el contador de turnos de Paso Fantasma
            if (turnosPasoFantasma == 0)
            {
                Console.WriteLine($"{Nombre} ya no puede atravesar obstáculos.");
            }
        }
    }

    // Usa la habilidad especial de la ficha, si no está en cooldown
    public void UsarHabilidad(Casilla[,] tablero, Random random, List<Ficha> fichasSeleccionadas)
    {
        if (Cooldown > 0)
        {
            Console.WriteLine($"La habilidad {Habilidad} está en enfriamiento por {Cooldown} turnos más.");
            return;
        }

        switch (Habilidad)
        {
            case "Teletransportación Aleatoria":
                TeletransportarAleatoriamente(tablero, random, fichasSeleccionadas);
                 Cooldown = 4;
                break;
            case "Inmunidad Temporal":
                InmunidadTemporal();
                 Cooldown = 3;
                break;
            case "Paso Fantasma":
    if (turnosPasoFantasma <= 0)
    {
        turnosPasoFantasma = 3; // Establecer los turnos de Paso Fantasma
        Console.WriteLine($"{Nombre} ha activado 'Paso Fantasma' y podrá atravesar obstáculos durante {turnosPasoFantasma} turnos.");
    }
    else
    {
        Console.WriteLine($"{Nombre} ya tiene activa la habilidad 'Paso Fantasma'.");
    }
    Cooldown = 3; // Establecer el tiempo de enfriamiento para la habilidad
    break;

            
    
            case "Curación Rápida":
                Curar();
                 Cooldown = 3;
                 turnosPasoFantasma = 3;
                break;
            case "Avance Triple":
                AvanceTriple(tablero, fichasSeleccionadas);
                 Cooldown = 3;
                break;
            default:
                Console.WriteLine("Habilidad no reconocida.");
                break;
        }
    }

    // Teletransporta la ficha a una posición aleatoria en el tablero
    private void TeletransportarAleatoriamente(Casilla[,] tablero, Random random, List<Ficha> fichasSeleccionadas)
    {
        int fila, columna;

        do
        {
            fila = random.Next(tablero.GetLength(0));
            columna = random.Next(tablero.GetLength(1));
        } while (tablero[fila, columna] != Casilla.Camino);  // Busca una casilla válida
        Mover(fila, columna,tablero);
        Console.WriteLine($"{Nombre} se teletransportó a la posición ({fila}, {columna}).");
        Thread.Sleep(3000); // Pausa de 3 segundos
        TableroDrawer.DibujarTablero(tablero, fichasSeleccionadas);
    }

    // Activa la inmunidad temporal para evitar pérdida de puntos
    private void InmunidadTemporal()
    {
        if (TurnosInmunidad == 0)  // Si la inmunidad no está activa
        {
            TurnosInmunidad = 6;  // Activar inmunidad por 6 turnos
            Console.WriteLine($"{Nombre} ha activado Inmunidad Temporal por 3 turnos.");
            Thread.Sleep(3000); // Pausa de 3 segundos
        }
        else
        {
            Console.WriteLine($"{Nombre} ya tiene Inmunidad Temporal activa.");
        }
    }



    // Permite a la ficha atravesar obstáculos durante 2 turnos// Método para comprobar si la ficha puede atravesar un obstáculo
  public bool PasoFantasma(Casilla casilla, int fila, int columna, int totalFilas, int totalColumnas)
{
    // Verificar si la casilla está en el borde del tablero
    bool esBorde = fila == 0 || fila == totalFilas - 1 || columna == 0 || columna == totalColumnas - 1;

    if (esBorde)
    {
        Console.WriteLine($"{Nombre} está intentando atravesar un obstáculo en el borde del tablero.");
        Thread.Sleep(3000); 
        return false;
    }

    // Permitir atravesar obstáculos si no están en los bordes y la habilidad está activa
    return turnosPasoFantasma > 0 && casilla == Casilla.Obstaculo;
}







    // Incrementa los puntos de la ficha, hasta un máximo de 4
    private void Curar()
    {
        Puntos += 2;
        if (Puntos > 4) Puntos = 4;  // Asegura que los puntos no excedan 4
        Console.WriteLine($"{Nombre} se ha curado. Puntos actuales: {Puntos}");
        Thread.Sleep(3000); // Pausa de 3 segundos
    }

    // Permite a la ficha moverse dos veces en un turno
    public void AvanceTriple(Casilla[,] tablero, List<Ficha> fichasSeleccionadas)
    {
        Console.WriteLine($"{Nombre} Puede moverse 2 veces seguidas y luego otravez al presionar `Mover Ficha`");
        Thread.Sleep(3000); // Pausa de 3 segundos

        for (int i = 0; i < 2; i++)
        {
            Console.WriteLine($"Movimiento {i + 1} de 2: Usa las teclas de dirección para mover la ficha.");
            ConsoleKey teclaPresionada = Console.ReadKey(true).Key;

            int nuevaFila = Fila;
            int nuevaColumna = Columna;

            switch (teclaPresionada)
            {
                case ConsoleKey.UpArrow: nuevaFila--; break;
                case ConsoleKey.DownArrow: nuevaFila++; break;
                case ConsoleKey.LeftArrow: nuevaColumna--; break;
                case ConsoleKey.RightArrow: nuevaColumna++; break;
                default:
                    Console.WriteLine("Tecla inválida. Usa las flechas de dirección.");
                    i--;  // Repetir el movimiento si la tecla es inválida
                    continue;
            }

            if (IsMovimientoValido(nuevaFila, nuevaColumna, tablero))
            {
                Mover(nuevaFila, nuevaColumna,tablero);
                Console.WriteLine($"{Nombre} se ha movido a la posición ({nuevaFila}, {nuevaColumna}).");
                TableroDrawer.DibujarTablero(tablero, fichasSeleccionadas);
            }
            else
            {
                Console.WriteLine("Movimiento no permitido. Intenta nuevamente.");
                i--;  // Repetir el movimiento si es inválido
            }
        }
    }

    // Verifica si un movimiento es válido, es decir, no fuera de los límites ni hacia un obstáculo
    public static bool IsMovimientoValido(int nuevaFila, int nuevaColumna, Casilla[,] tablero)
    {
        if (nuevaFila < 0 || nuevaFila >= tablero.GetLength(0) || nuevaColumna < 0 || nuevaColumna >= tablero.GetLength(1))
        {
            return false;  // Movimiento fuera de los límites del tablero
        }

        return tablero[nuevaFila, nuevaColumna] != Casilla.Obstaculo;  // Verifica que la casilla no sea un obstáculo
    }
}