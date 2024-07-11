namespace Imagile.Domain.Hosting;

public static class ImagileCloudResource
{
    public enum Types
    {
        SqlDatabase,
        StorageAccount,
        ManagedIdentity,
        AzureFunction,
    }

    public enum Ids
    {
        SharedDatabase,
        CompanyDatabase

    }
}