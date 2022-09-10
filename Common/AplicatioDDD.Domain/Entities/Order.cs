using AplicationDDD.Domain.Base.Entities;

namespace AplicatioDDD.Domain.Entities
{
    public class Order : Entity
    {
        public DateTimeOffset Time { get; set; }=DateTimeOffset.Now;//время заказа 
        public ICollection<OrderItem> items { get; set; }= new HashSet<OrderItem>();//список элементов заказа
    }

}
