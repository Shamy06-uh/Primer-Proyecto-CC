public class Trampa(Trampa.Tipo tipo)
{
    public enum Tipo
    {
        Quita1Punto,
        Quita2Puntos,
        AumentarCooldown // Otro tipo de trampa
    }

    public  Tipo TipoDeTrampa { get; set; } = tipo;

    public void AplicarTrampa(Ficha ficha)
    {
        switch (TipoDeTrampa)
        {
            case Tipo.Quita1Punto:
            ficha.PerderPuntos(1); // Llama a PerderPuntos para restar 1 punto
            break;
            case Tipo.Quita2Puntos:
            ficha.PerderPuntos(2); // Llama a PerderPuntos para restar 2 puntos
            break;
            case Tipo.AumentarCooldown:
            Console.WriteLine($"Trampa AumentarCooldown activada en {ficha.Nombre}. Cooldown antes: {ficha.Cooldown}");
            ficha.AumentarCooldown(5);
            Console.WriteLine($"Cooldown después: {ficha.Cooldown}");
           break;

        }
    }
        
}

