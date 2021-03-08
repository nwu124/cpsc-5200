using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormValidation.Model
{
    public class AddressFormModel
    {
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string Neighborhood { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string County { get; set; }
        [Required]
        public string State { get; set; }

        public string Province { get; set; }

        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string PostOffice { get; set; }
        [Required]
        public string Country { get; set; }
    }
}