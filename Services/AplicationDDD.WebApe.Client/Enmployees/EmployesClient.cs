using AplicatioDDD.Domain.Entities;
using AplicationDDD.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AplicationDDD.WebApe.Client.Enmployees
{
    /// <summary>
    /// для каждого метода в клиенте должен быть такой же метод в контроллере 
    /// </summary>
    public class EmployesClient:IRepositoryAsync<Employe>
    {
        private readonly HttpClient _HttpClient;
        private readonly ILogger<EmployesClient> Logger;

        public EmployesClient(HttpClient httpClient,ILogger<EmployesClient> logger)
        {
            _HttpClient = httpClient;
            Logger = logger;
        }

        public Task<int> AddAsync(Employe entity, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CountAsync(CancellationToken token = default)
        {
            var count = await _HttpClient.GetFromJsonAsync<int>("api/EmployerApi/Count").ConfigureAwait(false);
            return count;
        }

        public Task<Employe?> DeleteAsync(Employe entity, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Employe>> GetAllAsync(CancellationToken token = default)
        {
            var employes = await _HttpClient// длеаем гет запрос при помощи http клиента     
                .GetFromJsonAsync<IEnumerable<Employe>>("api/EmployerApi",token)//и деселаризуем  ответ в формат джейсон который нам прислал указанный адрес ( это адрес нашего контроллера)
                .ConfigureAwait(false);

            if(employes is null)
            {
                throw new InvalidOperationException("не удалось получиться данные от серввиса");
            }
            return employes;
        }

        public async Task<Employe?> GetTByIdAsync(int id, CancellationToken token = default)
        {
            var employe = await _HttpClient.GetFromJsonAsync<Employe>("api/EmployerApi,{id}",token).ConfigureAwait(false);
            return employe;
        }

        public Task<bool> UpdateAsync(Employe entity, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
