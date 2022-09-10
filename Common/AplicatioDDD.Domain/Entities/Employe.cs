using AplicationDDD.Domain.Base.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplicatioDDD.Domain.Entities
{
    public class Employe:NamedEntity
    {
        public string LastName { get; set; }
        public string Patronomic { get;set; }
        [Column(TypeName ="decimal(18,2)")]//нужно бля таблицы в базе данных чтобы не было проблем
        public decimal Salary { get; set; }
        public Departamen departamen { get; set; }
    }

}
