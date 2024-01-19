using DIS_projekt.Models;
using System.Collections.Generic;

namespace CompanyApplication.Logic
{
    public interface ICompanyLogic
    {
        bool ValidateCompany(Company company);
        IEnumerable<Company> GetAllCompanies();
        Company GetSingleCompany(int id);
        void CreateNewCompany(Company company);
        void UpdateCompany(int id, Company company);
        bool DeleteCompany(int id);
        void ValidateRevenueField(string revenue);
    }
}