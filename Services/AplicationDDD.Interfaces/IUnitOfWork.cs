using AplicationDDD.Interfaces.Base.Enteties;

namespace AplicationDDD.Interfaces
{
    public interface IUnitOfWork<T> where T:class, IEntity
    {
        bool SaveChanges();
        Task<bool> SaveChangesAsync();   
    }
}
