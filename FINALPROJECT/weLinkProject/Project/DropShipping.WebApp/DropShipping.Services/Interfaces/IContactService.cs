namespace DropShipping.Services.Interfaces
{
    public interface IContactService
    {
        Task SendEmailAsync(string to, string message);

        Task SendContactUsEmailAsync(string userName, string message);
    }
}
