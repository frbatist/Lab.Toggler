namespace Lab.Toggler.Model
{
    /// <summary>
    /// Application feature
    /// </summary>
    public class ApplicationFeatureModel
    {
        /// <summary>
        /// Name
        /// </summary>
        public string ApplicationName { get; set; }
        /// <summary>
        /// Version
        /// </summary>
        public string ApplicationVersion { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string FeatureName { get; set; }        
        /// <summary>
        /// Indicate if it's active
        /// </summary>
        public bool IsActive { get; set; }

        public ApplicationFeatureModel(string applicationName, string applicationVersion, string featureName, bool isActive)
        {
            ApplicationName = applicationName;
            ApplicationVersion = applicationVersion;
            FeatureName = featureName;
            IsActive = isActive;
        }
    }
}
