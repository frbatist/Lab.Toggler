namespace Lab.Toggler.Model
{
    public class FeatureModelResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public FeatureModelResponse(int id, string name, bool isActive)
        {
            Id = id;
            Name = name;
            IsActive = isActive;
        }
    }
}
