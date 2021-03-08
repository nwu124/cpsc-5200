using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Models
{
    public class CountryFormatModel
    {
        public int Id { get; set; }
        public int StreetAddress { get; set; }
        public int City { get; set; }
        public int State { get; set; }
        public int Province { get; set; }
        public int PostalCode { get; set; }
        public string Country { get; set; }
    }
}
