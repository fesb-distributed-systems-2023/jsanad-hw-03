using DIS_projekt.Models;

namespace DIS_project.Controllers.DTO
{
    public class NewCompanyDTO
    {
        public string? CompanyName { get; set; }
        public string? OwnerName { get; set; }
        public string? Revenue { get; set; }

        public Company ToModel()
        {
            return new Company
            {
                Id = -1,
                CompanyName = CompanyName,
                OwnerName = OwnerName,
                Revenue = Revenue,
               
            };
        }
    }
}
