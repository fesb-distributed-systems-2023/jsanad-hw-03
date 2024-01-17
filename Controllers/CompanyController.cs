/*
**********************************
* Author: Josip Sanader
* Project Task: Company, Phase 2
**********************************
* Description:
* 
*    CREATE - Add new company
*    READ - Get all companies
*    READ - Get specific company
*    DELETE - Delete companies
*
**********************************
*/

using CompanyApplication.Repositories;
using DIS_projekt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace CompanyApplication.Controllers
{
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

       
        [HttpPost("/companies/new")]
        public IActionResult CreateNewCompany([FromBody] Company company)
        {
            bool fSuccess = _companyRepository.CreateNewCompany(company);

            if (fSuccess)
            {
                return Ok("New company created!");
            }
            else
            {
                return BadRequest("Something went wrong!");
            }
        }

        
        [HttpGet("/companies/all")]
        public IActionResult GetAllCompanies()
        {
            return Ok(_companyRepository.GetAllCompanies());
        }

        
        [HttpGet("/companies/{id}")]
        public IActionResult GetSingleCompany([FromRoute] int id)
        {
            var company = _companyRepository.GetSingleCompany(id);

            if (company is null)
            {
                return NotFound($"Company with id:{id} doesn't exist!");
            }
            else
            {
                return Ok(company);
            }
        }

    
        [HttpDelete("/company/{id}")]
        public IActionResult DeleteCompany([FromRoute] int id)
        {
            if (_companyRepository.DeleteCompany(id))
            {
                return Ok($"Deleted company with id={id}!");
            }
            else
            {
                return NotFound($"Could not find company with id={id}!");
            }
        }
    }
}