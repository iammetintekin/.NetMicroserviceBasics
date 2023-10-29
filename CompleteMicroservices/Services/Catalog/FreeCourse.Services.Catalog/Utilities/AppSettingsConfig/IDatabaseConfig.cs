namespace FreeCourse.Services.Catalog.Utilities.AppSettingsConfig
{
    public interface IDatabaseConfig
    {
        public string Name { get; set; }
        public AppsettingsCollection Collection { get; set; }
        public string ConnectionString { get; set; }
    }
}
