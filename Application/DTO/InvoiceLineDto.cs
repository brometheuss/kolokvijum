using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class InvoiceLineDto
    {
        public int InvoiceLineId { get; set; }
        public string TrackName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
