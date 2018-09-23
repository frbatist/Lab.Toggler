using Lab.Toggler.Domain.Interface.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab.Toggler.Domain.Entities
{
    public class Feature : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; protected set; }
        public bool IsActive { get; protected set; }

        protected Feature()
        {
        }

        public Feature(string name, bool isActive)
        {
            Name = name;
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
