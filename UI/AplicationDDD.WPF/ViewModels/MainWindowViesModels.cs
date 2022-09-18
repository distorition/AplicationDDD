using AplicatioDDD.Domain.Entities;
using AplicationDDD.Interfaces;
using AplicationDDD.WebApe.Client.Enmployees;
using AplicationDDD.WPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AplicationDDD.WPF.ViewModels
{
    public class MainWindowViesModels:ViewModel
    {
        private readonly IRepositoryAsync<Employe> _EmployesRepository;
        private string _Title = "Главное окно программы";
        public string Title { get=> _Title; set => Set(ref _Title, value); }


        private IEnumerable<Employe> _Employes;
        public IEnumerable<Employe>? Employes
        {
            get
            {
                if(_Employes is not null)
                {
                    return _Employes;//если в поле что то есть то просто возвращаем 
                }
                _ = LoadEmployeeAsync();//если же пуст то запрашиваем данные , символ(_ = это симвл отмены результата чтобы то что он возвращал не учитывалс компилятором)
                return null;
            }
        }

        private async Task LoadEmployeeAsync()
        {
            var employess= await _EmployesRepository.GetAllAsync().ConfigureAwait(false);
            Set(ref _Employes, employess, nameof(Employes));
        }
        public MainWindowViesModels()
        {
            var client = new HttpClient { BaseAddress = new Uri("https://localhost:7182/") };

            var emloyes= new EmployesClient(client,null!);

            _EmployesRepository = emloyes;
        }
    }
}
