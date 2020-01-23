using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string BillingAddress { get; set; }
        public string BillingState { get; set; }
        public string BillingCountry { get; set; }
        public string BillingPostalCode { get; set; }
        public IEnumerable<InvoiceLineDto> InvoiceLines { get; set; }
    }
}
