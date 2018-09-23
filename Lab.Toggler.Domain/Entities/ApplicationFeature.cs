using Lab.Toggler.Domain.Interface.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab.Toggler.Domain.Entities
{
    public class ApplicationFeature : IEntity<int>
    {
        public int Id { get; set; }
        public Feature Feature { get; set; }
        public Application Application { get; set; }
        public bool IsActive { get; protected set; }

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
