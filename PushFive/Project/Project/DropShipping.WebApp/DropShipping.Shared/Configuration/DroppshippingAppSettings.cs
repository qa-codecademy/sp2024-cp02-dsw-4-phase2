namespace DropShipping.Shared.Configuration
{
    public class DroppshippingAppSettings
    {
        public string ConnectionString { get; set; }

        public string SecretKey { get; set; }

        public string MailjetApiKey { get; set; } // DELETE IF NOT WORK

        public string MailjetApiSecret { get; set; } // DELETE IF NOT WORK

        public string Email { get; set; }
    }
}