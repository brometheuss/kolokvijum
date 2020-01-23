using Application.Commands;
using Application.DTO;
using Application.Queries;
using Application.Responses;
using DataAccess.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfGetInvoicesCommand : EfBaseCommand, IGetInvoicesCommand
    {
        public EfGetInvoicesCommand(ChinookContext context) : base(context)
        {
        }

        public PagedResponse<InvoiceDto> Execute(InvoiceQuery request)
        {
            var query = Context.Invoice
                .Include(il => il.InvoiceLine)
                .ThenInclude(t => t.Track)
                .Include(c => c.Customer)
                .AsQueryable();

            if (request.TrackName != null)
                query = query.Where(i => i.InvoiceLine.Any(t => t.Track.Name.ToLower().Contains(request.TrackName.ToLower())));

            if (request.CustomerId > 0)
                query = query.Where(i => i.Customer.CustomerId == request.CustomerId);

            if (request.CustomerCompany != null)
                query = query.Where(i => i.Customer.Company.ToLower().Contains(request.CustomerCompany.ToLower()));

            if (request.CustomerState != null)
                query = query.Where(i => i.Customer.State.ToLower().Contains(request.CustomerState.ToLower()));

            if (request.MinPrice.HasValue)
                query = query.Where(i => i.InvoiceLine.Any(il => il.UnitPrice > request.MinPrice));

            if (request.MaxPrice.HasValue)
                query = query.Where(i => i.InvoiceLine.Any(il => il.UnitPrice < request.MaxPrice));

            var totalCount = query.Count();

            query = query.Skip((request.PageNumber - 1) * request.PerPage).Take(request.PerPage);

            var pagesCount = (int)Math.Ceiling((double)totalCount / request.PerPage);

            return new PagedResponse<InvoiceDto>
            {
                CurrentPage = request.PageNumber,
                PagesCount = pagesCount,
                TotalCount = totalCount,
                Data = query.Select(i => new InvoiceDto
                {
                    Id = i.InvoiceId,
                    BillingAddress = i.BillingAddress,
                    BillingCountry = i.BillingCountry,
                    BillingPostalCode = i.BillingPostalCode,
                    BillingState = i.BillingState,
                    CustomerEmail = i.Customer.Email,
                    CustomerFirstName = i.Customer.FirstName,
                    CustomerLastName = i.Customer.LastName,
                    CustomerPhone = i.Customer.Phone,
                    InvoiceLines = i.InvoiceLine.Select(il => new InvoiceLineDto
                    {
                        InvoiceLineId = il.InvoiceLineId,
                        Quantity = il.Quantity,
                        TrackName = il.Track.Name,
                        UnitPrice = il.UnitPrice
                    })
                })
            };
        }
    }
}
