using System;
using System.ComponentModel.DataAnnotations;

namespace AcemStudios.ApiRefactor.DTOs
{
    public class PaymentChargeDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        [Required]
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Amount { get; set; } = 10.00M;
    }
}
