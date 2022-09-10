using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationDDD.Interfaces.Base.Enteties
{
    public interface IEntity<T>
    {
        T id { get; set; }

    }

    public interface IEntity : IEntity<int> { }
    public interface INamedEntity : IEntity
    {
        [Required]
        string name { get; set; }   
    }
}
