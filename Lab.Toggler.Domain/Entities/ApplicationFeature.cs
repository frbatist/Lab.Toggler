using Lab.Toggler.Domain.Interface.Entity;

namespace Lab.Toggler.Domain.Entities
{
    public class ApplicationFeature : IEntity<int>
    {
        public int Id { get; set; }
        public Feature Feature { get; protected set; }
        public int FeatureId { get; protected set; }
        public Application Application { get; protected set; }
        public int ApplicationId { get; set; }
        public bool IsActive { get; protected set; }

        protected ApplicationFeature()
        {

        }

        public ApplicationFeature(Feature feature, Application application, bool isActive)
        {
            Feature = feature;
            Application = application;
            IsActive = isActive;
        }

        public void Enable()
        {
            IsActive = true;
        }

        public void Disable()
        {
            IsActive = false;
        }
    }
}
