using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;
using MySql.Data.MySqlClient;
using Dapper;

namespace BlazorServer.Controllers
{
    [ApiController]
    [Route("addresses")]
    public class AddressController : ControllerBase
    {
        string connectionString = "Server=127.0.0.1;Port=3306;database=addresses;user id=root;password=nathaniel";
        private readonly ILogger<AddressController> _logger;

        public AddressController(ILogger<AddressController> logger)
        {
            _logger = logger;
        }

        [Route("get")]
        public async Task<IEnumerable<AddressModel>> GetAsync()
        {
            List<AddressModel> addresses;

            string sql = "select * from addressdb";
            

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                var rows = await connection.QueryAsync<AddressModel>(sql, new { });

                addresses = rows.ToList();
            }

            return addresses.ToArray();
        }

        [Route("delete")]
        public async Task DeleteData()
        {
            string sql = "use addresses;" +
                    "delete from addressdb;";
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql, new { });
            }
        }

        [Route("put/{StreetAddress}/{City}/{State}/{Province}/{PostalCode}/{Country}")]
        public async Task InsertData(string StreetAddress, string City, string State, 
            string Province, string PostalCode, string Country)
        {
            string sql = "use addresses;" +
                    "insert into addressdb (StreetAddress, City, State, Province, PostalCode, Country) " +
                    "values (@StreetAddress, @City, @State, @Province, @PostalCode, @Country);";

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql, new
                {
                    StreetAddress = StreetAddress,
                    City = City,
                    State = State,
                    Province = Province,
                    PostalCode = PostalCode,
                    Country = Country
                });
            }
        }
    }
}
