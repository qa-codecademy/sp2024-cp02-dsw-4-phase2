namespace DropShipping.Dto.OrderDtos
{
    public class UpdateOrderDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public List<int> ProductIds { get; set; }
    }
}
