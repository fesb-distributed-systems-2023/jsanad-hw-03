using System.Collections.Generic;
using DIS_projekt.Models;
using Microsoft.Data.Sqlite;

namespace CompanyApplication.Repositories
{
    public interface ICompanyRepository
    {
        bool CreateNewCompany(Company company);
        IEnumerable<Company> GetAllCompanies();
        Company GetSingleCompany(int id);
        bool DeleteCompany(int id);
    }
}
