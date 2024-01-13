/*
**********************************
* Author: Josip Sanader
* Project Task: Company, Phase 2
**********************************
* Description:
*  
*  Implement `ICompanyRepository` interface
*
**********************************
*/

using Microsoft.AspNetCore.Mvc;
using DIS_projekt.Models;

namespace CompanyApplication.Repositories
{
    public class CompanyRepository
    {
        // List of all companies
        private readonly List<Company> m_lstCompanies;

        public CompanyRepository()
        {
            // Creating new list
            m_lstCompanies = new List<Company>();
        }

        // CREATE : Create new company
        public bool CreateNewCompany(Company company)
        {
            // Adding new company to the list
            m_lstCompanies.Add(company);

            return true;
        }

        // READ : Get all companies
        public IEnumerable<Company> GetAllCompanies()
        {
            // Returns entire list 
            return m_lstCompanies;
        }

        // READ : Get single company(specified by ID)
        public Company GetSingleCompany(int id)
        {
            if (!m_lstCompanies.Any(company => company.Id == id))
            {
                // Checks if any company matches currently used id, if not returns null
                return null;
            }

            var company = m_lstCompanies.FirstOrDefault(company => company.Id == id);

            // Checks if company matches an id, if yes returns that company
            return company;
        }

        // DELETE : Delete company (specified by ID)
        public bool DeleteCompany(int id)
        {
            // Check if company matches ID
            var companyToDelete = m_lstCompanies.FirstOrDefault(itemCompany => itemCompany.Id == id);
            if (companyToDelete == null)
            {
                return false;
            }

            m_lstCompanies.Remove(companyToDelete);

            return true;
        }
    }
}
