namespace dot.Models
{
    public class IotDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string MeasurementsCollectionName { get; set; } = null!;
    }
}
