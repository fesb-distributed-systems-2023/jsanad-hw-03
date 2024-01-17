using System.Collections.Generic;
using System.Data;
using System.Linq;
using DIS_projekt.Models;
using Microsoft.Data.Sqlite;

namespace CompanyApplication.Repositories
{
    public class CompanyRepository_SQL : ICompanyRepository
    {

        private readonly string _connectionString = "Data Source = \"C:\\Users\\josip\\OneDrive\\Radna površina\\jsanad-hw-03\\SQL\\Company.db\"";

        public bool CreateNewCompany(Company company)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                INSERT INTO Company (CompanyName, OwnerName, Product, Revenue)
                VALUES ($CompanyName, $OwnerName, $Product, $Revenue)";

            command.Parameters.AddWithValue("$CompanyName", company.CompanyName);
            command.Parameters.AddWithValue("$OwnerName", company.OwnerName);
            command.Parameters.AddWithValue("$Product", company.Product);
            command.Parameters.AddWithValue("$Revenue", company.Revenue);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected < 1)
            {
                throw new ArgumentException("Could not insert email into database.");
                return false;
            }
            return true;
        }

        public bool DeleteCompany(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                DELETE FROM Company
                WHERE Id == $id";

            command.Parameters.AddWithValue("$id", id);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected < 1)
            {
                throw new ArgumentException($"No companiess with ID = {id}.");
                return false;
            }
            return true;
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"SELECT Id, CompanyName, OwnerName, Product, Revenue FROM Company";

            using var reader = command.ExecuteReader();

            var results = new List<Company>();
            while (reader.Read())
            {

                var row = new Company
                {
                    Id = reader.GetInt32(0),
                    CompanyName = reader.GetString(1),
                    OwnerName = reader.GetString(2),
                    Product = reader.GetString(3),
                    Revenue = reader.GetInt32(4),
                };

                results.Add(row);
            }

            return results;
        }

        public Company? GetSingleCompany(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"SELECT Id, CompanyName, OwnerName, Product, Revenue FROM Company WHERE Id == $id";

            command.Parameters.AddWithValue("$id", id);

            using var reader = command.ExecuteReader();

            Company result = null;

            if (reader.Read())
            {
                result = new Company
                {
                    Id = reader.GetInt32(0),
                    CompanyName = reader.GetString(1),
                    OwnerName = reader.GetString(2),
                    Product = reader.GetString(3),
                    Revenue = reader.GetInt32(4),
                };
            }

            return result;
        }

    }
}