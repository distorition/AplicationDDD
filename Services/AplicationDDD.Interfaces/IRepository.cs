using AplicationDDD.Interfaces.Base.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationDDD.Interfaces
{
    public interface IRepository<T> where T : class,IEntity
    {
        IEnumerable<T> GetAll();    
        T? GetTById(int id);
        int Count();
        //T? Add(T entity) либо сделать так чтобы при добавлении возвращался именно сам добавленный  обьект 
        int Add(T entity);//добавить элемент в репозитоий и возвращает индефикатор обьетка который репозиторий ему назначил

        bool Update(T entity);//обновленя обьекты и возвращает удалось ли ему сделать или нет
        T? Delete(T entity);//вернём сам обьект который удалили 
    }
}
