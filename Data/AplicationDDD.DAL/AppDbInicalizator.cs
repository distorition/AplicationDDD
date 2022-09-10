using AplicatioDDD.Domain.Entities;
using AplicationDDD.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationDDD.DAL
{
    public class AppDbInicalizator//класс для инициализации базы данных
    {
        private readonly AppDB _db;
        private readonly ILogger<AppDbInicalizator> Logger;

        public AppDbInicalizator(AppDB db,ILogger<AppDbInicalizator> logger)
        {
            _db = db;
            Logger = logger;
        }
        public  async Task<bool> RemoveAsync(CancellationToken token = default)//удаляем базу данных
        {
            if(!await _db.Database.EnsureDeletedAsync(token).ConfigureAwait(false))//ConfigureAwait нужен для избегания дед локов ( блокирует сохранения исходных потоков)
            {
                Logger.LogInformation("База Данных отсутствует");
                return false;
            }
            Logger.LogInformation("База Данных Удалена");
            return true;

        }
        public async Task InitialAsync(bool RemoveBefore=false,CancellationToken cancellationToken=default)//метод для отмены асинхронных операций 
        {
            if (RemoveBefore)//если нужно удалить базу данных то удаляем 
            {
                await RemoveAsync(cancellationToken).ConfigureAwait(false);
            }
            await _db.Database.MigrateAsync(cancellationToken).ConfigureAwait(false);//выполняем миграции , таким образом оно автоматически применит миграции из сборки 
            Logger.LogInformation("База Данных создана");

            await AddTestDaataAsync(cancellationToken);
        }
        private async Task AddTestDaataAsync(CancellationToken cancellationToken = default)//добавялем разного
        {
            //если мы в первой строчке используеем ConfigureAwait false то в последующих уже не потербуется ибо все остальные уже из пула пойдут 
            if (!await _db.Employers.AnyAsync(cancellationToken).ConfigureAwait(false))//если ничего нет в нашей базе данных (в таблице сотрудников) то создаем
            {
                var deps = Enumerable.Range(1, 10).Select(i => new Departamen //создаем 10 офисов 
                {
                    name = $"Dep-{i}"

                }).ToArray();

                var rnd = new Random();

                var employers = Enumerable.Range(1, 100).Select(i => new Employe //создаем сотрудников 
                {
                    LastName = $"LastName-{i}",
                    name = $"Name-{i}",
                    Patronomic = $"Patronomic-{i}",
                    Salary = rnd.Next(1, 100),
                    departamen = deps[rnd.Next(0, deps.Length)],
                });
               await _db.Employers.AddRangeAsync(employers,cancellationToken);
               await  _db.departamens.AddRangeAsync(deps,cancellationToken);
               await _db.SaveChangesAsync(cancellationToken);  
            }
          
            if(!await _db.products.AnyAsync(cancellationToken))
            {
                var product = Enumerable.Range(1, 10).Select(i => new Product
                {
                    name = $"Product-{i}",

                }).ToArray();
                await _db.AddRangeAsync(product,cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);
            }
 
        }

    }
}
