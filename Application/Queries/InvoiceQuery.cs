using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries
{
    public class InvoiceQuery : BaseQuery
    {
        public string TrackName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerCompany { get; set; }
        public string CustomerState { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }
    }
}
