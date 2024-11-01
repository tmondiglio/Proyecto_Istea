namespace Api.ModelDTO
{

    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime DeliveryDate { get; set; }
        public CustomerDto Customer { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }

    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    }

    public class OrderItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Amount { get; set; }
        public ProductDto Product { get; set; }
    }

    public class ProductDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Family { get; set; }
    }
}