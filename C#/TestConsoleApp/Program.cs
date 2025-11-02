using System;
using System.Globalization;

class Ejercicio1
{
    // Constantes para los factores de conversión
    private const float FactorDolar = 0.30f;
    private const float FactorSol = 2.45f;

    /// <summary>
    /// Redondea un valor de dinero a dos decimales.
    /// </summary>
    private static float FormatearDinero(float monto)
    {
        return (float)Math.Round(monto, 2, MidpointRounding.AwayFromZero);
    }

    /// <summary>
    /// Convierte la moneda de acuerdo al tipo solicitado.
    /// </summary>
    private static (string Simbolo, float Factor) ObtenerFactorConversion(string moneda)
    {
        switch (moneda)
        {
            case "S/.":
                return (moneda, FactorSol);
            case "$":
                return (moneda, FactorDolar);
            default:
                // Valor por defecto: dólar
                return ("$", FactorDolar);
        }
    }

    /// <summary>
    /// Imprime la cantidad formateada en consola con su símbolo.
    /// </summary>
    private static void MostrarDinero(string nombre, string moneda, float monto)
    {
        string montoFormateado = FormatearDinero(monto).ToString("F2", CultureInfo.InvariantCulture);
        Console.WriteLine($"{nombre} tiene {moneda}{montoFormateado}");
    }

    public static void Main()
    {
        const string nombre = "Franco";
        const string monedaBase = "$";
        float dinero = 1500.25f;

        // Mostrar dinero original
        MostrarDinero(nombre, monedaBase, dinero);

        // Conversión a soles
        string monedaObjetivo = "S/.";
        (string simbolo, float factor) = ObtenerFactorConversion(monedaObjetivo);

        float dineroConvertido = dinero * factor;
        Console.Write("Equivalente en soles: ");
        MostrarDinero(nombre, simbolo, dineroConvertido);
    }
}
