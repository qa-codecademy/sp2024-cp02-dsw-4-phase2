namespace DropShipping.Shared.Configuration
{
    public class DroppshippingAppSettings
    {
        public string ConnectionString { get; set; }

        public string SecretKey { get; set; }

        public string MailjetApiKey { get; set; }

        public string MailjetApiSecret { get; set; }

        public string Email { get; set; }
    }
}