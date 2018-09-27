namespace Lab.Toggler.Events
{
    public class ApplicationFeatureChanged
    {
        public string ApplicationName { get; set; }
        public string ApplicationVersion { get; set; }
        public string FeatureName { get; set; }
        public bool IsActive { get; set; }
    }
}
