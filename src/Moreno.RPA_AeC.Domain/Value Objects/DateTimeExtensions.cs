using System.Globalization;

namespace Moreno.RPA_AeC.Domain.Value_Objects;

public static class DateTimeExtensions
{
    private static List<string> FormatosDeData = 
        new() { "dd/MM/yyyy", "MM/dd/yyyy", "M/dd/yyyy", "dd MMMM yyyy", "MMMM dd", "yyyy-MM-dd", "dd MMM yyyy" };

    public static DateTime ConverterStringParaData(this string textoData)
    {
        if (string.IsNullOrWhiteSpace(textoData))
            throw new Exception("O texto da data está vazio");

        var textoSemEspacos = textoData.Trim();

        foreach (var formato in FormatosDeData)
        {
            var (convertido, data) = ConverterParaData(textoSemEspacos, formato);
            if (convertido)
                return data;
        }

        throw new Exception($"Não foi possível converter o texto {textoData} para uma data");
    }

    private static (bool convertido, DateTime data) ConverterParaData(string textoData, string formato)
    {
        var convertido =
            DateTime.TryParseExact(textoData, formato,
            CultureInfo.InvariantCulture, DateTimeStyles.None, out var date);

        return (convertido, date);
    }
}
