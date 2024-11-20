using DropShipping.Services.Interfaces;
using DropShipping.Shared.Configuration;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace DropShipping.Services.Implementations
{
    public class ContactService : IContactService
    {
        private readonly string _emailAddress;
        private readonly string _mailjetApiKey;
        private readonly string _mailjetApiSecret;

        public ContactService(IOptions<DroppshippingAppSettings> options)
        {
            var config = options.Value;
            _emailAddress = config.Email;
            _mailjetApiKey = config.MailjetApiKey;
            _mailjetApiSecret = config.MailjetApiSecret;
        }

        public async Task SendEmailAsync(string customerEmail, string description)
        {
            MailjetClient client = new MailjetClient(_mailjetApiKey, _mailjetApiSecret);

            MailjetRequest request = new MailjetRequest
            {
                Resource = SendV31.Resource,
            }
            .Property(Send.Messages, new JArray
            {
                new JObject
                {
                    {
                        "From", new JObject
                        {
                            {"Email", _emailAddress},
                            {"Name", "DropShipping Store"}
                        }
                    },
                    {
                        "To", new JArray
                        {
                            new JObject
                            {
                                {"Email", customerEmail},
                                {"Name", "Customer"}
                            }
                        }
                    },
                    {"Subject", "Thank You! Your Order Has Been Confirmed."},
                    {"TextPart", description},
                    {"HTMLPart", $"<h3>Dear Customer</h3><p>{description}</p>"}
                }
            });

            MailjetResponse response = await client.PostAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                string errorInfo = response.GetErrorInfo();
                Console.WriteLine(errorInfo);
                throw new Exception($"Failed to send email: {response.GetErrorInfo()}");
            }
        }

        public async Task SendContactUsEmailAsync(string userEmail, string userMessage)
        {
            MailjetClient client = new MailjetClient(_mailjetApiKey, _mailjetApiSecret);

            MailjetRequest request = new MailjetRequest
            {
                Resource = SendV31.Resource,
            }
            .Property(Send.Messages, new JArray
            {
        new JObject
        {
            {
                "From", new JObject
                {
                    {"Email", _emailAddress},
                    {"Name", "DropShipping Store Support"}
                }
            },
            {
                "To", new JArray
                {
                    new JObject
                    {
                        {"Email", _emailAddress},
                        {"Name", "DropShipping Store Support"}
                    }
                }
            },
            {"Subject", "Contact Us - Customer Inquiry"},
            {"TextPart", userMessage},
            {"HTMLPart", $"<h3>Customer Inquiry</h3><p>{userMessage}</p><p>Contact Email: {userEmail}</p>"}

        }
            });

            MailjetResponse response = await client.PostAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                string errorInfo = response.GetErrorInfo();
                Console.WriteLine($"Failed to send email: {errorInfo}");
                throw new Exception($"Failed to send email: {errorInfo}");
            }
        }

    }
}