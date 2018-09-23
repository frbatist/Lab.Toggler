using Lab.Toggler.Domain.Interface.Entity;

namespace Lab.Toggler.Domain.Entities
{
    public class Application : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; protected set; }
        public string Version { get; protected set; }

        public Application(string name, string version)
        {
            Name = name;
            Version = version;
        }
    }
}
