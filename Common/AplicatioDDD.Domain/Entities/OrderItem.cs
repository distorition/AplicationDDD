using AplicationDDD.Domain.Base.Entities;

namespace AplicatioDDD.Domain.Entities
{
    public class OrderItem : Entity
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }

}
