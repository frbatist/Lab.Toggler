namespace Lab.Toggler.Model
{
    /// <summary>
    /// Feature is enabled or disabled
    /// </summary>
    public class FeatureCheckModelResponse
    {
        /// <summary>
        /// Indicates if feature is enabled
        /// </summary>
        public bool Enabled { get; set; }
        public string Mesage { get; set; }
    }
}
