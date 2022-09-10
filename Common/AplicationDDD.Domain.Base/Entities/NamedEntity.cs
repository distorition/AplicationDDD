using AplicationDDD.Interfaces.Base.Enteties;

namespace AplicationDDD.Domain.Base.Entities
{
    public abstract class NamedEntity : Entity, INamedEntity
    {
        public string name { get; set; }
    }
}
