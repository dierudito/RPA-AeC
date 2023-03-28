namespace Moreno.RPA_AeC.Infra.CrossCutting.IoC.Configurations;

[ExcludeFromCodeCoverage]
public static class ConnStrConfig
{
    private class ConnectionString
    {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    private const string DEFAULT_CONN_STR_NAME = "Default";

    public static Dictionary<string, string> AppConnections
    {
        get
        {
            var list = ConnectionStrings.Value.FromJsonTo<List<ConnectionString>>();

            var dict = list.ToDictionary(
                x => x.Name,
                x => x.Value
            );

            return dict;
        }
    }

    public static string DefaultConnectionString => AppConnections[DEFAULT_CONN_STR_NAME];
}
