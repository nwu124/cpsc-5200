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
    [Route("countries")]
    public class CountryFormatController : ControllerBase
    {
        string connectionString = "Server=127.0.0.1;Port=3306;database=addresses;user id=root;password=nathaniel";
        // string connectionString = "Server=127.0.0.1;Port=3306;database=addresses;user id=root;password=Higgins5021";
        private readonly ILogger<AddressController> _logger;

        public CountryFormatController(ILogger<AddressController> logger)
        {
            _logger = logger;
        }

        // localhost/countries/get
        [Route("get")]
        public async Task<IEnumerable<AddressModel>> GetAsync()
        {
            List<AddressModel> addresses;

            string sql = "use addresses;" +
                "select * from countriesdb";

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                var rows = await connection.QueryAsync<AddressModel>(sql, new { });

                addresses = rows.ToList();
            }

            return addresses.ToArray();
        }

        // localhost/countries/delete
        [Route("delete")]
        public async Task DeleteData()
        {
            string sql = "use addresses;" +
                    "delete from countriesdb;";
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql, new { });
            }
        }

        // localhost/addresses/put/parameters
        // parameters are boolean values except the country name, e.g. put/0/0/0/1/0/USA
        [Route("put/{StreetAddress}/{City}/{State}/{Province}/{PostalCode}/{Country}")]
        public async Task InsertData(int StreetAddress, int City, int State,
            int Province, int PostalCode, string Country)
        {
            string sql = "use addresses;" +
                    "insert into countriesdb (StreetAddress, City, State, Province, PostalCode, Country) " +
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

        // localhost/addresses/search/parameters
        // parameter is country name, e.g. search/USA 
        [Route("search/{Country}")]
        public async Task<IEnumerable<AddressModel>> SearchData(string Country)
        {
            List<AddressModel> addresses;

            string sql = "use addresses; " +
                "select * from countriesdb " +
                "where Country=@Country;";

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                var rows = await connection.QueryAsync<AddressModel>(sql, new
                {
                    Country = Country
                });
                addresses = rows.ToList();
            }
            return addresses.ToArray();
        }
    }
}
