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
        // string connectionString = "Server=127.0.0.1;Port=3306;database=addresses;user id=root;password=Higgins5021";
        private readonly ILogger<AddressController> _logger;

        public AddressController(ILogger<AddressController> logger)
        {
            _logger = logger;
        }

        // localhost/addresses/get
        [Route("get")]
        public async Task<IEnumerable<AddressModel>> GetAsync()
        {
            List<AddressModel> addresses;

            string sql = "use addresses;" + 
                "select * from addressdb";
            
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                var rows = await connection.QueryAsync<AddressModel>(sql, new { });

                addresses = rows.ToList();
            }

            return addresses.ToArray();
        }

        // localhost/addresses/delete
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

        // localhost/addresses/put/parameters
        // parameters are all strings, e.g. put/street/neighborhood/city/county/state/province/zip/PO/country
        // " " can be used for null values
        [Route("put/{StreetAddress}/{Neighborhood}/{City}/{County}/{State}/{Province}/{PostalCode}/{PostOffice}/{Country}")]
        public async Task InsertData(string StreetAddress, string Neighborhood, string City, string County, string State, 
            string Province, string PostalCode, string PostOffice, string Country)
        {
            string sql = "use addresses;" +
                    "insert into addressdb (StreetAddress, Neighborhood, City, County, State, Province, PostalCode, PostOffice, Country) " +
                    "values (@StreetAddress, @Neighborhood, @City, @County, @State, @Province, @PostalCode, @PostOffice, @Country);";

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql, new
                {
                    StreetAddress = StreetAddress,
                    Neighborhood = Neighborhood,
                    City = City,
                    County = County,
                    State = State,
                    Province = Province,
                    PostalCode = PostalCode,
                    PostOffice = PostOffice,
                    Country = Country
                });
            }
        }

        // localhost/addresses/exactsearch/parameters
        // parameters are all strings, e.g. search/street/neighborhood/city/county/state/province/zip/PO/country
        // " " can be used for null values
        [Route("exactsearch/{StreetAddress}/{Neighborhood}/{City}/{County}/{State}/{Province}/{PostalCode}/{PostOffice}/{Country}")]
        public async Task<IEnumerable<AddressModel>> ExactSearchData(string StreetAddress, string Neighborhood, string City, string County, string State,
            string Province, string PostalCode, string PostOffice, string Country)
        {
            List<AddressModel> addresses;

            string sql = "use addresses; " +
                "select * from addressdb " +
                "where StreetAddress=@StreetAddress and Neighborhood=@Neighborhood and City=@City and County=@County and State=@State " +
                "and Province=@Province and PostalCode=@PostalCode and PostOffice=@PostOffice and Country=@Country;";

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                var rows = await connection.QueryAsync<AddressModel>(sql, new
                {
                    StreetAddress = StreetAddress,
                    Neighborhood = Neighborhood,
                    City = City,
                    County = County,
                    State = State,
                    Province = Province,
                    PostalCode = PostalCode,
                    PostOffice = PostOffice,
                    Country = Country
                });
                addresses = rows.ToList();
            }
            return addresses.ToArray();
        }

        // localhost/addresses/partialsearch/parameters
        // parameters are all strings, e.g. search/street/neighborhood/city/county/state/province/zip/PO/country
        // " " can be used for null values
        [Route("partialsearchinternational/{StreetAddress}/{Neighborhood}/{City}/{County}/{State}/{Province}/{PostalCode}/{PostOffice}/{Country}")]
        public async Task<IEnumerable<AddressModel>> PartialSearchDataInternational(string StreetAddress, string Neighborhood, string City, string County, string State,
            string Province, string PostalCode, string PostOffice, string Country)
        {
            List<AddressModel> addresses;

            string sql = "use addresses; " +
                "select * from addressdb " +
                "where StreetAddress=@StreetAddress or Neighborhood=@Neighborhood or City=@City or County=@County or State=@State " +
                "or Province=@Province or PostalCode=@PostalCode or PostOffice=@PostOffice or Country=@Country;";

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                var rows = await connection.QueryAsync<AddressModel>(sql, new
                {
                    StreetAddress = StreetAddress,
                    Neighborhood = Neighborhood,
                    City = City,
                    County = County,
                    State = State,
                    Province = Province,
                    PostalCode = PostalCode,
                    PostOffice = PostOffice,
                    Country = Country
                });
                addresses = rows.ToList();
            }
            return addresses.ToArray();
        }

        // localhost/addresses/partialsearch/parameters
        // parameters are all strings, e.g. search/street/neighborhood/city/county/state/province/zip/PO/country
        // " " can be used for null values
        [Route("partialsearchlocal/{StreetAddress}/{Neighborhood}/{City}/{County}/{State}/{Province}/{PostalCode}/{PostOffice}/{Country}")]
        public async Task<IEnumerable<AddressModel>> PartialSearchDataLocal(string StreetAddress, string Neighborhood, string City, string County, string State,
            string Province, string PostalCode, string PostOffice, string Country)
        {
            List<AddressModel> addresses;

            string sql = "use addresses; " +
                "select * from addressdb " +
                "where (StreetAddress=@StreetAddress or Neighborhood=@Neighborhood or City=@City or County=@County or State=@State " +
                "or Province=@Province or PostalCode=@PostalCode or PostOffice=@PostOffice) and Country=@Country;";

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                var rows = await connection.QueryAsync<AddressModel>(sql, new
                {
                    StreetAddress = StreetAddress,
                    Neighborhood = Neighborhood,
                    City = City,
                    County = County,
                    State = State,
                    Province = Province,
                    PostalCode = PostalCode,
                    PostOffice = PostOffice,
                    Country = Country
                });
                addresses = rows.ToList();
            }
            return addresses.ToArray();
        }


        [Route("search/{Country}")]
        public async Task<IEnumerable<AddressModel>> SearchData(string Country)
        {
            List<AddressModel> addresses;

            string sql = "use addresses; " +
                "select * from addressdb " +
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

        [Route("match/{City}")]
        public async Task<IEnumerable<AddressModel>> SearchCity(string City)
        {
            List<AddressModel> addresses;

            string sql = "use addresses; " +
                "select * from addressdb " +
                "where City=@City;";

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                var rows = await connection.QueryAsync<AddressModel>(sql, new
                {
                    City = City
                });
                addresses = rows.ToList();
            }
            return addresses.ToArray();
        }
    }
}
