namespace Lab.Toggler.Model
{
    /// <summary>
    /// Feature
    /// </summary>
    public class FeatureModelResponse
    {
        /// <summary>
        /// Feature Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Feature name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Indicates if feature is active
        /// </summary>
        public bool IsActive { get; set; }

        public FeatureModelResponse(int id, string name, bool isActive)
        {
            Id = id;
            Name = name;
            IsActive = isActive;
        }
    }
}
