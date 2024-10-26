namespace DropShipping.Dto.OrderDtos
{
    public class AddOrderDto
    {
        public int UserId { get; set; }
        public List<int> ProductIds { get; set; }
    }
}