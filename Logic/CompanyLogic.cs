using System.Collections.Generic;
using CompanyApplication.Repositories;
using DIS_projekt.Models;
using DIS_project.Exceptions;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Data.Sqlite;
using CompanyApplication.Logic;
using DIS_project.Configuration;
using Microsoft.Extensions.Options;

namespace CompanyApplication.Logic
{
    public class CompanyLogic : ICompanyLogic
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ValidationConfiguration _validationConfiguration;
        public CompanyLogic(ICompanyRepository companyRepository, IOptions<ValidationConfiguration> configuration)
        {
            _companyRepository = companyRepository;
            _validationConfiguration = configuration.Value;
        }
        //public CompanyLogic(ICompanyRepository companyRepository)
        //{
        //    _companyRepository = companyRepository;
        //}

        private void ValidateCompanyNameField(string? companyName)
        {
            if (companyName is null)
            {
                throw new UserErrorException("Field: Company name cannot be empty.");
            }

            if (companyName.Length > _validationConfiguration.CompanyNameMaxCharacters)
            {
                throw new UserErrorException($"Field: Company name is too long. Exceeded {_validationConfiguration.CompanyNameMaxCharacters} characters");
            }

            if (!Regex.IsMatch(companyName, pattern: _validationConfiguration.CompanyRegex))
            {
                throw new UserErrorException($"Company invalid for company name '{companyName}'");
            }
            return;
        }
        private void ValidateOwnerNameField(string? ownerName)
        {
            if (string.IsNullOrEmpty(ownerName))
            {
                throw new UserErrorException("Field: Owner name cannot be empty.");
            }

            if (ownerName.Length > _validationConfiguration.OwnerNameMaxCharacters)
            {
                throw new UserErrorException($"Owner name field is too long. Exceeded {_validationConfiguration.OwnerNameMaxCharacters} characters");
            }

        }
        public void ValidateRevenueField(string revenue)
        {
            if (revenue is null)
            {
                throw new UserErrorException("Field: Revenue cannot be empty.");
            }

            if (revenue.Length > _validationConfiguration.RevenueMaxCharacters)
            {
                throw new UserErrorException($"Revenue field is too long. Exceeded {_validationConfiguration.RevenueMaxCharacters} characters");
            }

            bool succes = int.TryParse(revenue, out int revenueNumber);
            if (!succes || revenueNumber >= _validationConfiguration.RevenueMaxValue)
            {
                throw new UserErrorException($"Too big revenue value, max revenue value is {_validationConfiguration.RevenueMaxValue}.");
            }
        }
        public IEnumerable<Company> GetAllCompanies()
        {
            // Implement logic to get all companies from the repository
            return _companyRepository.GetAllCompanies();
        }

        public void CreateNewCompany(Company? company)
        {
            // Check all arguments
            if (company is null)
            {
                throw new UserErrorException("Cannot create a new company - no company specified or the company is invalid.");
            }

            // Sanitize inputs
            company.Id = -1;

            ValidateCompanyNameField(company.CompanyName);
            ValidateOwnerNameField(company.OwnerName);
            ValidateRevenueField(company.Revenue);

            // All fields validated, continue...

            _companyRepository.CreateNewCompany(company);

        }

        public void UpdateCompany(int id, Company? company)
        {
            // Check all arguments
            if (company is null)
            {
                throw new UserErrorException("Cannot create a new company. No company specified or the company is invalid.");
            }

            // Sanitize inputs
            company.Id = -1;

            ValidateCompanyNameField(company.CompanyName);
            ValidateOwnerNameField(company.OwnerName);
            ValidateRevenueField(company.Revenue);

            // All fields validated, continue...

            _companyRepository.UpdateCompany(id, company);
        }

        //private void ValidateRevenueField(string revenue)
        //{
        //    throw new NotImplementedException();
        //}
        public bool DeleteCompany(int id)
        {
            if (_companyRepository.GetSingleCompany(id) is null)
            {
                throw new UserErrorException($"Unable to find the requested company with id {id} to be deleted.");
            }
            else
            {
                _companyRepository.DeleteCompany(id);
                return true;
            }
        }

        //bool ICompanyLogic.DeleteCompany(int id)
        //{
        //    throw new NotImplementedException();
        //}
        bool ICompanyLogic.ValidateCompany(Company company)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Company> ICompanyLogic.GetAllCompanies()
        {
            throw new NotImplementedException();
        }

        Company ICompanyLogic.GetSingleCompany(int id)
        {
            throw new NotImplementedException();
        }
        //bool ICompanyLogic.CreateNewCompany(Company company)
        //{
        //    throw new NotImplementedException();
        //}

        //void ICompanyLogic.DeleteCompany(int id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}