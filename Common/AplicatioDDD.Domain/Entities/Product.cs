using AplicationDDD.Domain.Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicatioDDD.Domain.Entities
{
    public class Product:NamedEntity
    {
        [Column(TypeName = "decimal(18,2)")]//нужно бля таблицы в базе данных чтобы не было проблем
        public decimal Price { get; set; }
    }

}
