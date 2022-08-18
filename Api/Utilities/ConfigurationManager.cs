//Provides a method in which to pass configuration specified within appsettings.json to classes (especially static classes).
public static class ConfigurationManager
{
    public static IConfiguration AppSetting { get; }

    static ConfigurationManager()
    {
        AppSetting = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
    }
}