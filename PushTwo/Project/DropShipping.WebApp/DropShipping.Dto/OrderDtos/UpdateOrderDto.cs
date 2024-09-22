namespace DropShipping.Dto.OrderDtos
{
    public class UpdateOrderDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string CardNumber { get; set; }

        public string City { get; set; }

        public int UserId { get; set; }

        public string PostalCode { get; set; }
    }
}
