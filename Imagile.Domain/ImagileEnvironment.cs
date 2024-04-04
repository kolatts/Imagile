using System.ComponentModel;

namespace Imagile.Domain
{
    public static class ImagileEnvironment
    {
        public enum Types
        {
            Local,
            [Description("Quality Assurance")]
            QA,
            [Description("Production")]
            Prod
        }


        public static Types Get(string? hostEnvironment = null)
        {
            hostEnvironment ??= Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return Enum.GetValues<Types>()
                .FirstOrDefault(x => string.Equals(x.ToString(), hostEnvironment, StringComparison.OrdinalIgnoreCase));
        }

        private static string ToEnvironmentName(this Types type) => type.ToString().ToLower();

        public static string ToStorageAccountName(this Types type) =>
            $"imagile{type.ToEnvironmentName()}storage";

        public static string ToSqlServerName(this Types type)
        {
            return type == Types.Local ? "localhost" : $"imagile-{type.ToEnvironmentName()}-sqlserver";
        }
        public static string ToSharedDatabaseName(this Types type) => $"imagile-{type.ToEnvironmentName()}-shared";
        public static string ToCompanyDatabaseTemplate(this Types type) => $"imagile-{type.ToEnvironmentName()}-company";

        public static string ToCompanyDatabaseName(this Types type, int number) =>
            $"{type.ToCompanyDatabaseTemplate()}-{number:##}";

        public static string ToManagedIdentityName(this Types type) => $"imagile-{type.ToEnvironmentName()}-identity";
    }
}
