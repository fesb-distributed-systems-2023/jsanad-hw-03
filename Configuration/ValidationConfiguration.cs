namespace DIS_project.Configuration
{
    public class ValidationConfiguration
    {
        public int IdMaxCharacters { get; set; }
        public int CompanyNameMaxCharacters { get; set; }
        public int OwnerNameMaxCharacters { get; set; }
        public int RevenueMaxCharacters { get; set; }
        public int RevenueMaxValue {  get; set; }
        public string? CompanyRegex { get; set; }
    }
}
