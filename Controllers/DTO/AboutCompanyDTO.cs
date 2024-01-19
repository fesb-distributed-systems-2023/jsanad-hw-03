using CompanyApplication.Controllers.DTO;
using CompanyApplication.Repositories;
using DIS_projekt.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyApplication.Controllers.DTO
{
    public class AboutCompanyDTO
    {
        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public string? OwnerName { get; set; }
        public string? Revenue { get; set; }

        public static AboutCompanyDTO FromModel(Company company)
        {
            return new AboutCompanyDTO
            {
                Id = company.Id,
                CompanyName = company.CompanyName,
                OwnerName = company.OwnerName,
                Revenue = company.Revenue,
            };
        }
    }
}
