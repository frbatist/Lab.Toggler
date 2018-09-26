namespace Lab.Toggler.Tests.Unit.Controller.Contract
{
    public class ApplicationFeatureResponseModelContract
    {
        public int Id { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationVersion { get; set; }
        public string FeatureName { get; set; }
        public bool IsActive { get; set; }
    }
}
