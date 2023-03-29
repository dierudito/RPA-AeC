using Newtonsoft.Json.Converters;

namespace Moreno.RPA_AeC.Infra.CrossCutting.Filters.Extensions;

public class DateFormatConverter : IsoDateTimeConverter
{
    public DateFormatConverter(string format)
    {
        DateTimeFormat = format;
    }
}
