using CompanyApplication.Repositories;
using DIS_project.Filters;
using DIS_projekt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using CompanyApplication.Controllers.DTO;
using CompanyApplication.Logic;
using DIS_project.Controllers.DTO;
using System.ComponentModel.DataAnnotations;

namespace CompanyApplication.Controllers
{
    [ErrorFilter]
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyLogic _companyLogic;

        public CompanyController(ICompanyLogic companyLogic)
        {
            this._companyLogic = companyLogic;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AboutCompanyDTO>> Get()
        {
            var allCompanies = _companyLogic.GetAllCompanies().Select(x => AboutCompanyDTO.FromModel(x));
            return Ok(allCompanies);
        }

        [HttpGet("{id}")]
        public ActionResult<AboutCompanyDTO> Get(int id)
        {
            var company = _companyLogic.GetSingleCompany(id);
            if (company == null)
            {
                return NotFound($"Company with ID {id} not found.");
            }

            return Ok(AboutCompanyDTO.FromModel(company));
        }

        [HttpPost]
        public ActionResult Post([FromBody] NewCompanyDTO company)
        {
            if (company == null)
            {
                return BadRequest($"Wrong company format!");
            }

            _companyLogic.CreateNewCompany(company.ToModel());

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] NewCompanyDTO updatedCompany)
        {
            if (updatedCompany == null)
            {
                return BadRequest($"Wrong Company format!");
            }

            var existingCompany = _companyLogic.GetSingleCompany(id);
            if (existingCompany == null)
            {
                return NotFound($"Company with ID {id} not found.");
            }

            _companyLogic.UpdateCompany(id, updatedCompany.ToModel());

            return Ok();
        }

    
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var company = _companyLogic.GetSingleCompany(id);
            if (company == null)
            {
                return NotFound($"Company with ID {id} not found.");
            }

            _companyLogic.DeleteCompany(id);

            return Ok();
        }
    }
}