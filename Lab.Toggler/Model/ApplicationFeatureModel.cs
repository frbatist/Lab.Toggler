namespace Lab.Toggler.Model
{
    public class ApplicationFeatureModel
    {
        public string ApplicationName { get; set; }
        public string ApplicationVersion { get; set; }
        public string FeatureName { get; set; }        
        public bool IsActive { get; protected set; }

        public ApplicationFeatureModel(string applicationName, string applicationVersion, string featureName, bool isActive)
        {
            ApplicationName = applicationName;
            ApplicationVersion = applicationVersion;
            FeatureName = featureName;
            IsActive = isActive;
        }
    }
}
