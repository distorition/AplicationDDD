using AplicationDDD.DAL.Context;
using AplicationDDD.Interfaces;
using AplicationDDD.Interfaces.Base.Enteties;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationDDD.DAL.Repositories
{
    /// <summary>
    /// этот класс( репозиторий) нужен для реализации работы с базой данных
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntitiesRepositories<T> : IRepositoryAsync<T> where T : class, IEntity
    {
        private readonly AppDB _Db;
        private readonly ILogger<EntitiesRepositories<T>> _Logger;

        public EntitiesRepositories(AppDB db,ILogger<EntitiesRepositories<T>> logger)
        {
            _Db = db;
            _Logger = logger;
        }
        public async Task<int> AddAsync(T entity, CancellationToken token = default)
        {
            await _Db.Set<T>().AddAsync(entity, token).ConfigureAwait(false);//берем таблицу нашей  базы данных( Set) и просто добавляем что нам надо ( AddAsync)
            //await _Db.AddAsync(entity,token).ConfigureAwait(false);//либо просто сразу добавляем в нашу базу данных 
            //_Db.Entry(entity).State = EntityState.Added;//либо мы просто помечаем обьект как добавленный 

            await _Db.SaveChangesAsync(token).ConfigureAwait(false);
            return entity.id;// и возвращаем ид обьекта котоырй добавили 
        }

        public async Task<int> CountAsync(CancellationToken token = default)
        {
            var result= await _Db.Set<T>().CountAsync(token).ConfigureAwait(false);//Set это мы образаемся к одному из наших столбцов в базе данных
            return result;
        }

        public async Task<T?> DeleteAsync(T entity, CancellationToken token = default)
        {
            //_Db.Set<T>().Remove(entity);//удаления конкретных данных
            //_Db.Remove(entity);//удаления всей бд
           // _Db.Entry(entity).State = EntityState.Deleted;//либо помечаем как удаленный 
            if(!await _Db.Set<T>().AnyAsync(i => i.id == entity.id, token).ConfigureAwait(false))//если элмента нет то возвращаем нул
            {
                return null;
            }
            _Db.Entry(entity).State = EntityState.Deleted;
            await _Db.SaveChangesAsync(token).ConfigureAwait(false);
            return entity;

        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default)
        {
            var items = await _Db.Set<T>().ToArrayAsync(token).ConfigureAwait(false);

            return items;
        }

        public async Task<T?> GetTByIdAsync(int id, CancellationToken token = default)
        {
           var item= await _Db.Set<T>().FirstOrDefaultAsync(o=>o.id==id).ConfigureAwait(false);//будет проблема в том что всегда будут отправлятся запросы в базу данных(более универсальный)

          //  var items= await _Db.Set<T>().FindAsync(id,token).ConfigureAwait(false);//этот пред данные из хеша то есть если у нас уже был запрос на определенный ид то он не будет отправялть запрос в бд а возмёт уже готовый из хеша
          return item;
        }

        public async Task<bool> UpdateAsync(T entity, CancellationToken token = default)
        {
            //_Db.Set<T>().Update(entity);//обновления конкректных данных

            //_Db.Update(entity);//обновления всей базы данных

            _Db.Entry(entity).State=EntityState.Modified;//либо помечаем обьект как обновленный ( самый быстрый но не универсальный)


            //самый универсальный
            //var db_item= await GetTByIdAsync(entity.id, token).ConfigureAwait(false);
            //if(db_item is null)
            //{
            //    return false;//если ничего не нашли 
            //}
            //else
            //{
            //    //логи на копирвоание данных из  item в  db_item
            //}
            return await _Db.SaveChangesAsync(token).ConfigureAwait(false)>0;
        }
    }
}
