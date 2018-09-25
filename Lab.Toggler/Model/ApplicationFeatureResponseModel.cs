namespace Lab.Toggler.Model
{
    public class ApplicationFeatureResponseModel
    {
        public int Id { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationVersion { get; set; }
        public string FeatureName { get; set; }
        public bool IsActive { get; protected set; }

        public ApplicationFeatureResponseModel(int id, string applicationName, string applicationVersion, string featureName, bool isActive)
        {
            Id = id;
            ApplicationName = applicationName;
            ApplicationVersion = applicationVersion;
            FeatureName = featureName;
            IsActive = isActive;
        }
    }
}
