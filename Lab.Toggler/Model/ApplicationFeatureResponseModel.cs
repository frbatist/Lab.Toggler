namespace Lab.Toggler.Model
{
    /// <summary>
    /// Application Feature
    /// </summary>
    public class ApplicationFeatureResponseModel
    {
        /// <summary>
        /// Application feature Id
        /// </summary>
        public int Id { get; set; }
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
        /// Indicates if it's active
        /// </summary>
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
