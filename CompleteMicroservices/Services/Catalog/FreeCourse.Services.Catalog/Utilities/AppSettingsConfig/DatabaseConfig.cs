namespace FreeCourse.Services.Catalog.Utilities.AppSettingsConfig
{
    public class DatabaseConfig : IDatabaseConfig
    {
        // public AppsettingsDatabase Database { get; set; }
        public string Name { get; set; }
        public AppsettingsCollection Collection { get; set; }
        public string ConnectionString { get; set; }
    }
}
