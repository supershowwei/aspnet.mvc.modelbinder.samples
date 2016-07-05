namespace AspNetMvcModelBinderSamples.Models
{
    public abstract class Order
    {
        public int Id { get; set; }

        public OrderType Type { get; set; }
    }
}