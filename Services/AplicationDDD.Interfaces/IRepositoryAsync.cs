using AplicationDDD.Interfaces.Base.Enteties;

namespace AplicationDDD.Interfaces
{
    public interface IRepositoryAsync<T> where T : class, IEntity //так же только асинхронный интерфейс
    {
       Task<IEnumerable<T>> GetAllAsync(CancellationToken token=default);//token нужен для отмены асинхронный операции
        Task<T?> GetTByIdAsync(int id, CancellationToken token = default);//default это чтобы можно было не указывать сам обьект при использовании метода
        Task<int> CountAsync(CancellationToken token = default);
        //T? Add(T entity) либо сделать так чтобы при добавлении возвращался именно сам добавленный  обьект 
        Task<int> AddAsync(T entity,CancellationToken token= default);

        Task<bool> UpdateAsync(T entity, CancellationToken token = default);
        Task<T?> DeleteAsync(T entity, CancellationToken token = default);
    }
}
